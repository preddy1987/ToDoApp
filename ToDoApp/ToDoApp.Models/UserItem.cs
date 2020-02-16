using System.Collections.Generic;

namespace ToDoApp.Models
{
    public class UserItem : BaseItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<UserToDoListItem> UserToDoListItems { get; set; }

        public List<ToDoListItem> ToDoListItems {
            get
            {
                List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
                foreach(UserToDoListItem join in UserToDoListItems)
                {
                    toDoListItems.Add(join.ToDoListItem);
                }
                return toDoListItems;
            }           
        }

        public UserItem()
        {
            UserToDoListItems = new List<UserToDoListItem>();
        }

        public UserItem Clone()
        {
            UserItem item = new UserItem();
            item.Id = this.Id;
            item.FirstName = this.FirstName;
            item.LastName = this.LastName;
            item.Username = this.Username;
            item.Email = this.Email;
            item.Hash = this.Hash;
            item.Salt = this.Salt;
            item.RoleId = this.RoleId;
            item.UserToDoListItems = this.UserToDoListItems;
            return item;
        }
    }
}
