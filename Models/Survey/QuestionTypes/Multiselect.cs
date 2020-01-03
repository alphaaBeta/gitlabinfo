using GitlabInfo.Code;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.Survey
{
    [JsonObject]
    public class MultiselectQuestion : IQuestion
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty("text")]
        public string QuestionText { get; set; }
        [JsonProperty]
        public string Type { get; set; }
        [JsonProperty]
        public MultiselectQuestionOptions Options { get; set; }

        public MultiselectQuestion(string question, List<string> options)
        {
            Type = Consts.QuestionTypes.Multiselect;
            QuestionText = question;
            Options = new MultiselectQuestionOptions() { Choices = options };
        }
        public MultiselectQuestion()
        {

        }
    }

    [JsonObject]
    public class MultiselectQuestionOptions
    {
        [JsonProperty]
        public List<string> Choices { get; set; }
    }



    public class MultiselectAnswer : IAnswer
    {
        public int QuestionId { get; set; }
        public MultiselectAnswerAnswer Answer { get; set; }
    }

    [JsonObject]
    public class MultiselectAnswerAnswer
    {
        [JsonProperty]
        public List<MultiselectAnswerChoice> Choices { get; set; }
    }

    [JsonObject]
    public class MultiselectAnswerChoice
    {
        [JsonProperty]
        public string Key { get; set; }
        [JsonProperty]
        public string Value { get; set; }
    }
}
