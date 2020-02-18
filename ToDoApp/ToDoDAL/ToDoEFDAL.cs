using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApp;
using ToDoApp.Models;
using ToDoEFDB.Context;

namespace ToDoDAL
{
    public class ToDoEFDAL : IToDoApp
    {
        //DbContextOptionsBuilder _optionsBuilder;
        //public ToDoEFDAL(DbContextOptionsBuilder optionsBuilder)
        //{
        //    _optionsBuilder = optionsBuilder;
        //}

        #region RoleItem
        public int AddRoleItem(RoleItem newRole)
        {
            int id;
            using (var context = new ToDoAppContext())
            {
                RoleItem role = new RoleItem();
                role.Name = newRole.Name;               
                context.RoleItem.Add(role);
                context.SaveChanges();
                id = role.Id;
            }
            return id;
        }

        public bool UpdateRoleItem(RoleItem item)
        {
            throw new NotImplementedException();
        }

        public RoleItem GetRoleItem(int roleId)
        {
            using (var context = new ToDoAppContext())
            {
                RoleItem role = context.RoleItem.Single(r => r.Id == roleId);
                return role;
            }
        }

        public List<RoleItem> GetRoleItems()
        {
            using (var context = new ToDoAppContext())
            {
                List<RoleItem> roles = context.RoleItem.ToList();
                return roles;
            }
        }

        public void UpdateRoleItem(int roleId, string name)
        {
            using (var context = new ToDoAppContext())
            {
                RoleItem updatedRole = context.RoleItem.Single(r => r.Id == roleId);
                updatedRole.Name = name;
                context.SaveChanges();
            }
        }

        public void DeleteRoleItem(int roleId)
        {
            using (var context = new ToDoAppContext())
            {
                RoleItem role = context.RoleItem.Single(r => r.Id == roleId);
                context.RoleItem.Remove(role);
                context.SaveChanges();
            }
        }
        #endregion

        #region UserItem
        public int AddUserItem(UserItem newUser)
        {
            int id;
            UserItem user = new UserItem();
            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.Username = newUser.Username;
            user.Email = newUser.Email;
            user.Hash = newUser.Hash;
            user.Salt = newUser.Salt;
            user.RoleId = newUser.RoleId;
            using (var context = new ToDoAppContext())
            {
                context.UserItem.Add(user);
                context.SaveChanges();
                id = context.UserItem.Last().Id;
                
            }
            return id;
        }

        public UserItem GetUserItem(int userId)
        {
            using (var context = new ToDoAppContext())
            {
                UserItem user = context.UserItem.Find(userId);
                return user;
            }
        }

        public UserItem GetUserItem(string name)
        {
            using (var context = new ToDoAppContext())
            {
                UserItem user = context.UserItem.Single(u => u.Username == name);
                return user;
            }
        }

        public List<UserItem> GetUserItems()
        {
            using (var context = new ToDoAppContext())
            {
                List<UserItem> users = context.UserItem.ToList();

                return users;
            }
        }

        public bool UpdateUserItem(UserItem updatedUser)
        {
            using (var context = new ToDoAppContext())
            {
                UserItem user = context.UserItem.Single(u => u.Id == updatedUser.Id);
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Username = updatedUser.Username;
                user.Email = updatedUser.Email;
                user.Hash = updatedUser.Hash;
                user.Salt = updatedUser.Salt;
                user.RoleId = updatedUser.RoleId;
                user.Password = updatedUser.Password;
                user.ConfirmPassword = updatedUser.ConfirmPassword;

                context.SaveChanges();
                int? id = user.Id;
                return id != null;
            }
        }

        public void DeleteUserItem(int userId)
        {
            using (var context = new ToDoAppContext())
            {
                UserItem user = context.UserItem.Find(userId);
                context.UserItem.Remove(user);
                context.SaveChanges();
            }
        }
        #endregion

        #region ToDoItem
        public ToDoItem AddToDo(ToDoItem newToDo, int toDoListId)
        {
            ToDoItem toDo = new ToDoItem();
            ToDoListItem toDoList;
            using (var context = new ToDoAppContext())
            {
                toDoList = context.ToDoListItem.Find(toDoListId);

                toDo.Name = newToDo.Name;
                toDo.Description = newToDo.Description;
                toDo.ToDoListItem = toDoList;

                context.ToDoItem.Add(toDo);
                context.SaveChanges();
            }
            return toDo;
        }

        public bool UpdateToDo(ToDoItem updatedToDo)
        {
            using (var context = new ToDoAppContext())
            {
                ToDoItem toDo = context.ToDoItem.Find(updatedToDo.Id);
                toDo.Name = updatedToDo.Name;
                toDo.Description = updatedToDo.Description;
                toDo.ToDoListItem = updatedToDo.ToDoListItem;
                context.SaveChanges();

                int? id = toDo.Id;
                return id != null;
            }
        }

        public ToDoItem GetToDo(int toDoId)
        {
            ToDoItem toDo;
            using (var context = new ToDoAppContext())
            {
                toDo = context.ToDoItem.Find(toDoId);
                context.ToDoItem.Remove(toDo);
                context.SaveChanges();
            }
            return toDo;
        }



        public void DeleteToDo(int toDoId)
        {
            using (var context = new ToDoAppContext())
            {
                ToDoItem toDo = context.ToDoItem.Find(toDoId);
                context.ToDoItem.Remove(toDo);
                context.SaveChanges();
            }
        }
        #endregion

        #region ToDoListItems
        public int AddToDoList(ToDoListItem newToDoList, int userId)
        {
            //UserItem user;
            ToDoListItem toDoList = new ToDoListItem();
            toDoList.Category = newToDoList.Category;
            toDoList.Description = newToDoList.Description;
            toDoList.Name = newToDoList.Name;
            toDoList.TimeCreated = DateTime.Now;
            toDoList.UserItemId = userId;
            using (var context = new ToDoAppContext())
            {
                //user = context.UserItem.Find(userId);
                context.ToDoListItem.Add(toDoList);
                //user.UserToDoListItems.Add(new UserToDoListItem { ToDoListItemId = toDoList.Id });
                context.SaveChanges();
            }
            return toDoList.Id;
        }
        #endregion
    }
}
