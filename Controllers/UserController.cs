using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Service;
using ToDo.interfaces;

namespace ToDo.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserInterface us;
        ToDoInterface tdi;

        public UserController(UserInterface us, ToDoInterface tdi)
        {
            this.us = us;
            this.tdi=tdi;
        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult<string> login([FromBody] User user)
        {
            User u=us.getUserId(user.name, user.password);

            var claims = new List<Claim>();

            if (u.isAdmin)
            {
                claims = new List<Claim>{
                    new Claim("type","Admin")
                };
            }
            else
            {
                claims = new List<Claim>{
                    new Claim("type","Person")
                };
            }

            claims.Add(new Claim("Id" , u.id.ToString()));

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult GetAllUsers()
        {
            return Ok(us.getAll());
        }

        [HttpGet("{name},{password}")]
        [Authorize(Policy = "Person")]
        public IActionResult GetMyUser(string name, string password)
        {
            var user = us.getUserId(name, password);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult AddNewUser(User user)
        {
            us.addUser(user);
            return CreatedAtAction(nameof(AddNewUser), new { id = user.id }, user);
        }


        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public IActionResult deleteUser(int id)
        {
            if(id==0)
                return BadRequest();
            tdi.DeleteToDoByUserId(id);
            us.deleteUser(id);
            return Ok();
        }
    }
}
