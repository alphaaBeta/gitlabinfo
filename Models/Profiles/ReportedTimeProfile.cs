using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GitlabInfo.Models.EFModels;
using GitlabInfo.Models.ViewModel;

namespace GitlabInfo.Models.Profiles
{
    public class ReportedTimeProfile : Profile
    {
        public ReportedTimeProfile()
        {
            CreateMap<ReportedTime, ReportedTimeDto>()
                .ForMember(o => o.UserId, m => m.MapFrom(r => r.User.Id))
                .ForMember(o => o.UserName, m => m.MapFrom(r => r.User.Name))
                .ForMember(o => o.ProjectId, m => m.MapFrom(r => r.Project.Id));

            CreateMap<ReportedTimeDto, ReportedTime>()
                .ForPath(o => o.User.Id, m => m.MapFrom(r => r.UserId))
                .ForPath(o => o.Project.Id, m => m.MapFrom(r => r.ProjectId));

            CreateMap<ReportedTime, ReportedTimeModel>()
                .ForPath(o => o.User.Id, m => m.MapFrom(r => r.User.Id))
                .ForPath(o => o.Project.Id, m => m.MapFrom(r => r.Project.Id));
        }
    }
}
