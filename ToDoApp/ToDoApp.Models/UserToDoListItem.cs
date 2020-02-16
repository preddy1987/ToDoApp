using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models
{
    public class UserToDoListItem : BaseItem
    {
        public int UserItemId { get; set; }
        public UserItem UserItem { get; set; }
        public int ToDoListItemId { get; set; }
        public ToDoListItem ToDoListItem { get; set; }
    }
}
