using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ToDo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ToDo
        [HttpPost]
        [Route("addtodolist")]
        public ActionResult<StatusViewModel> addToDoList([FromBody] ToDoViewModel info)
        {
            StatusViewModel result = new StatusViewModel();

            ToDoListItem newToDoList = new ToDoListItem();
            newToDoList.Name = info.Name;
            newToDoList.Description = info.Description;
            newToDoList.Category = info.Category;
            
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

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
