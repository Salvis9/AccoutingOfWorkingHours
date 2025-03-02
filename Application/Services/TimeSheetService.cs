using Application.Resources;
using AutoMapper;
using Domain.Dto.TimeSheet;
using Domain.Entity;
using Domain.Enum;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Interface.Validations;
using Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly IBaseRepository<TimeSheet> _timeSheetRepository;
        private readonly IBaseRepository<TaskEntity> _taskEntityRepository;
        private readonly ITimeSheetValidator _timeSheetValidator;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;

        public TimeSheetService(IBaseRepository<TimeSheet> timeSheetRepository, Serilog.ILogger logger,
            IBaseRepository<TaskEntity> taskEntityRepository, ITimeSheetValidator timeSheetValidator, IMapper mapper)
        {
            _timeSheetRepository = timeSheetRepository;
            _logger = logger;
            _taskEntityRepository = taskEntityRepository;
            _timeSheetValidator = timeSheetValidator;
            _mapper = mapper;
        }

        public async Task<CollectionResult<TimeSheetDto>> GetAllTimeSheetsAsync(long userId)
        {
            TimeSheetDto[] timeSheets;
            try
            {
                timeSheets = await _timeSheetRepository.GetAll()
                    .Where(x => x.UserId == userId)
                    .Select(x => new TimeSheetDto(x.Id, x.Hours, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<TimeSheetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }

            if (!timeSheets.Any())
            {
                _logger.Warning(ErrorMessage.TimeSheetsNotFound, timeSheets.Length);
                return new CollectionResult<TimeSheetDto>
                {
                    ErrorMessage = ErrorMessage.TimeSheetsNotFound,
                    ErrorCode = (int)ErrorCodes.TimeSheetsNotFound
                };
            }

            return new CollectionResult<TimeSheetDto>()
            {
                Data = timeSheets,
                Count = timeSheets.Length
            };
        }

        public async Task<CollectionResult<TimeSheetDto>> GetMonthTimeSheetsAsync(long userId, int year, int month)
        {
            TimeSheetDto[] timeSheets;
            try
            {
                timeSheets = await _timeSheetRepository.GetAll()
                    .Where(x => x.UserId == userId && x.CreatedAt.Year == year && x.CreatedAt.Month == month)
                    .Select(x => new TimeSheetDto(x.Id, x.Hours, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<TimeSheetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }

            if (!timeSheets.Any())
            {
                _logger.Warning(ErrorMessage.TimeSheetsNotFound, timeSheets.Length);
                return new CollectionResult<TimeSheetDto>
                {
                    ErrorMessage = ErrorMessage.TimeSheetsNotFound,
                    ErrorCode = (int)ErrorCodes.TimeSheetsNotFound
                };
            }

            return new CollectionResult<TimeSheetDto>()
            {
                Data = timeSheets,
                Count = timeSheets.Length
            };
        }

        public async Task<CollectionResult<TimeSheetDto>> GetDayTimeSheetsAsync(long userId, DateTime date)
        {
            TimeSheetDto[] timeSheets;
            try
            {
                // временно, т.к. PostgreSql возвращает дату с UTC. В будущем исправить поля в СУБД
                // selectedDate уменьшает время на 3 часа и получается не 27 число, а 26
                timeSheets = await _timeSheetRepository.GetAll()
                    .Where(x => x.UserId == userId && x.CreatedAt.Date == date.Date)
                    .Select(x => new TimeSheetDto(x.Id, x.Hours, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<TimeSheetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }

            if (!timeSheets.Any())
            {
                _logger.Warning(ErrorMessage.TimeSheetsNotFound, timeSheets.Length);
                return new CollectionResult<TimeSheetDto>
                {
                    ErrorMessage = ErrorMessage.TimeSheetsNotFound,
                    ErrorCode = (int)ErrorCodes.TimeSheetsNotFound
                };
            }

            return new CollectionResult<TimeSheetDto>()
            {
                Data = timeSheets,
                Count = timeSheets.Length
            };
        }

        public async Task<CollectionResult<TimeSheetDto>> GetTimeSheetsAsync(long taskEntityId)
        {
            TimeSheetDto[] timeSheets;
            try
            {
                timeSheets = await _timeSheetRepository.GetAll()
                    .Where(x => x.TaskEntityId == taskEntityId)
                    .Select(x => new TimeSheetDto(x.Id, x.Hours, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<TimeSheetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }

            if (!timeSheets.Any())
            {
                _logger.Warning(ErrorMessage.TimeSheetsNotFound, timeSheets.Length);
                return new CollectionResult<TimeSheetDto>
                {
                    ErrorMessage = ErrorMessage.TimeSheetsNotFound,
                    ErrorCode = (int)ErrorCodes.TimeSheetsNotFound
                };
            }

            return new CollectionResult<TimeSheetDto>()
            {
                Data = timeSheets,
                Count = timeSheets.Length
            };
        }

        public Task<BaseResult<TimeSheetDto>> GetTimeSheetByIdAsync(long id)
        {
            TimeSheetDto? timeSheet;
            try
            {
                //report = await _reportRepository.GetAll()
                // .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                // .FirstOrDefaultAsync(x => x.Id == id);
                timeSheet = _timeSheetRepository.GetAll()
                    .AsEnumerable()
                    .Select(x => new TimeSheetDto(x.Id, x.Hours, x.Description, x.CreatedAt.ToLongDateString()))
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return Task.FromResult(new BaseResult<TimeSheetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                });
            }

            if (timeSheet == null)
            {
                _logger.Warning($"Проводка с {id} не найдена", id);
                return Task.FromResult(new BaseResult<TimeSheetDto>
                {
                    ErrorMessage = ErrorMessage.TimeSheetNotFound,
                    ErrorCode = (int)ErrorCodes.TimeSheetNotFound
                });
            }

            return Task.FromResult(new BaseResult<TimeSheetDto>()
            {
                Data = timeSheet
            });
        }

        public async Task<BaseResult<TimeSheetDto>> CreateTimeSheetAsync(CreateTimeSheetDto dto)
        {
            try
            {
                var taskEntity = await _taskEntityRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.TaskEntityId);
                // Что передавать в timeSheet??? Hours??? у других сервисов передавалось Name
                var timeSheet = await _timeSheetRepository.GetAll().FirstOrDefaultAsync(x => x.Description == dto.Description);
                // Подсчет суммы часов всех проводок за один день для пользователя. Далее отправка в валидатор, если >24, то нельзя
                var existingHours = await _timeSheetRepository.GetAll()
                    .Where(x => x.UserId == dto.UserId && x.CreatedAt.Date == DateTime.Now.Date)
                    .SumAsync(x => x.Hours);

                var resultCreate = _timeSheetValidator.CreateValidator(timeSheet, taskEntity);
                if (!resultCreate.IsSuccess)
                {
                    return new BaseResult<TimeSheetDto>()
                    {
                        ErrorMessage = resultCreate.ErrorMessage,
                        ErrorCode = resultCreate.ErrorCode
                    };
                }
                var resultHoursLimit = _timeSheetValidator.HoursLimitPerDayValidator(dto.Hours, existingHours);
                if (!resultHoursLimit.IsSuccess)
                {
                    return new BaseResult<TimeSheetDto>()
                    {
                        ErrorMessage = resultHoursLimit.ErrorMessage,
                        ErrorCode = resultHoursLimit.ErrorCode
                    };
                }
                timeSheet = new TimeSheet()
                {
                    Hours = dto.Hours,
                    Description = dto.Description,
                    UserId = dto.UserId,
                    TaskEntityId = taskEntity.Id
                };
                await _timeSheetRepository.CreateAsync(timeSheet);
                return new BaseResult<TimeSheetDto>()
                {
                    Data = _mapper.Map<TimeSheetDto>(timeSheet)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TimeSheetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }

        public async Task<BaseResult<TimeSheetDto>> DeleteTimeSheetAsync(long id)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _timeSheetValidator.ValidateOnNull(timeSheet);
                if (!result.IsSuccess)
                {
                    return new BaseResult<TimeSheetDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }
                await _timeSheetRepository.RemoveAsync(timeSheet);
                return new BaseResult<TimeSheetDto>()
                {
                    Data = _mapper.Map<TimeSheetDto>(timeSheet)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TimeSheetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }

        public async Task<BaseResult<TimeSheetDto>> UpdateTimeSheetAsync(UpdateTimeSheetDto dto)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
                var existingHours = await _timeSheetRepository.GetAll()
                    .Where(x => x.CreatedAt.Date == DateTime.Now.Date)
                    .SumAsync(x => x.Hours);

                var resultOnNull = _timeSheetValidator.ValidateOnNull(timeSheet);
                if (!resultOnNull.IsSuccess)
                {
                    return new BaseResult<TimeSheetDto>()
                    {
                        ErrorMessage = resultOnNull.ErrorMessage,
                        ErrorCode = resultOnNull.ErrorCode
                    };
                }

                var resultHoursLimit = _timeSheetValidator.HoursLimitPerDayValidator(dto.Hours, existingHours);
                if (!resultHoursLimit.IsSuccess)
                {
                    return new BaseResult<TimeSheetDto>()
                    {
                        ErrorMessage = resultHoursLimit.ErrorMessage,
                        ErrorCode = resultHoursLimit.ErrorCode
                    };
                }

                timeSheet.Hours = dto.Hours;
                timeSheet.Description = dto.Description;

                await _timeSheetRepository.UpdateAsync(timeSheet);
                return new BaseResult<TimeSheetDto>()
                {
                    Data = _mapper.Map<TimeSheetDto>(timeSheet)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TimeSheetDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }
    }
}
