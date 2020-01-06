using GitlabInfo.Models.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class SurveyDto
    {
        public int? SurveyId { get; set; }
        public string Name { get; set; }
        public List<MultiselectQuestion> MultiselectQuestions { get; set; }
        public List<TextQuestion> TextQuestions { get; set; }
    }
}
