using GitlabInfo.Models.ExcelExport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Code.Repositories.Interfaces
{
    public interface IExcelExportRepository
    {
        MemoryStream ExportGroupInfo(List<ReportedTime> reportedTimes, List<EngagementPoints> engagementPoints, List<WorkDescription> workDescriptions, List<Survey> surveys);
    }
}
