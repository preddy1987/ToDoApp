using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SessionControllerData;
using ToDoAPI.ViewModels;
using ToDoApp;
using ToDoApp.Models;
using ToDoApp.Exceptions;

namespace ToDoAPI.Controllers
{
    public class AuthController : SessionController
    {
        /// <summary>
        /// Manages the user authentication and authorization
        /// </summary>
        private RoleManager _roleMgr = null;
        protected IToDoApp _db = null;
        private const string RoleMgrKey = "RoleManager";

        public AuthController(IToDoApp db, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _db = db;
            // Get the role manager from the session
            _roleMgr = GetSessionData<RoleManager>(RoleMgrKey);
            // If it does not exist on the session then add it
            if(_roleMgr == null)
            {
                // Since the role manager is being created then a user still needs to be authenticated
                _roleMgr = new RoleManager(null);
                SetSessionData(RoleMgrKey, _roleMgr);
            }
        }

        /// <summary>
        /// The current logged in user
        /// </summary>
        public UserItem CurrentUser
        {
            get
            {
                return _roleMgr.User;
            }
        }

        /// <summary>
        /// RoleManager used to validate user permissions and have access to the current user information
        /// </summary>
        public RoleManager Role
        {
            get
            {
                return _roleMgr;
            }
        }

        /// <summary>
        /// Returns true if the user is authenticated
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return _roleMgr.User != null;
            }
        }

        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="userModel">Model that contains all the user information</param>
        public void RegisterUser(UserItem userModel)
        {
            UserItem userItem = null;
            try
            {
                userItem = _db.GetUserItem(userModel.Username);
            }
            catch (Exception)
            {
            }

            if (userItem != null)
            {
                throw new UserExistsException("The username is already taken.");
            }

            if (userModel.Password != userModel.ConfirmPassword)
            {
                throw new PasswordMatchException("The password and confirm password do not match.");
            }

            PasswordManager passHelper = new PasswordManager(userModel.Password);
            UserItem newUser = new UserItem()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Username = userModel.Username,
                Salt = passHelper.Salt,
                Hash = passHelper.Hash,
                RoleId = (int)RoleManager.eRole.Customer
            };

            _db.AddUserItem(newUser);
            LoginUser(newUser.Username, userModel.Password);
        }

        /// <summary>
        /// Logs a user and throws exceptions on any failures
        /// </summary>
        /// <param name="username">The username of the user to authenicate</param>
        /// <param name="password">The password of the user to authenicate</param>
        public void LoginUser(string username, string password)
        {
            UserItem user = null;

            try
            {
                user = _db.GetUserItem(username);
            }
            catch (Exception)
            {
                throw new Exception("Either the username or the password is invalid.");
            }

            PasswordManager passHelper = new PasswordManager(password, user.Salt);
            if (!passHelper.Verify(user.Hash))
            {
                throw new Exception("Either the username or the password is invalid.");
            }

            _roleMgr = new RoleManager(user);

            // Put the authenticated user in the session
            SetSessionData(RoleMgrKey, _roleMgr);
        }

        /// <summary>
        /// Logs the current user out
        /// </summary>
        public void LogoutUser()
        {
            _roleMgr = new RoleManager(null);
            
            SetSessionData(RoleMgrKey, _roleMgr);
        }

        public ActionResult GetAuthenticatedView(string viewName, object model = null)
        {
            ActionResult result = null;
            if (IsAuthenticated)
            {
                result = View(viewName, model);
            }
            else
            {
                result = RedirectToAction("Login", "User");
            }
            return result;
        }

        public JsonResult GetAuthenticatedJson(JsonResult json, bool hasPermission)
        {
            JsonResult result = null;
            if (!hasPermission && IsAuthenticated)
            {
                result = Json(new { error = "User is not permitted to access this data." });
            }
            else if (IsAuthenticated)
            {
                result = json;
            }
            else
            {
                result = Json(new { error = "User is not authenticated." });
            }
            return result;
        }
    }
}
