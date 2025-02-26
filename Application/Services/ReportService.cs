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
using Domain.Dto.Report;
using Domain.Interface.Validations;
using AutoMapper;

namespace Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IBaseRepository<Report> _reportRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IReportValidator _reportValidator;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;

        public ReportService(IBaseRepository<Report> reportRepository, Serilog.ILogger logger,
            IBaseRepository<User> userRepository, IReportValidator reportValidator, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _reportValidator = reportValidator;
        }


        public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
        {
            ReportDto[] reports;
            try
            {
                reports = await _reportRepository.GetAll()
                    .Where(x => x.UserId == userId)
                    .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.IsActive, x.CreatedAt.ToLongDateString()))
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
            }

            if (!reports.Any())
            {
                _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
                return new CollectionResult<ReportDto>
                {
                    ErrorMessage = ErrorMessage.ReportsNotFound,
                    ErrorCode = (int)ErrorCodes.ReportsNotFound
                };
            }

            return new CollectionResult<ReportDto>()
            {
                Data = reports,
                Count = reports.Length
            };
        }
        public Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
        {
            ReportDto? report;
            try
            {
                //report = await _reportRepository.GetAll()
                   // .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                   // .FirstOrDefaultAsync(x => x.Id == id);
                report = _reportRepository.GetAll()
                    .AsEnumerable()
                    .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.IsActive, x.CreatedAt.ToLongDateString()))
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return Task.FromResult(new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                });
            }

            if (report == null)
            {
                _logger.Warning($"Отчёт с {id} не найден", id);
                return Task.FromResult(new BaseResult<ReportDto>
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound
                });
            }

            return Task.FromResult(new BaseResult<ReportDto>()
            {
                Data = report
            });
        }

        public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.UserId);
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
                var result = _reportValidator.CreateValidator(report, user);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }
                report = new Report()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    UserId = user.Id
                };
                await _reportRepository.CreateAsync(report);
                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }

        public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _reportValidator.ValidateOnNull(report);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }
                await _reportRepository.RemoveAsync(report);
                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }

        public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
                var result = _reportValidator.ValidateOnNull(report);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }

                report.Name = dto.Name;
                report.Description = dto.Description;
                report.IsActive = dto.IsActive;

                await _reportRepository.UpdateAsync(report);
                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }
    }
}
