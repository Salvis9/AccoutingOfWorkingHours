using AutoMapper;
using Domain.Dto.Task;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class TaskEntityMapping : Profile
    {
        public TaskEntityMapping()
        {
            CreateMap<TaskEntity, TaskEntityDto>()
                .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
                .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name))
                .ForCtorParam(ctorParamName: "IsActive", m => m.MapFrom(s => s.IsActive))
                .ForCtorParam(ctorParamName: "CreatedAt", m => m.MapFrom(s => s.CreatedAt))
                .ReverseMap();
        }
    }
}
