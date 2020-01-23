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

                //using (var cells = workSheet.Cells[rowIndex, 1, rowIndex, 6])
                //{
                //}

                #region Reported times
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
                #endregion

                #region Engagement points
                workSheet = package.Workbook.Worksheets.Add("Engagement points");
                rowIndex = 1;
                width = 6;
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
                #endregion

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}
