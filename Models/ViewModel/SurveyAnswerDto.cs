using GitlabInfo.Models.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class SurveyAnswerDto
    {
        public int SurveyId { get; set; }
        public int ProjectId { get; set; }
        public List<MultiselectAnswer> MultiselectAnswers { get; set; }
        public List<TextAnswer> TextAnswers { get; set; }
    }
}
