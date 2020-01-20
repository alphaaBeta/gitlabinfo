using GitlabInfo.Models.Survey;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    [JsonObject]
    public class AnswersObject
    {
        [JsonProperty]
        public List<MultiselectAnswer> MultiselectAnswers { get; set; }
        [JsonProperty]
        public List<TextAnswer> TextAnswers { get; set; }

    }
}
