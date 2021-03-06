﻿using Microsoft.EntityFrameworkCore;
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
        DbContextOptions<ToDoAppContext> _options;
        public ToDoEFDAL(DbContextOptionsBuilder<ToDoAppContext> optionsBuilder)
        {
            _options = optionsBuilder.Options;
        }

        public ToDoEFDAL(DbContextOptions<ToDoAppContext> options)
        {
            _options = options;
        }

        public ToDoEFDAL()
        {

        }

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
            using (var context = new ToDoAppContext(_options))
            {
                context.UserItem.Add(user);
                context.SaveChanges();
                id = context.UserItem.Last().Id;
                
            }
            return id;
        }

        public UserItem GetUserItem(int userId)
        {
            using (var context = new ToDoAppContext(_options))
            {
                UserItem user = context.UserItem.Find(userId);
                return user;
            }
        }

        public UserItem GetUserItem(string name)
        {
            using (var context = new ToDoAppContext(_options))
            {
                UserItem user = context.UserItem.Single(u => u.Username == name);
                return user;
            }
        }

        public List<UserItem> GetUserItems()
        {
            using (var context = new ToDoAppContext(_options))
            {
                List<UserItem> users = context.UserItem.ToList();

                return users;
            }
        }

        public bool UpdateUserItem(UserItem updatedUser)
        {
            using (var context = new ToDoAppContext(_options))
            {
                UserItem user = context.UserItem.Single(u => u.Id == updatedUser.Id);
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Username = updatedUser.Username;
                user.Email = updatedUser.Email;
                user.Hash = updatedUser.Hash;
                user.Salt = updatedUser.Salt;
                user.RoleId = updatedUser.RoleId;

                context.SaveChanges();
                int? id = user.Id;
                return id != null;
            }
        }

        public void DeleteUserItem(int userId)
        {
            using (var context = new ToDoAppContext(_options))
            {
                UserItem user = context.UserItem.Find(userId);
                context.UserItem.Remove(user);
                context.SaveChanges();
            }
        }
        #endregion

        #region ToDoItem
        public int AddToDoItem(ToDoItem newToDo, int toDoListId)
        {
            ToDoItem toDo = new ToDoItem();
            ToDoListItem toDoList;
            using (var context = new ToDoAppContext(_options))
            {
                toDoList = context.ToDoListItem.Find(toDoListId);

                toDo.Name = newToDo.Name;
                toDo.Description = newToDo.Description;
                toDo.ToDoListItem = toDoList;

                context.ToDoItem.Add(toDo);
                context.SaveChanges();
            }
            return toDo.Id;
        }

        public bool UpdateToDoItem(ToDoItem updatedToDo)
        {
            int? id;
            using (var context = new ToDoAppContext(_options))
            {
                ToDoItem toDo = context.ToDoItem.Find(updatedToDo.Id);
                toDo.Name = updatedToDo.Name;
                toDo.Description = updatedToDo.Description;
                toDo.ToDoListItem = updatedToDo.ToDoListItem;
                context.SaveChanges();

                id = toDo.Id;                
            }
            return id != null;
        }

        public ToDoItem GetToDoItem(int toDoItemId)
        {
            ToDoItem toDo;
            using (var context = new ToDoAppContext(_options))
            {
                toDo = context.ToDoItem.Find(toDoItemId);
            }
            return toDo;
        }

        public List<ToDoItem> GetToDoItems(int toDoListId)
        {
            List<ToDoItem> toDoList;
            using (var context = new ToDoAppContext(_options))
            {
                toDoList = context.ToDoItem.Where(u => u.ToDoListItem.Id == toDoListId).ToList();
            }
            return toDoList;
        }

        public void DeleteToDoItem(int toDoItemId)
        {
            using (var context = new ToDoAppContext(_options))
            {
                ToDoItem toDo = context.ToDoItem.Find(toDoItemId);
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
            using (var context = new ToDoAppContext(_options))
            {
                //user = context.UserItem.Find(userId);
                context.ToDoListItem.Add(toDoList);
                //user.UserToDoListItems.Add(new UserToDoListItem { ToDoListItemId = toDoList.Id });
                context.SaveChanges();
            }
            return toDoList.Id;
        }

        public ToDoListItem GetToDoListItem(int toDoListItemId)
        {
            ToDoListItem toDoListItem;
            using (var context = new ToDoAppContext(_options))
            {
                toDoListItem = context.ToDoListItem.Find(toDoListItemId);
            }
            return toDoListItem;
        }

        public bool UpdateToDoListItem(ToDoListItem updatedToDoList)
        {
            int? id;
            ToDoListItem usersToDoListItem;
            using (var context = new ToDoAppContext(_options))
            {
                usersToDoListItem = context.ToDoListItem.Find();
                usersToDoListItem.Name = updatedToDoList.Name;
                usersToDoListItem.Description = updatedToDoList.Description;
                usersToDoListItem.Category = updatedToDoList.Category;
                context.SaveChanges();
                id = usersToDoListItem.Id;
            }
            return id != null;
        }

        public List<ToDoListItem> GetToDoListItems(int userId)
        {
            List<ToDoListItem> usersToDoListItems;
            using (var context = new ToDoAppContext(_options))
            {
                usersToDoListItems = context.ToDoListItem.Where(u => u.UserItemId == userId).ToList();
            }
            return usersToDoListItems;
        }

        public void DeleteToDoList(ToDoListItem toDoList)
        {
            using (var context = new ToDoAppContext(_options))
            {
                //ToDoListItem toDoList = context.ToDoListItem.Find(toDoListId);
                context.ToDoListItem.Remove(toDoList);
                context.SaveChanges();
            }
        }


        #endregion
    }
}
