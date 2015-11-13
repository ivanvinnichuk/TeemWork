using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Management.Automation;

namespace Site.Controllers
{
    public class FilesController : Controller
    {
        //
        // GET: /Files/
        public ActionResult Index()
        {
            return View();
        }


        //
        public ActionResult ListOfFile(string str)
        {
            
            var shell = PowerShell.Create();
            shell.Commands.AddScript(" Get-ChildItem '" + str + "' |  Select-Object Name, Mode, LastWriteTime, FullName  | ConvertTo-Json");
            var results = shell.Invoke();
            if (results.Count > 0)
            {

                var builder = new StringBuilder();
                


                foreach (var psObject in results)
                {
                    builder.Append(psObject.ToString() + "\r\n");
                   
                }
                string Result = builder.ToString();
                return Content(Result);
            }

            return Content("Папка пуста!");
        }
        public ActionResult CutCopy(string type)
        {

            string s = type;
            
            /*var shell = PowerShell.Create();
            if(type=="2")
            {
                
                    shell.Commands.AddScript("Copy-Item  '" + file + "'  '" + path+ "'");
            }
            if (type == "1")
            {
               
                    shell.Commands.AddScript("Move-Item  '" + file + "'  '" + path + "'");
            }
            */
            return Content("Done");
        }
        
        
        
	}
}