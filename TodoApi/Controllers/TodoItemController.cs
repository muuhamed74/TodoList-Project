using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.DTOs;
using Todo.Domain.Entities;
using Todo.Serv;
using Todo.Serv.Interfaces;
using TodoApi.DTOs;

namespace TodoApi.Controllers
{
   
    //[Authorize]
    public class TodoItemController : BaseController
    {
        private readonly ITodoItemService _todoService;

        public TodoItemController(ITodoItemService todoService)
        {
            _todoService = todoService;
        }

        //Get all items for user and admin with sorting
        [HttpGet]
        public async Task<ActionResult> GetSorted([FromQuery] string? sortField, [FromQuery] string? sortDirection,
            [FromQuery] string? titleFilter, [FromQuery] bool? isCompletedFilter,[FromQuery] DateTime? dueDateBefore)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            var result = await _todoService.GetSortedItemsAsync(email, roles, sortField, sortDirection, titleFilter, isCompletedFilter, dueDateBefore);

            return Ok(result);
        }



        //Get item by ID (only if owned by user)
        [HttpGet("{id}")]
         public async Task<ActionResult<TodoItemDto>> GetById(int id)
        {
            var item = await _todoService.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        //Create new item

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<TodoItemDto>> Create([FromBody] CreateTodoItemDto dto)
        {
            var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;


            if (string.IsNullOrEmpty(email))
                return Unauthorized("User email not found in claims");

          
            var result = await _todoService.CreateAsync(dto, email);

            return Ok(result);
        }

        //Update item
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoItemDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var result = await _todoService.UpdateAsync(dto);
            if (result == null)
                return NotFound();

            //return NoContent(); // 204
            return Ok(result);
        }

        //Delete item
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _todoService.DeleteAsync(id);
            return success ? Ok("The list has been deleted") : BadRequest("Delete failed.");
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Controller is working");
        }
    }
}

