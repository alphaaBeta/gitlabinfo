using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GitlabInfo.Models.EFModels
{
    public class JoinRequestModel
    {
        [Key]
        public int Id { get; set; }
        public UserModel Requestee { get; set; }
        public GroupModel RequestedGroup { get; set; }
    }
}
