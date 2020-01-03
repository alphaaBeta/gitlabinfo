using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("SurveyAnswers")]
    public class SurveyAnswerModel
    {
        [Key]
        public int SurveyAnswerId { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public UserModel User { get; set; }
        [ForeignKey(nameof(Survey))]
        public int SurveyId { get; set; }
        public SurveyModel Survey { get; set; }
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public ProjectModel Project { get; set; }
        [Column("Answer")]
        public string AnswerString { get; set; }
    }
}
