using AutoMapper;
using GitlabInfo.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models.ExcelExport;

namespace GitlabInfo.Models.Profiles
{
    public class ExcelExportProfiles : Profile
    {
        public ExcelExportProfiles()
        {
            CreateMap<ReportedTimeModel, ExcelExport.ReportedTime>()
                .ForMember(o => o.UserName, m => m.MapFrom(r => r.User.Name))
                .ForMember(o => o.Date, m => m.MapFrom(r => r.Date))
                .ForMember(o => o.TimeInHours, m => m.MapFrom(r => r.TimeInHours))
                .ForMember(o => o.Description, m => m.MapFrom(r => r.Description))
                .ForMember(o => o.IssueId, m => m.MapFrom(r => r.IssueId))
                .ForMember(o => o.ProjectName, m => m.MapFrom(r => r.Project.Name))
                .ForMember(o => o.ReportedDate, m => m.MapFrom(r => r.ReportedDate));

            CreateMap<EngagementPointsModel, ExcelExport.EngagementPoints>()
                .ForMember(o => o.AwardingUserName, m => m.MapFrom(r => r.AwardingUser.Name))
                .ForMember(o => o.ReceivingUserName, m => m.MapFrom(r => r.ReceivingUser.Name))
                .ForMember(o => o.Points, m => m.MapFrom(r => r.Points))
                .ForMember(o => o.ReceivingDate, m => m.MapFrom(r => r.ReceivingDate))
                .ForMember(o => o.ProjectName, m => m.MapFrom(r => r.Project.Name))
                .ForMember(o => o.Bonus, m => m.MapFrom(r => r.Bonus))
                .ForMember(o => o.Comment, m => m.MapFrom(r => r.Comment));


            CreateMap<WorkDescriptionModel, ExcelExport.WorkDescription>()
                .ForMember(o => o.UserName, m => m.MapFrom(r => r.User.Name))
                .ForMember(o => o.ProjectName, m => m.MapFrom(r => r.Project.Name))
                .ForMember(o => o.Description, m => m.MapFrom(r => r.Description))
                .ForMember(o => o.Date, m => m.MapFrom(r => r.Date))
                .ForMember(o => o.Comments, m => m.MapFrom(r => r.Comments.ToList()));

            CreateMap<WorkDescriptionCommentModel, ExcelExport.WorkDescriptionComment>()
                .ForMember(o => o.CommenterUserName, m => m.MapFrom(r => r.Commenter.Name))
                .ForMember(o => o.Comment, m => m.MapFrom(r => r.Comment));
        }
    }
}
