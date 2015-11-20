using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Management.Automation;
using System.IO;

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

            return Content("Папка пуста!", "text/plain");
        }

        //[HttpGet]
        public ActionResult CutCopy(string type, string path, string file)
        {

            string s = type.ToString();


            if (type == "2")
            {
                var shell = PowerShell.Create();
                shell.Commands.AddScript(" Copy-Item  " + file + "  " + path);
                var results = shell.Invoke();
            }
            if (type == "1")
            {
                var shell = PowerShell.Create();
                shell.Commands.AddScript(" Move-Item  '" + file + "'  '" + path + "'");
                var results = shell.Invoke();
            }


            return Content(path);

        }

        public ActionResult OpenFile(string str)
        {

            StreamReader sr = new StreamReader(str);
            string strin = sr.ReadToEnd().ToString();
            sr.Close();


            return Content(strin, "text/plain");

        }
        public ActionResult RewriteToFile(string path, string str)
        {
            string s = path;
            string s1 = str;
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(str);


            sw.Close();


            return Content("done", "text/plain");

        }
        public ActionResult CreateFile(string str)
        {
            string ss = str;
            var shell = PowerShell.Create();
            shell.Commands.AddScript(" New-Item " + str + " -type file");
            var results = shell.Invoke();
            return Content("Done", "text/pain");
        }
        public ActionResult CreateDirectory(string str)
        {
            string ss = str;
            var shell = PowerShell.Create();
            shell.Commands.AddScript(" New-Item " + str + " -type directory");
            var results = shell.Invoke();
            return Content("Done", "text/pain");
        }
        public ActionResult Delete(string str)
        {
            string ff = str;
            var shell = PowerShell.Create();
            shell.Commands.AddScript(" Remove-Item  '" + str + "' -recurse");
            var results = shell.Invoke();
            return Content("Done", "text/pain");
        }

    }
}