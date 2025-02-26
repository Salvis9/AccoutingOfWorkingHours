﻿using Domain.Dto.TimeSheet;
using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface ITimeSheetService
    {
        // Получение всех проводок задачи
        Task<CollectionResult<TimeSheetDto>> GetTimeSheetsAsync(long taskEntityId);

        // Получение проводки по ID
        Task<BaseResult<TimeSheetDto>> GetTimeSheetByIdAsync(long id);

        // Создание проводки с базовыми параметрами
        Task<BaseResult<TimeSheetDto>> CreateTimeSheetAsync(CreateTimeSheetDto dto);

        // Удаление проводки по ID
        Task<BaseResult<TimeSheetDto>> DeleteTimeSheetAsync(long id);

        // Обновление проводки
        Task<BaseResult<TimeSheetDto>> UpdateTimeSheetAsync(UpdateTimeSheetDto dto);
    }
}
