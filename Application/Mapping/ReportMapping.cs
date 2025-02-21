using AutoMapper;
using Domain.Dto.Report;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class ReportMapping : Profile
    {
        CreateMap<Report, ReportDto>().ReverseMap();
    }
}
