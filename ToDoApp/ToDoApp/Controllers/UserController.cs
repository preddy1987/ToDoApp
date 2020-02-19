using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.ViewModels;
using ToDoApp;
using ToDoApp.Models;

namespace ToDoAPI.Controllers
{
    public class UserController : AuthController
    {
        public UserController(IToDoApp db, IHttpContextAccessor httpContext) : base(db, httpContext)
        {

        }
        
        [HttpGet]
        [Route("api/user")]
        public ActionResult<IEnumerable<UserItem>> GetUser()
        {
            var result = Json(_db.GetUserItem(CurrentUser.Id));
            return GetAuthenticatedJson(result, (Role.IsExecutive || Role.IsAdministrator));
        }

        [HttpPost]
        [Route("api/login")]
        public ActionResult<StatusViewModel> Login([FromBody] LoginViewModel info)
        {
            StatusViewModel result = new StatusViewModel();

            try
            {
                LoginUser(info.UserName, info.Password);
            }
            catch(Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        [Route("api/register")]
        public ActionResult<StatusViewModel> Register([FromBody] RegisterViewModel info)
        {
            StatusViewModel result = new StatusViewModel();

            try
            {
                var user = new UserItem();
                user.ConfirmPassword = info.ConfirmPassword;
                user.Email = info.Email;
                user.FirstName = info.FirstName;
                user.LastName = info.LastName;
                user.Password = info.Password;
                user.Username = info.Username;

                RegisterUser(user);
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message + ex.InnerException;
            }

            return Json(result);
        }


    }
}