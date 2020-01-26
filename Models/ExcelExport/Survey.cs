using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ExcelExport
{
    public class Survey
    {
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public AnswersObject Answers { get; set; }
        public SurveyObject Questions { get; set; }
        public DateTime AnswerDate { get; set; }
    }
}
