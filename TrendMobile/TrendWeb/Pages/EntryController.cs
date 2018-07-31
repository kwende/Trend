using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrendWeb.Pages
{
    [Route("api/[controller]")]
    public class EntryController : Controller
    {
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]JToken jsonbody)
        {
            string json = jsonbody.ToString(); 
            return; 
        }
    }
}
