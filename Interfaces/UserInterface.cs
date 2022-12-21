using System;
using System.Collections.Generic;

using ToDo;

namespace ToDo.interfaces{
    public interface UserInterface
    {
        List<User> getAll();
        User getUserId(int id);
        void addUser(User u);
    }
}