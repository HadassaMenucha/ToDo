using System;
using ToDo;
using System.Collections.Generic;

namespace Todo.interfaces
{
    public interface ToDoInterface
    {
        List<myToDo> GetAllToDos();
         myToDo getToDoById(int id);
         void AddToDo(myToDo t);
         void DeleteToDo(int id);
         bool Update(myToDo td);
    }
}
