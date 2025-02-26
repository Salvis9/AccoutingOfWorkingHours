using Application.Resources;
using AutoMapper;
using Domain.Dto.Task;
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
    public class TaskEntityService : ITaskEntityService
    {
        private readonly IBaseRepository<TaskEntity> _taskEntityRepository;
        private readonly IBaseRepository<Report> _reportRepository;
        private readonly ITaskEntityValidator _taskEntityValidator;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;

        public TaskEntityService(IBaseRepository<TaskEntity> taskEntityRepository, Serilog.ILogger logger,
            IBaseRepository<Report> reportRepository, ITaskEntityValidator taskEntityValidator, IMapper mapper)
        {
            _taskEntityRepository = taskEntityRepository;
            _logger = logger;
            _reportRepository = reportRepository;
            _taskEntityValidator = taskEntityValidator;
            _mapper = mapper;
        }


        public async Task<CollectionResult<TaskEntityDto>> GetTasksEntityAsync(long reportId)
        {
            TaskEntityDto[] tasksEntity;
            try
            {
                tasksEntity = await _taskEntityRepository.GetAll()
                    .Where(x => x.ReportId == reportId)
                    .Select(x => new TaskEntityDto(x.Id, x.Name, x.IsActive, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<TaskEntityDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }

            if (!tasksEntity.Any())
            {
                _logger.Warning(ErrorMessage.TasksEntityNotFound, tasksEntity.Length);
                return new CollectionResult<TaskEntityDto>
                {
                    ErrorMessage = ErrorMessage.TasksEntityNotFound,
                    ErrorCode = (int)ErrorCodes.TasksEntityNotFound
                };
            }

            return new CollectionResult<TaskEntityDto>()
            {
                Data = tasksEntity,
                Count = tasksEntity.Length
            };
        }

        public Task<BaseResult<TaskEntityDto>> GetTaskEntityByIdAsync(long id)
        {
            TaskEntityDto? taskEntity;
            try
            {
                //report = await _reportRepository.GetAll()
                // .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                // .FirstOrDefaultAsync(x => x.Id == id);
                taskEntity = _taskEntityRepository.GetAll()
                    .AsEnumerable()
                    .Select(x => new TaskEntityDto(x.Id, x.Name, x.IsActive, x.CreatedAt.ToLongDateString()))
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return Task.FromResult(new BaseResult<TaskEntityDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                });
            }

            if (taskEntity == null)
            {
                _logger.Warning($"Задача с {id} не найдена", id);
                return Task.FromResult(new BaseResult<TaskEntityDto>
                {
                    ErrorMessage = ErrorMessage.TaskEntityNotFound,
                    ErrorCode = (int)ErrorCodes.TaskEntityNotFound
                });
            }

            return Task.FromResult(new BaseResult<TaskEntityDto>()
            {
                Data = taskEntity
            });
        }

        public async Task<BaseResult<TaskEntityDto>> CreateTaskEntityAsync(CreateTaskEntityDto dto)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.ReportId);
                var taskEntity = await _taskEntityRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
                var result = _taskEntityValidator.CreateValidator(taskEntity, report);
                if (!result.IsSuccess)
                {
                    return new BaseResult<TaskEntityDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }
                taskEntity = new TaskEntity()
                {
                    Name = dto.Name,
                    ReportId = report.Id
                };
                await _taskEntityRepository.CreateAsync(taskEntity);
                return new BaseResult<TaskEntityDto>()
                {
                    Data = _mapper.Map<TaskEntityDto>(taskEntity)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TaskEntityDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }

        public async Task<BaseResult<TaskEntityDto>> DeleteTaskEntityAsync(long id)
        {
            try
            {
                var taskEntity = await _taskEntityRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _taskEntityValidator.ValidateOnNull(taskEntity);
                if (!result.IsSuccess)
                {
                    return new BaseResult<TaskEntityDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }
                await _taskEntityRepository.RemoveAsync(taskEntity);
                return new BaseResult<TaskEntityDto>()
                {
                    Data = _mapper.Map<TaskEntityDto>(taskEntity)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TaskEntityDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }

        public async Task<BaseResult<TaskEntityDto>> UpdateTaskEntityAsync(UpdateTaskEntityDto dto)
        {
            try
            {
                var taskEntity = await _taskEntityRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
                var result = _taskEntityValidator.ValidateOnNull(taskEntity);
                if (!result.IsSuccess)
                {
                    return new BaseResult<TaskEntityDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }

                taskEntity.Name = dto.Name;
                taskEntity.IsActive = dto.IsActive;

                await _taskEntityRepository.UpdateAsync(taskEntity);
                return new BaseResult<TaskEntityDto>()
                {
                    Data = _mapper.Map<TaskEntityDto>(taskEntity)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TaskEntityDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.IternalServerError
                };
            }
        }
    }
}
