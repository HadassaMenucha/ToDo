using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.interfaces;
using ToDo.Service;

namespace ToDo.Controller
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Person")]
    public class ToDoController : ControllerBase
    {
        private ToDoInterface tdi;

        public ToDoController(ToDoInterface tdi)
        {
            this.tdi=tdi;
        }

        [HttpGet]
        public IEnumerable<myToDo> GetMyToDos()
        {
           return tdi.getToDoByUserId(int.Parse(User.Claims.First(c=>(c.Type=="Id")).Value));
        }

        [HttpGet("{id}")]
        public ActionResult<myToDo> GetTaskByid(int id)
        {
           var td = tdi.getToDoById(id);
           if (td==null)
                return NotFound();
            return td;
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, myToDo td)
        {
           if(id!=td.id)
                return BadRequest("id isn\"t the right todo");
            var res =tdi.Update(td);
            if(!res)
               return NotFound();

            return NoContent();
        }
        [HttpPost]
        public ActionResult AddNewToDo(myToDo td)
        {
            tdi.AddToDo(td);
            return CreatedAtAction(nameof(AddNewToDo), new { id = td.id}, td);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id) 
        {
            var td = tdi.getToDoById(id);
            if (td == null)
                return NotFound();
            tdi.DeleteToDo(id);         
            return NoContent();
        }
    }
}
