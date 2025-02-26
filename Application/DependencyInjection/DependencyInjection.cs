using Application.Mapping;
using Application.Services;
using Application.Validations;
using Application.Validations.FluentValidations.Report;
using Application.Validations.FluentValidations.Task;
using Application.Validations.FluentValidations.TimeSheet;
using Domain.Dto.Report;
using Domain.Dto.Task;
using Domain.Dto.TimeSheet;
using Domain.Interface.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interface.Validations;
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
            services.AddScoped<IReportValidator, ReportValidator>();
            services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
            services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();

            services.AddScoped<ITaskEntityValidator, TaskEntityValidator>();
            services.AddScoped<IValidator<CreateTaskEntityDto>, CreateTaskEntityValidator>();
            services.AddScoped<IValidator<UpdateTaskEntityDto>, UpdateTaskEntityValidator>();

            services.AddScoped<ITimeSheetValidator, TimeSheetValidator>();
            services.AddScoped<IValidator<CreateTimeSheetDto>, CreateTimeSheetValidator>();
            services.AddScoped<IValidator<UpdateTimeSheetDto>, UpdateTimeSheetValidator>();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ITaskEntityService, TaskEntityService>();
            services.AddScoped<ITimeSheetService, TimeSheetService>();
        }
    }
}
