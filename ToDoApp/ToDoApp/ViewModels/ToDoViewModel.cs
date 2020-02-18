using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoAPI.ViewModels
{
    public class ToDoViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
