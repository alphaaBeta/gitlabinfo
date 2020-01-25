using GitlabInfo.Code;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitlabInfo.Models.Survey
{
    public class MultiselectQuestion : IQuestion
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("text")]
        public string QuestionText { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("options")]
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

    public class MultiselectQuestionOptions
    {
        [JsonPropertyName("choices")]
        public List<string> Choices { get; set; }
    }



    public class MultiselectAnswer : IAnswer
    {
        public int QuestionId { get; set; }
        public MultiselectAnswerAnswer Answer { get; set; }
    }

    public class MultiselectAnswerAnswer
    {
        public List<MultiselectAnswerChoice> Choices { get; set; }
    }

    public class MultiselectAnswerChoice
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
