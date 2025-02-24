using Application.Mapping;
using Application.Services;
using Application.Validations;
using Application.Validations.FluentValidations.Report;
using Domain.Dto.Report;
using Domain.Interface.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ReportMapping));

            InitService(services);
        }

        private static void InitService(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
            services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();

            services.AddScoped<IReportService, ReportService>();

        }
    }
}
