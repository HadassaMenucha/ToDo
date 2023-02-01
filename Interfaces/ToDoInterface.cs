using System;
using ToDo;
using System.Collections.Generic;

namespace ToDo.interfaces
{
    public interface ToDoInterface
    {
        List<myToDo> GetAllToDos();
        List<myToDo> getToDoByUserId(int id);
         myToDo getToDoById(int id);
         void AddToDo(myToDo t);
         void DeleteToDo(int id);
         public void DeleteToDoByUserId(int id);
         bool Update(myToDo td);
    }
}
