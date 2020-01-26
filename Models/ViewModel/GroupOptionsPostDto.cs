using System.Text.Json.Serialization;

namespace GitlabInfo.Models.ViewModel
{
    public class GroupOptionsPostDto
    {
        [JsonPropertyName("groupId")]
        public int GroupId { get; set; }
        [JsonPropertyName("surveyString")]
        public string SurveyString { get; set; }
        [JsonPropertyName("surveyId")]
        public int? SurveyId { get; set; }
        [JsonPropertyName("reportTimeEnabled")]
        public bool ReportTimeEnabled { get; set; }
        [JsonPropertyName("engagementPointsEnabled")]
        public bool EngagementPointsEnabled { get; set; }
        [JsonPropertyName("workDescriptionEnabled")]
        public bool WorkDescriptionEnabled { get; set; }
        [JsonPropertyName("workDescriptionCommentsEnabled")]
        public bool WorkDescriptionCommentsEnabled { get; set; }
        [JsonPropertyName("surveyEnabled")]
        public bool SurveyEnabled { get; set; }
        [JsonPropertyName("allowsProjectCreation")]
        public bool AllowsProjectCreation { get; set; }
    }
}
