using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.ViewModels
{
    public class UserItemViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
