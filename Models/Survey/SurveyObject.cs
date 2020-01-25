using GitlabInfo.Models.Survey;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    public class SurveyObject
    {
        public string Name { get; set; }
        public List<MultiselectQuestion> MultiselectQuestions { get; set; }
        public List<TextQuestion> TextQuestions { get; set; }
    }
}
