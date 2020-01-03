using GitlabInfo.Models.Survey;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    [JsonObject]
    public class SurveyObject
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public List<MultiselectQuestion> MultiselectQuestions { get; set; }
        [JsonProperty]
        public List<TextQuestion> TextQuestions { get; set; }
    }
}
