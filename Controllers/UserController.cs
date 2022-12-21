using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDo;
using ToDo.Service;
using Todo.interfaces;
using ToDo.interfaces;

namespace ToDo.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserInterface us;
        TokenService tokenService;

        public UserController(UserInterface us)
        {
            this.us = us;
            this.tokenService= new TokenService();
        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult<string> login([FromBody] User u){
            var dt = DateTime.Now;

            if (u.name != "HM")
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };

            var token = this.tokenService.GetToken(claims);

            return new OkObjectResult(this.tokenService.WriteToken(token));
      
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return us.getAll();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
           var user = us.getUserId(id);
           if (user==null)
                return NotFound();
            return user;
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            us.addUser(user);
            return CreatedAtAction(nameof(Post), new { id = user.id}, user);
        }
    }
}
