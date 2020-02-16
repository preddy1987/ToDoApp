using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models
{
    public class ToDoItem : BaseItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTime StartTime { get; set; }
        //public DateTime EndTime { get; set; }
        public ToDoListItem ToDoListItem { get; set; }
    }
}
