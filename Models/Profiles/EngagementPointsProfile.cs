using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GitlabInfo.Models.ViewModel;

namespace GitlabInfo.Models.Profiles
{
    public class EngagementPointsProfile : Profile
    {
        public EngagementPointsProfile()
        {
            CreateMap<EngagementPoints, EngagementPointsGetDto>();
        }
    }
}
