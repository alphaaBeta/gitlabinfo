using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GitlabInfo.Models.EFModels
{
    [Table("JoinRequests")]
    public class JoinRequestModel
    {
        [Key]
        public int Id { get; set; }
        public UserModel Requestee { get; set; }
        public GroupModel RequestedGroup { get; set; }
    }
}
