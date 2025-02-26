using Domain.Dto.Task;
using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface ITaskEntityService
    {
        // Получение всех задач проекта
        Task<CollectionResult<TaskEntityDto>> GetTasksEntityAsync(long reportId);

        // Получение задачи по ID
        Task<BaseResult<TaskEntityDto>> GetTaskEntityByIdAsync(long id);

        // Создание задачи с базовыми параметрами
        Task<BaseResult<TaskEntityDto>> CreateTaskEntityAsync(CreateTaskEntityDto dto);

        // Удаление задачи по ID
        Task<BaseResult<TaskEntityDto>> DeleteTaskEntityAsync(long id);

        // Обновление задачи
        Task<BaseResult<TaskEntityDto>> UpdateTaskEntityAsync(UpdateTaskEntityDto dto);
    }
}
