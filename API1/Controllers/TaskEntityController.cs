using Domain.Dto.Task;
using Domain.Interface.Services;
using Domain.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaskEntityController : ControllerBase
    {
        private readonly ITaskEntityService _taskEntityService;

        public TaskEntityController(ITaskEntityService taskEntityService)
        {
            _taskEntityService = taskEntityService;
        }

        [HttpGet("tasksEntity/{reportId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TaskEntityDto>>> GetReportTasks(long reportId)
        {
            var response = await _taskEntityService.GetTasksEntityAsync(reportId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TaskEntityDto>>> GetReport(long id)
        {
            var response = await _taskEntityService.GetTaskEntityByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TaskEntityDto>>> Delete(long id)
        {
            var response = await _taskEntityService.DeleteTaskEntityAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TaskEntityDto>>> Create([FromBody] CreateTaskEntityDto dto)
        {
            var response = await _taskEntityService.CreateTaskEntityAsync(dto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TaskEntityDto>>> Update([FromBody] UpdateTaskEntityDto dto)
        {
            var response = await _taskEntityService.UpdateTaskEntityAsync(dto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
