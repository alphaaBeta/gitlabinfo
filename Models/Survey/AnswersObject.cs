using GitlabInfo.Models.Survey;
using System.Collections.Generic;

namespace GitlabInfo.Models
{
    public class AnswersObject
    {
        public List<MultiselectAnswer> MultiselectAnswers { get; set; }
        public List<TextAnswer> TextAnswers { get; set; }

    }
}
