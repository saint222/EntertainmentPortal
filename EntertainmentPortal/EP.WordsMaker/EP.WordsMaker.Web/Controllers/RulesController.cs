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
    public class RulesController : ControllerBase
    {
        // GET: api/Rules
        [HttpGet]
        public IEnumerable<string> GetRules()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Rules/5
        [HttpGet("{id}")]
        public string GetRules(int id)
        {
            return "value";
        }

        // POST: api/Rules
        [HttpPost]
        public void PostRules([FromBody] string value)
        {
        }

        // PUT: api/Rules/5
        [HttpPut("{id}")]
        public void PutRules(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteRules(int id)
        {
        }
    }
}
