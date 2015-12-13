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
        public ActionResult Generalmonitoring()
        {
            return View();
        }
        public ActionResult Cron()
        {
            return View();
        }
        public ActionResult Manager()
        {
            return View();
        }
        public ActionResult Enter(string login, string pass)
        {
            if ((login == "admin") && (pass == "1"))
                return Manager();
            else 
                return Content("error");
        }
        /////////
        public string List(string str)
        {
            string Result;
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
                Result = builder.ToString();
                return Result;
            }

            return "Folder is empty!!!";
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

            return Content("Folder is empty!!!", "text/plain");
        }

        //[HttpGet]


        public void Cut(string path, string file)
        {
            var shell = PowerShell.Create();
            shell.Commands.AddScript(" Move-Item  " + file + "  " + path);
            var results = shell.Invoke();

            if (System.IO.Directory.Exists(file))
            {

                string dirName = new DirectoryInfo(@file).Name;
                string s = path + "\\" + dirName;
                CopyCutARR(file, s, 2);
            }


        }


        public void CopyCutARR(string file, string path, int c)
        {
            List<string> ss = new List<string>();
            var shell = PowerShell.Create();
            shell.Commands.AddScript(" Get-ChildItem '" + file + "' |  Select-Object  FullName ");
            var results = shell.Invoke();
            if (results.Count > 0)
            {
                var builder = new StringBuilder();
                foreach (var Obj in results)
                {
                    string qwe = Obj.ToString().Replace("@{FullName=", "").Replace("}", "");
                    ss.Add(qwe);//.Members["FullName"].Value);

                }
                if (c == 1)
                    foreach (string s in ss)
                        Copy(path, s);
                if (c == 2)
                    foreach (string s in ss)
                        Cut(path, s);
            }
        }
        public void Copy(string path, string file)
        {
            var shell = PowerShell.Create();
            shell.Commands.AddScript(" Copy-Item  " + file + "  " + path);
            var results = shell.Invoke();

            if (System.IO.Directory.Exists(file))
            {

                string dirName = new DirectoryInfo(@file).Name;
                string s = path + "\\" + dirName;
                CopyCutARR(file, s, 1);
            }


        }
        public ActionResult CutCopy(string type, string path, string file, int count)
        {

            string s = type.ToString();


            if (type == "2")
            {
                Copy(path, file);
            }
            if (type == "1")
            {
                Cut(path, file);
            }
            if (count == 2)
            {
                string res = List(path);
                return Content(res);
            }
            return Content("11", "text/plain");
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
            string ff = str;
            var shell = PowerShell.Create();

            shell.Commands.AddScript(" New-Item " + str + " -type file ");
            var results = shell.Invoke();
            ff = ff.Remove(ff.LastIndexOf(@"\"));
            string result = List(ff);
            return Content(result, "text/pain");
        }
        public ActionResult CreateDirectory(string str)
        {
            string ff = str;
            var shell = PowerShell.Create();
            shell.Commands.AddScript(" New-Item " + str + " -type directory");
            var results = shell.Invoke();
            ff = ff.Remove(ff.LastIndexOf(@"\"));
            string result = List(ff);
            return Content(result, "text/pain");
        }
        public ActionResult Delete(string str, int count)
        {

            string ff = str;
            var shell = PowerShell.Create();

            shell.Commands.AddScript(" Remove-Item  '" + str + "' -Recurse ");

            var results = shell.Invoke();
            if (count == 0)
            {
                if (ff.Length > 4)
                    ff = ff.Remove(ff.LastIndexOf(@"\")) + "\\";
                string result = List(ff);
                return Content(result, "text/pain");
            }
            return Content("12", "text/pain");
        }

    }
}