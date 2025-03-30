using Application.Services;
using Domain.Dto.TimeSheet;
using Domain.Interface.Services;
using Domain.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// Сделать метод, когда он активируется (кнопка завершить рабочий день), то происходит подсчет времени проводок и вылазит смайл

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TimeSheetController : ControllerBase
    {
        private readonly ITimeSheetService _timeSheetService;

        public TimeSheetController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }

        [HttpGet("timeSheetsAllTime/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TimeSheetDto>>> GetAllTimeSheets(long userId)
        {
            var response = await _timeSheetService.GetAllTimeSheetsAsync(userId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("timeSheetsMonth/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TimeSheetDto>>> GetMonthTimeSheets(long userId, int year, int month)
        {
            var response = await _timeSheetService.GetMonthTimeSheetsAsync(userId, year, month);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("timeSheetsDay/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TimeSheetDto>>> GetDayTimeSheets(long userId, DateTime date)
        {
            var response = await _timeSheetService.GetDayTimeSheetsAsync(userId, date);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("timeSheetsByTaskId/{taskEntityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TimeSheetDto>>> GetTimeSheetsByTaskId(long taskEntityId)
        {
            var response = await _timeSheetService.GetTimeSheetsAsync(taskEntityId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TimeSheetDto>>> GetTimeSheetById(long id)
        {
            var response = await _timeSheetService.GetTimeSheetByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TimeSheetDto>>> Delete(long id)
        {
            var response = await _timeSheetService.DeleteTimeSheetAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TimeSheetDto>>> Create([FromBody] CreateTimeSheetDto dto)
        {
            var response = await _timeSheetService.CreateTimeSheetAsync(dto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TimeSheetDto>>> Update([FromBody] UpdateTimeSheetDto dto)
        {
            var response = await _timeSheetService.UpdateTimeSheetAsync(dto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
