using AutoMapper;
using Domain.Dto.TimeSheet;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class TimeSheetMapping : Profile
    {
        public TimeSheetMapping()
        {
            CreateMap<TimeSheet, TimeSheetDto>()
                .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
                .ForCtorParam(ctorParamName: "Hours", m => m.MapFrom(s => s.Hours))
                .ForCtorParam(ctorParamName: "Description", m => m.MapFrom(s => s.Description))
                .ForCtorParam(ctorParamName: "CreatedAt", m => m.MapFrom(s => s.CreatedAt))
                .ReverseMap();
        }
    }
}
