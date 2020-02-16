using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp;
using ToDoAPI.ViewModels;
using ToDoApp.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IToDoApp _db = null;

        public RoleController(IToDoApp db)
        {
            _db = db;
        }

        // GET api/role
        [HttpGet]
        public ActionResult<IEnumerable<RoleItem>> Get()
        {
            return _db.GetRoleItems();
        }

        // GET api/role/5
        [HttpGet("{id}")]
        public ActionResult<RoleItem> Get(int id)
        {
            return _db.GetRoleItem(id);
        }

        // POST api/role
        [HttpPost]
        public ActionResult<int> Post([FromBody] RoleItemViewModel value)
        {
            RoleItem item = new RoleItem();
            item.Name = value.Name;
            item.Id = value.Id;
            return _db.AddRoleItem(item);
        }

        // PUT api/role/5
        [HttpPut("{id}")]
        public ActionResult<bool> Put(int id, [FromBody] RoleItemViewModel value)
        {
            RoleItem item = new RoleItem();
            item.Id = id;
            item.Name = value.Name;
            return _db.UpdateRoleItem(item);
        }

        // DELETE api/role/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _db.DeleteRoleItem(id);
        }
    }
}