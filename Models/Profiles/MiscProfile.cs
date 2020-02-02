using AutoMapper;
using GitlabInfo.Models.EFModels;
using GitlabInfo.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.Profiles
{
    public class MiscProfile : Profile
    {
        public MiscProfile()
        {
            CreateMap<EngagementPoints, EngagementPointsGetDto>();

            CreateMap<GroupOptionsModel, GroupOptions>()
                .ForMember(o => o.EngagementPointsEnabled, m => m.MapFrom(r => r.EngagementPointsEnabled))
                .ForMember(o => o.ReportTimeEnabled, m => m.MapFrom(r => r.ReportTimeEnabled))
                .ForMember(o => o.SurveyEnabled, m => m.MapFrom(r => r.SurveyEnabled))
                .ForMember(o => o.WorkDescriptionEnabled, m => m.MapFrom(r => r.WorkDescriptionEnabled))
                .ForMember(o => o.HasNewData, m => m.MapFrom(r => r.HasNewData));
            CreateMap<GroupOptions, GroupOptionsModel>()
                .ForMember(o => o.EngagementPointsEnabled, m => m.MapFrom(r => r.EngagementPointsEnabled))
                .ForMember(o => o.ReportTimeEnabled, m => m.MapFrom(r => r.ReportTimeEnabled))
                .ForMember(o => o.SurveyEnabled, m => m.MapFrom(r => r.SurveyEnabled))
                .ForMember(o => o.WorkDescriptionEnabled, m => m.MapFrom(r => r.WorkDescriptionEnabled));
        }
    }
}
