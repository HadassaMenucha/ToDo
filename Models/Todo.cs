using System;

namespace ToDo{

    public class myToDo{
        private static int nextId=0;

        public int id {get; set;}
        public string name {get; set;}
        public bool isDone {get; set;}


        public myToDo(string name,bool isDone){
            this.id=nextId++;
            this.name=name;
            this.isDone=isDone;
        }


    }
}