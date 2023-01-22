using System;

namespace ToDo{

    public class myToDo{
        private static int nextId=0;

        public int id {get; set;}
        public int userid { get; set; }
        public string name {get; set;}
        public bool isDone {get; set;}


        public myToDo(int userid,string name){
            this.id=nextId++;
            this.userid=userid;
            this.name=name;
            this.isDone=false;
        }


    }
}