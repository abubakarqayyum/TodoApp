using CommonService.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todo.BusinessLogic.Dtos.Todo;
using Todo.BusinessLogic.IServices;
using Todo.Entities;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly IGenericService<Todo.Entities.Entity.Todo, TodoReadDto, AddUpdateTodoDto> _todoService;

        public TodosController(IGenericService<Todo.Entities.Entity.Todo, TodoReadDto, AddUpdateTodoDto> todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("GetAll")]
        public async Task<ActionResult<OperationResponse>> GetAllAsync([FromBody] PaginationDto search)
        {
            var (items, total) = await _todoService.GetListAsync(search);
            var result = new { Items = items, TotalCount = total };
            return Ok(new OperationResponse(true, 200, "Todos retrieved successfully", result));
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse>> GetByIdAsync([FromQuery]long id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            return Ok(new OperationResponse(true, 200, "Todo retrieved successfully", todo));
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse>> CreateAsync([FromBody] AddUpdateTodoDto dto,[FromServices] IValidator<AddUpdateTodoDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(new OperationResponse(false, 400, "Validation failed",
                    validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })));

            var createdTodo = await _todoService.AddAsync(dto);
            return StatusCode(201, new OperationResponse(true, 201, "Todo created successfully", createdTodo));
        }
    
        [HttpPut]
        public async Task<ActionResult<OperationResponse>> UpdateAsync([FromQuery]long id,[FromBody] AddUpdateTodoDto dto,[FromServices] IValidator<AddUpdateTodoDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(new OperationResponse(false, 400, "Validation failed",
                    validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })));

            var updatedTodo = await _todoService.UpdateAsync(id, dto);
            return Ok(new OperationResponse(true, 200, "Todo updated successfully", updatedTodo));
        }
        
        [HttpDelete]
        public async Task<ActionResult<OperationResponse>> DeleteAsync([FromQuery] long id)
        {
            await _todoService.DeleteAsync(id);
            return Ok(new OperationResponse(true, 200, "Todo deleted successfully", new { Id = id }));
        }
    }
}
