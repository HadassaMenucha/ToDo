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
        public List<User> getAll()
        {
            return users;
        }

        public User getUserId(string name, string password)
        {
            return users.FirstOrDefault(u=>u.name== name && u.password ==password);
        }

         public void addUser(User u)
        {
            users.Add(u);
        }

        public void deleteUser(int id){
            users=users.FindAll(u=>u.id!=id);
        }
    }
}
