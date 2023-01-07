using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using ToDo;
using ToDo.Service;
using ToDo.interfaces;

namespace ToDo.Service
{
    public class UserService : UserInterface
    {
        public List<User> users { get; set; }

        public UserService()
        {
            this.users=new List<User>();
            this.users.Add(new User("user0","123",true));
        }

        // private IWebHostEnvironment webHost;

        private string filePath;

        // public UserService(IWebHostEnvironment webHost)
        // {
        //     this.webHost = webHost;
        //     this.filePath =
        //         Path.Combine(webHost.ContentRootPath, "Data", "Users.json");
        //     using (var f = File.OpenText(filePath))
        //     {
        //         users =
        //             JsonSerializer
        //                 .Deserialize<List<User>>(f.ReadToEnd(),
        //                 new JsonSerializerOptions {
        //                     PropertyNameCaseInsensitive = true
        //                 });
        //     }
        // }

        public List<User> getAll()
        {
            return users;
        }

        public User getUserId(int id)
        {
            return users.FirstOrDefault(u=>u.id==id);
        }

         public void addUser(User u)
        {
            users.Add(u);
        }

    }
}
