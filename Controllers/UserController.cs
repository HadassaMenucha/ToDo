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
using System.IdentityModel.Tokens.Jwt;
using ToDo;
using ToDo.Service;
using Todo.interfaces;
using ToDo.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ToDo.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserInterface us;
        // TokenService tokenService;

        public UserController(UserInterface us)
        {
            this.us = us;
        }

        [HttpPut]
        [Route("[action]")]
        public ActionResult<string> login([FromBody] User u){
            var dt = DateTime.Now;

            var claims = new List<Claim>();

            if (u.isAdmin)
            {
                claims= new List<Claim>{
                    new Claim("type","Admin")
                } ;
            }
            else{
                 claims= new List<Claim>{
                    new Claim("type","Person")
                } ;
            }

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
      
        }

        [HttpGet]
        [Authorize(Policy="Admin")]
        public IActionResult Get()
        {
            return Ok(us.getAll());
        }

        [HttpGet("{id}")]
        [Authorize(Policy="Person")]
        public IActionResult Get(int id)
        {
           var user = us.getUserId(id);
           if (user==null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Policy="Person")]
        public ActionResult Post(User user)
        {
            us.addUser(user);
            return CreatedAtAction(nameof(Post), new { id = user.id}, user);
        }
    }
}
