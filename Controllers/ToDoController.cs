using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDo.Service;
using ToDo;
using Todo.interfaces;

namespace ToDo.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private ToDoInterface tdi;

        public ToDoController(ToDoInterface tdi)
        {
            this.tdi=tdi;
        }

        [HttpGet]
        public IEnumerable<myToDo> Get()
        {
           return tdi.GetAllToDos();
        }

        [HttpGet("{id}")]
        public ActionResult<myToDo> Get(int id)
        {
           var td = tdi.getToDoById(id);
           if (td==null)
                return NotFound();
            return td;
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, myToDo td)
        {
           if(id!=td.id)
                return BadRequest("id isn\"t the right todo");
            var res =tdi.Update(td);
            if(!res)
               return NotFound();

            return NoContent();
        }
         [HttpPost]
        public ActionResult Post(myToDo td)
        {
            tdi.AddToDo(td);
            return CreatedAtAction(nameof(Post), new { id = td.id}, td);
        }

          [HttpDelete]
        public ActionResult Delete(int id) 
        {
            var td = tdi.getToDoById(id);
            if (td == null)
                return NotFound();
            tdi.DeleteToDo(id);         
            return NoContent();
        }
    }
}
