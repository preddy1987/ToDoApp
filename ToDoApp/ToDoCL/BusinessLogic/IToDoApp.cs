using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp;
using ToDoApp.Models;


namespace ToDoApp
{
    public interface IToDoApp
    {
        //bool IsAuthenticated { get; }
        //void RegisterUser(UserItem userModel);
        //void LoginUser(string username, string password);
        //void LogoutUser();

        //IList<UserItem> Users { get; }
        //RoleManager Role { get; }
        //UserItem CurrentUser { get; }
        //void DeleteRoleItem(int id);
        //UserItem GetUserItem(string username);
        //void AddUserItem(UserItem newUser);
        //List<UserItem> GetUserItems();

        //int AddRoleItem(RoleItem newRole);
        //List<RoleItem> GetRoleItems();
        //bool UpdateRoleItem(RoleItem updatedRole);
        //RoleItem GetRoleItem(int roleId);

        #region UserItem
        int AddUserItem(UserItem item);
        bool UpdateUserItem(UserItem item);
        void DeleteUserItem(int id);
        UserItem GetUserItem(int id);
        UserItem GetUserItem(string name);
        List<UserItem> GetUserItems();
        #endregion

        #region RoleItem
        int AddRoleItem(RoleItem item);
        List<RoleItem> GetRoleItems();
        RoleItem GetRoleItem(int roleId);
        bool UpdateRoleItem(RoleItem item);
        void DeleteRoleItem(int roleId);
        #endregion

        #region ToDoListItem
        int AddToDoList(ToDoListItem newToDoList, int userId);
        #endregion
    }
}
