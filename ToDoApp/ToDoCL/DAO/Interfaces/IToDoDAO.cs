using ToDoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoEFDB.Models;

namespace ToDoApp.Interfaces
{
    public interface IToDoDAO
    {

        #region UserItem
        int AddUserItem(UserItem item);
        bool UpdateUserItem(UserItem item);
        void DeleteUserItem(int userId);
        UserItem GetUserItem(int userId);
        UserItem GetUserItem(string username);
        List<UserItem> GetUserItems();
        #endregion

        #region RoleItem
        int AddRoleItem(RoleItem item);
        List<RoleItem> GetRoleItems();
        RoleItem GetRoleItem(int id);
        bool UpdateRoleItem(RoleItem item);
        void DeleteRoleItem(int id);
        #endregion


    }
}
