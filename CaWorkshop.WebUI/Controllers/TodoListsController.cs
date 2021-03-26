using System.Threading.Tasks;
using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;
using CaWorkshop.Application.TodoLists.Commands.DeleteTodoList;
using CaWorkshop.Application.TodoLists.Commands.UpdateTodoList;
using CaWorkshop.Application.TodoLists.Queries.GetTodoLists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaWorkshop.WebUI.Controllers
{
    public class TodoListsController : ApiControllerBase
    {
        // GET: api/TodoLists
        [HttpGet]
        public async Task<ActionResult<TodosVm>> GetTodoLists()
        {
            return await Mediator.Send(new GetTodoListsQuery());
        }

        // POST: api/TodoLists
        [HttpPost]
        public async Task<ActionResult<int>> PostTodoList(
            CreateTodoListCommand command)
        {
            return await Mediator.Send(command);
        }

        // PUT: api/TodoLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoList(int id,
            UpdateTodoListCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/TodoLists/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            await Mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }
    }
}
