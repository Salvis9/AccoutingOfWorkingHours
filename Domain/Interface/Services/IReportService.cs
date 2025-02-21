using Domain.Dto.Report;
using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    // Сервис отвечающий за работу с доменной частью отчета (Report)
    public interface IReportService
    {
        // Получение всех отчетов пользователя
        Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);
        // Получение отчета по идентификатору
        Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);
        // Создание отчета с базовыми параметрами
        Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);
    }
}
