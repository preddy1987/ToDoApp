using System;
using System.Collections.Generic;

namespace ToDoApp.Models
{
   public class ToDoListItem : BaseItem
    {
        //public List<UserToDoListItem> UserToDoListItems { get; set; }
        public int UserItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime TimeCreated { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }

        public ToDoListItem()
        {
            //UserToDoListItems = new List<UserToDoListItem>();
            ToDoItems = new List<ToDoItem>();
        }
    }
}
