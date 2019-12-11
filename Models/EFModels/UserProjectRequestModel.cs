using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("UserProjectRequests")]
    public class UserProjectRequestModel
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int ProjectRequestId { get; set; }
        public ProjectRequestModel ProjectRequest { get; set; }
        public bool IsRequestee { get; set; }
    }
}
