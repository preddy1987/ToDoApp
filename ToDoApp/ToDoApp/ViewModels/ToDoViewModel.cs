using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoAPI.ViewModels
{
    public class ToDoViewModel
    {
        public ToDoItem ToDo { get; set; }
        public ToDoListItem ToDoList { get; set; }
    }
}
