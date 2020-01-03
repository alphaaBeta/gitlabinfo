using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.Survey
{
    public interface IQuestion
    {
        int Id { get; set; }
        string QuestionText { get; set; }
        string Type { get; set; }
    }
}
