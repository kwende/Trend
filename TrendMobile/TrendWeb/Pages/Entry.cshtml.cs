using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrendWeb.DataContracts;
using TrendWeb.Services;

namespace TrendWeb.Pages
{
    public class EntryModel : PageModel
    {
        public void OnPost()
        {
            Request.Body.Seek(0, SeekOrigin.Begin);
            string jsonData = new StreamReader(Request.Body).ReadToEnd();

            TrendService service = HttpContext.RequestServices.GetService(typeof(TrendService)) as TrendService;

            service.InsertJSON("userId", DateTime.Now, jsonData);
        }

        public JsonResult OnGet()
        {
            Request.Body.Seek(0, SeekOrigin.Begin);
            string jsonData = new StreamReader(Request.Body).ReadToEnd();

            TrendService service = HttpContext.RequestServices.GetService(typeof(TrendService)) as TrendService;

            service.InsertJSON("userId", DateTime.Now, jsonData); 

            return new JsonResult("");
        }
    }
}