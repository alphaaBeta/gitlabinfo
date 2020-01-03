using GitlabInfo.Code;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.Survey
{
    public class TextQuestion : IQuestion
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty("text")]
        public string QuestionText { get; set; }
        [JsonProperty]
        public string Type { get; set; }
        [JsonProperty]
        public object Options { get; set; }
        public TextQuestion(string question)
        {
            this.Type = Consts.QuestionTypes.Text;
            this.QuestionText = question;
            this.Options = new object();
        }
        public TextQuestion()
        {

        }
    }

    public class TextAnswer : IAnswer
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
    }
}
