using Api.Data.Interface;
using Api.Data.Model;
using Api.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("todo/[controller]")]
    public class ToDoController : ControllerBase
    {
        private IToDo todo = new ToDoRepository();

        [HttpGet]
        public ActionResult<IEnumerable<ToDo>> GetAllToDos()
        {
            return todo.GetAllToDos();
        }

        [HttpPost]
        public ActionResult<ToDo> CreateToDo(ToDo item)
        {
            todo.CreateToDo(item);
            return Ok();
        }

        [HttpPatch]
        public ActionResult<ToDo> UpdateToDo(ToDo item)
        {
            todo.UpdateToDo(item);
            return Ok();
        }

        [HttpDelete]
        public ActionResult<ToDo> DeleteToDo(int Id)
        {
            todo.DeleteToDo(Id);
            return Ok();
        }
    }
}
