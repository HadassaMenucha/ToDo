using System.Linq;
using System.Collections.Generic;
using ToDo.interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.Json;

namespace ToDo.Service
{

    public class ToDoService : ToDoInterface{
        public static List<myToDo> myToDos {get; set;}

         private IWebHostEnvironment webHost;
        private string filePath;
        
       public  ToDoService(IWebHostEnvironment webHost){
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "data", "Task.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                myToDos = JsonSerializer.Deserialize<List<myToDo>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            }
            myToDos.Add(new myToDo(0,"todo"));
            saveToFile();
        }

         private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(myToDos));
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
             saveToFile();
        }

         public  void DeleteToDo(int id){
           var td = getToDoById(id);
 
            if (td is null)
                return;

            myToDos.Remove(td);
             saveToFile();
        }

         public void DeleteToDoByUserId(int id){
        //    myToDos.ForEach((td)=>{
        //     if(td.userid==id)
        //         myToDos.Remove(td);
        //    });

           myToDos= myToDos.FindAll((td)=>td.userid!=id);

            saveToFile();
        }

         public  bool Update(myToDo td)
        {
            var index = myToDos.FindIndex(p => p.id == td.id);
            if (index == -1)
                return false;

            myToDos[index] = td;
            saveToFile();
            return true;
             
        }
    }
}