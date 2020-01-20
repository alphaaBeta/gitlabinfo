using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.Survey
{
    public interface IAnswer
    {
        int QuestionId { get; set; }
    }
}
