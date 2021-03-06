﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("Surveys")]
    public class SurveyModel
    {
        [Key]
        public int SurveyId { get; set; }
        [Column("Survey")]
        public string SurveyString { get; set; }
        [InverseProperty("Survey")]
        public virtual ICollection<GroupOptionsModel> GroupOptionsList { get; set; }
    }
}
