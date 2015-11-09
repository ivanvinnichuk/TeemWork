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
            string str1 = "@Url.Content(\"~/ScriptPowerShell\")";
            var shell = PowerShell.Create();
            shell.Commands.AddScript("@Url.Content(\"~/Scripts/PrintDirectory.ps1\") -Path " + str + "-pathToStore"+str1);
            var result = shell.Invoke();
            return File(str,"application/json");
        }

        
        
        
	}
}