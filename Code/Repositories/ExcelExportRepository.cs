using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models.ExcelExport;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Code.Repositories
{
    public class ExcelExportRepository : IExcelExportRepository
    {
        public MemoryStream ExportGroupInfo(List<ReportedTime> reportedTimes, List<EngagementPoints> engagementPoints, List<WorkDescription> workDescriptions, List<Survey> surveys)
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {

                ExportReportedTimes(package, reportedTimes);
                ExportEngagementPoints(package, engagementPoints);
                ExportWorkDescriptions(package, workDescriptions);
                ExportSurveys(package, surveys);

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }

        private void ExportReportedTimes(ExcelPackage package, List<ReportedTime> reportedTimes)
        {
            var workSheet = package.Workbook.Worksheets.Add("Reported times");
            var rowIndex = 1;
            var width = 6;
            var reportedTimesByProject = reportedTimes.GroupBy(r => r.ProjectName).ToList();
            foreach (var rt in reportedTimesByProject)
            {
                using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, width])
                {
                    cells.Merge = true;
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    cells.Value = rt.Key;
                    rowIndex++;
                }
                using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, width])
                {
                    cells.LoadFromText("Username,Date,Time in hours,Description,Issue ID,Received");
                    rowIndex++;
                }
                using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, width])
                {
                    cells.LoadFromArrays(rt.Select(r => new object[] { r.UserName, r.Date.ToLocalTime().ToString(), r.TimeInHours, r.Description, r.IssueId, r.ReportedDate.ToLocalTime().ToString() }));
                    rowIndex += rt.Count();
                }
                rowIndex++;
            }
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
        }

        private void ExportEngagementPoints(ExcelPackage package, List<EngagementPoints> engagementPoints)
        {
            var workSheet = package.Workbook.Worksheets.Add("Engagement points");
            var rowIndex = 1;
            var width = 6;
            var engagementPointsByProject = engagementPoints.GroupBy(e => e.ProjectName).ToList();
            foreach (var rt in engagementPointsByProject)
            {
                using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, width])
                {
                    cells.Merge = true;
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    cells.Value = rt.Key;
                    rowIndex++;
                }
                using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, width])
                {
                    cells.LoadFromText("User giving points,User receiving points,Number of points,Bonus?,Comment,Date");
                    rowIndex++;
                }
                var engagementPointsByUser = rt.GroupBy(ep => ep.AwardingUserName).ToList();
                foreach (var ep in engagementPointsByUser)
                {
                    var column = 1;
                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex + ep.Count() - 1, column])
                    {
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cells.Value = ep.Key;
                        column++;

                        //cells.LoadFromArrays(reportedTimes.Select(r => new object[] { r.UserName, r.Date.ToString(), r.TimeInHours, r.Description, r.IssueId, r.ReportedDate.ToString() }));
                        //rowIndex += reportedTimes.Count;
                    }

                    using (var cells = workSheet.Cells[rowIndex, column])
                    {
                        cells.LoadFromArrays(ep.Select(r => new object[] { r.ReceivingUserName, r.Points, r.Bonus }));
                        column += 3;
                    }

                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex + ep.Count() - 1, column])
                    {
                        workSheet.Column(column).Width = 60;
                        cells.Style.WrapText = true;
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cells.Value = ep.FirstOrDefault().Comment;
                        column++;
                    }

                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex + ep.Count() - 1, column])
                    {
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cells.Value = ep.FirstOrDefault().ReceivingDate.ToLocalTime().ToString();
                        column++;
                    }

                    rowIndex += ep.Count();
                }
                rowIndex++;
            }
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            workSheet.Column(1).Width = 20.0;
            workSheet.Column(5).Width = 60.0;
            workSheet.Column(6).Width = 20.0;
        }

        private void ExportWorkDescriptions(ExcelPackage package, List<WorkDescription> workDescriptions)
        {
            var workSheet = package.Workbook.Worksheets.Add("Work descriptions");
            var rowIndex = 1;
            var width = 4;
            var workDescriptionsByProject = workDescriptions.GroupBy(r => r.ProjectName).ToList();
            foreach (var rt in workDescriptionsByProject)
            {
                using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, width])
                {
                    cells.Merge = true;
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    cells.Value = rt.Key;
                    rowIndex++;
                }
                using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, width])
                {
                    cells.LoadFromText("User,Description,Date,Comments");
                    rowIndex++;
                }
                foreach (var d in rt)
                {
                    var commCount = d.Comments.Count == 0 ? 1 : d.Comments.Count;
                    var column = 1;
                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex + commCount - 1, column])
                    {
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cells.Value = d.UserName;
                        column++;
                    }
                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex + commCount - 1, column])
                    {
                        cells.Style.WrapText = true;
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cells.Value = d.Description;
                        column++;
                    }
                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex + commCount - 1, column])
                    {
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cells.Value = d.Date.ToLocalTime().ToString();
                        column++;
                    }
                    using (var cells = workSheet.Cells[rowIndex, column])
                    {
                        cells.LoadFromCollection(d.Comments.Select(c => c.Comment));
                        column += 1;
                    }
                    rowIndex += commCount;
                }
                rowIndex += 1;

            }
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            workSheet.Column(1).Width = 20.0;
            workSheet.Column(2).Width = 60.0;
            workSheet.Column(3).Width = 20.0;
        }

        private void ExportSurveys(ExcelPackage package, List<Survey> surveys)
        {
            var workSheet = package.Workbook.Worksheets.Add("Survey answers");
            var rowIndex = 1;
            var width = 4;
            var surveysByProject = surveys.GroupBy(r => r.ProjectName).ToList();
            var questions = surveys.FirstOrDefault()?.Questions;
            foreach (var userAnswer in surveysByProject)
            {

                rowIndex++;
                var column = 1;
                using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, 2])
                {
                    cells.LoadFromText("User,Date");
                    column += 2;
                }
                foreach (var multiselectQuestion in questions.MultiselectQuestions)
                {
                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex, column + 1])
                    {
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Value = multiselectQuestion.QuestionText;
                    }
                    column += 2;
                }
                foreach (var textQuestion in questions.TextQuestions)
                {
                    using (var cells = workSheet.Cells[rowIndex, column])
                    {
                        cells.Value = textQuestion.QuestionText;
                    }
                    column += 1;
                }
                width = column - 1;
                using (var cells = workSheet.Cells[rowIndex - 1, 1, rowIndex - 1, width])
                {
                    cells.Merge = true;
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    cells.Value = userAnswer.Key;
                }

                rowIndex++;

                foreach (var d in userAnswer)
                {
                    var rowAddCount = d.Answers.MultiselectAnswers.FirstOrDefault(m => m.Answer != null).Answer.Choices.Count;
                    if (rowAddCount == 0)
                        rowAddCount = 1;
                    column = 1;
                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex + rowAddCount - 1, column])
                    {
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cells.Value = d.UserName;
                        column++;
                    }
                    using (var cells = workSheet.Cells[rowIndex, column, rowIndex + rowAddCount - 1, column])
                    {
                        cells.Merge = true;
                        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cells.Value = d.AnswerDate.ToLocalTime().ToString();
                        column++;
                    }
                    foreach (var multiselectAnswer in d.Answers.MultiselectAnswers)
                    {
                        var choices = multiselectAnswer.Answer.Choices.Select(c => new { c.Key, c.Value });
                        using (var cells = workSheet.Cells[rowIndex, column])
                        {
                            cells.LoadFromCollection(choices);
                        }
                        column += 2;
                    }
                    foreach (var textAnswer in d.Answers.TextAnswers)
                    {
                        using (var cells = workSheet.Cells[rowIndex, column, rowIndex + rowAddCount - 1, column])
                        {
                            cells.Merge = true;
                            cells.Value = textAnswer.Text;
                        }
                        column++;
                    }
                    rowIndex += rowAddCount;
                }
                rowIndex++;
            }
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
            workSheet.Column(1).Width = 20.0;
            workSheet.Column(2).Width = 20.0;
        }
    }
}
