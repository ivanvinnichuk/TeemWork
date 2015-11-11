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
            string ss;
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
        public ActionResult Hello() {
            var shell = PowerShell.Create();
            shell.Commands.AddScript("New-Item -Path 'C:\' -ItemType 'file' -Value 'HEllo' -name 'vasa' ");
            var result = shell.Invoke();
            return View();
        }
        
        
        
	}
}