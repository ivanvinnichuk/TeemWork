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
        public ActionResult CutCopy(string[] str) {
            var shell = PowerShell.Create();
            if(str[0]=="2")
            {
                for (int i = str.Length; i > 1;i-- )
                    shell.Commands.AddScript("Copy-Item  '" + str[i] + "'  '" + str[1]+ "'");
            }
            if (str[0] == "1")
            {
                for (int i = str.Length; i > 1; i--)
                    shell.Commands.AddScript("Move-Item  '" + str[i] + "'  '" + str[1] + "'");
            }
            
            return Content("Done");
        }
        
        
        
	}
}