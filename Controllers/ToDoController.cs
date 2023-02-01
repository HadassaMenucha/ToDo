using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.interfaces;
using ToDo.Service;
using System;

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
        public ActionResult<myToDo> GetTaskByid( int id)
        {
           var td = tdi.getToDoById(id);
           if (td==null)
                return NotFound();
            return td;
        }

        [HttpPut]
        public ActionResult UpdateTask([FromBody] myToDo td)
        {
            td.userid = int.Parse(User.Claims.First(c=>c.Type=="Id").Value);
            var res = tdi.Update(td);
            if(!res)
               return NotFound();
            return NoContent();
        }
        [HttpPost]
        public ActionResult AddNewToDo(myToDo td)
        {
            td.userid= int.Parse(User.Claims.First(c=>c.Type=="Id").Value);
            tdi.AddToDo(td);
            return CreatedAtAction(nameof(AddNewToDo), new { id = td.id}, td);
        }

        [HttpDelete]
        public ActionResult DeleteTask([FromBody] int id) 
        {
            var td = tdi.getToDoById(id);
            if(td.userid!= int.Parse(User.Claims.First(c=>(c.Type=="Id")).Value))
                return Unauthorized();
            if (td == null)
                return NotFound();
            tdi.DeleteToDo(id);         
            return NoContent();
        }
    }
}