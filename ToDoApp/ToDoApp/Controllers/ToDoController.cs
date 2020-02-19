using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ToDoAPI.ViewModels;
using ToDoApp;
using ToDoApp.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : AuthController
    {

        public ToDoController(IToDoApp db, IHttpContextAccessor httpContext) : base(db, httpContext)
        {

        }
        

        // GET: api/ToDo
        [HttpGet]
        public ActionResult<List<ToDoListItem>> GetUsersToDoLists()
        {
            List<ToDoListItem> userToDoListItems;
            try
            {
                userToDoListItems = _db.GetToDoListItems(CurrentUser.Id);
            }
            catch (Exception ex)
            {
                userToDoListItems = new List<ToDoListItem>();
            }
            return userToDoListItems;
        }

        [HttpPost]
        [Route("addtodolist")]
        public ActionResult<StatusViewModel> AddToDoList([FromBody] ToDoViewModel info)
        {
            StatusViewModel result = new StatusViewModel();

            ToDoListItem newToDoList = new ToDoListItem();
            newToDoList.Name = info.ToDoList.Name;
            newToDoList.Description = info.ToDoList.Description;
            newToDoList.Category = info.ToDoList.Category;
            
            try
            {
                _db.AddToDoList(newToDoList, CurrentUser.Id);
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }

            return Json(result);
        }
        [HttpPost]
        [Route("addtodoitem")]
        public ActionResult<StatusViewModel> AddToDoItem([FromBody] ToDoViewModel info)
        {
            StatusViewModel result = new StatusViewModel();

            ToDoItem newToDo = new ToDoItem();
            newToDo.Name = info.ToDo.Name;
            newToDo.Description = info.ToDo.Description;

            try
            {
                _db.AddToDoItem(newToDo, info.ToDoList.Id);
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        [Route("updatetodoitem")]
        public ActionResult<StatusViewModel> UpdateToDoItem([FromBody] ToDoViewModel info)
        {
            StatusViewModel result = new StatusViewModel();

            ToDoItem updatedToDo = new ToDoItem();
            updatedToDo.Name = info.ToDo.Name;
            updatedToDo.Description = info.ToDo.Description;
            //updatedToDo.Id = info.ToDo.Id;
            try
            {

                updatedToDo.Id = _db.GetToDoItem(info.ToDo.Id).Id;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }
            try
            {
                _db.UpdateToDoItem(updatedToDo);
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        [Route("updatetodolist")]
        public ActionResult<StatusViewModel> UpdateToDoList([FromBody] ToDoViewModel info)
        {
            StatusViewModel result = new StatusViewModel();

            ToDoListItem updatedToDoList = new ToDoListItem();
            updatedToDoList.Name = info.ToDoList.Name;
            updatedToDoList.Description = info.ToDoList.Description;
            updatedToDoList.Category = info.ToDoList.Category;
            try
            {

                updatedToDoList.Id = _db.GetToDoListItem(info.ToDoList.Id).Id;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }
            try
            {
                _db.UpdateToDoListItem(updatedToDoList);
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = ex.Message;
            }
            return Json(result);
        }
        //// GET: api/ToDo/
        //[HttpGet]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// PUT: api/ToDo/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
