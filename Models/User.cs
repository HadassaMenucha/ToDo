using System;
namespace ToDo{

public class User{
    private static int nextId=0;
    public int id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public bool isAdmin { get; set; }

    public User(string name, string password, bool isAdmin)
    {
        this.id=nextId++;
        this.name=name;
        this.password=password;
        this.isAdmin=isAdmin;
    }
}    
}