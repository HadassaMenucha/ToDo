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
        private IWebHostEnvironment webHost;
        private string filePath;

        public UserService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "data", "users.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            }

        }
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }
        public List<User> getAll()
        {
            return users;
        }

        public User getUserId(string name, string password)
        {
            return users.FirstOrDefault(u => u.name == name && u.password == password);
        }

        public void addUser(User u)
        {
            users.Add(u);
            saveToFile();
        }

        public void deleteUser(int id)
        {
            users = users.FindAll(u => u.id != id);
            saveToFile();
        }
    }
}