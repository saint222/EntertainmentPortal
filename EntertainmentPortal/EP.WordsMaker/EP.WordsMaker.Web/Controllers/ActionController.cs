using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EP.WordsMaker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        // GET: api/Action
        [HttpGet]
        public IEnumerable<string> GetAllActions()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Action/5
        [HttpGet("{id}")]
        public string GetAction(int id)
        {
            return "value";
        }

        // POST: api/Action
        [HttpPost]
        public void Action([FromBody] string value)
        {
        }

        // PUT: api/Action/5
        [HttpPut("{id}")]
        public void Action(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteAction(int id)
        {
        }
    }
}
