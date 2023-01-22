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

        public UserController(UserInterface us)
        {
            this.us = us;
        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult<string> login(string name, string password)
        {
            // var dt = DateTime.Now;

            User u=us.getUserId(name, password);

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
            us.deleteUser(id);
            return Ok();
        }
    }
}
