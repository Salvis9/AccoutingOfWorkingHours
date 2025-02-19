using Domain.Dto;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Result;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Application.Resources;
using Domain.Enum;

namespace Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IBaseRepository<Report> _reportRepository;
        private readonly Serilog.ILogger _logger;

        public ReportService(IBaseRepository<Report> reportRepository, Serilog.ILogger logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
        }
        public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
        {
            ReportDto[] reports;
            try
            {
                reports = await _reportRepository.GetAll()
                    .Where(x => x.UserId == userId)
                    .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };

                if (!reports.Any())
                {
                    _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
                    return new CollectionResult<ReportDto>
                    {
                        ErrorMessage = ErrorMessage.ReportsNotFound,
                        ErrorCode = (int)ErrorCodes.ReportsNotFound
                    };
                }
            }

            return new CollectionResult<ReportDto>()
            {
                Data = reports,
                Count = reports.Length
            };
        }
    }
}
