using GitlabInfo.Code.Repositories.Interfaces;
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

        public MemoryStream ExportGroupInfo()
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Reported times");
                workSheet.Cells.LoadFromText("chuj,xd");
                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}
