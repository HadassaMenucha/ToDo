using System.Linq;
using System.Collections.Generic;
using Todo.interfaces;

namespace ToDo.Service
{

    public class ToDoService : ToDoInterface{
        public static List<myToDo> myToDos {get; set;}
        
       public  ToDoService(){
            myToDos=new List<myToDo>();
            myToDos.Add(new myToDo(0,"MyFirstToDo"));
        }

        public List<myToDo> GetAllToDos()
        {
            return myToDos;
        }

        public List<myToDo> getToDoByUserId(int id){
            return myToDos.FindAll(td=>td.userid==id);
        }

        public  myToDo getToDoById(int id){
            return myToDos.Find(t=>t.id== id);
        }

        public  void AddToDo(myToDo t){
            myToDos.Add(t);
        }

         public  void DeleteToDo(int id){
           var td = getToDoById(id);
 
            if (td is null)
                return;

            myToDos.Remove(td);
        }

         public  bool Update(myToDo td)
        {
            var index = myToDos.FindIndex(p => p.id == td.id);
            if (index == -1)
                return false;

            myToDos[index] = td;
            return true;
        }
    }
}