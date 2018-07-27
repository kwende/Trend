using System;
using System.Collections.Generic;
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
        public JsonResult OnGet()
        {
            JsonResult result = new JsonResult("");

            string command = HttpContext.Request.Query["command"];
            string name = HttpContext.Request.Query["name"];
            string id = HttpContext.Request.Query["id"];

            TrendService service = HttpContext.RequestServices.GetService(typeof(TrendService)) as TrendService;

            // one at a time. 
            if (command == "create" && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(id))
            {
                service.AddEntryType(name, id);
            }
            // multiple
            else if (command == "sync")
            {
                string[] names = null;
                string[] ids = null;

                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(id))
                {
                    names = new string[0];
                    ids = new string[0];
                }
                else
                {
                    names = name.Split(",").Select(n => n.Trim()).ToArray();
                    ids = id.Split(",").Select(n => n.Trim()).ToArray();
                }

                service.SyncEntryTypes(names, ids);
            }
            else if (command == "delete" && !string.IsNullOrEmpty(id))
            {
                service.DeleteEntryType(id);
            }
            else if (command == "get")
            {
                List<EntryType> entryTypes = service.GetEntryTypes();
                result = new JsonResult(entryTypes);
            }

            return result;
        }
    }
}