using System;
using System.Collections.Generic;

using ToDo;

namespace ToDo.interfaces{
    public interface UserInterface
    {
        List<User> getAll();
        User getUserId(string name, string password);
        void addUser(User u);
        void deleteUser(int id);
    }
}