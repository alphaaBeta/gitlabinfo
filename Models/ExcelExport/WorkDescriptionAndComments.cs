using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ExcelExport
{
    public class WorkDescription
    {
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public List<WorkDescriptionComment> Comments { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }

    public class WorkDescriptionComment
    {
        public string CommenterUserName { get; set; }
        public string Comment { get; set; }
    }
}
