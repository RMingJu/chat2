using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chat.Controllers
{
    public class MemberController : Controller
    {
        //登入頁面
        public ActionResult Index(String errorMsg)
        {
            if (errorMsg.Length > 0)
                ViewData["errorMsg"] = errorMsg;
                
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidLoginProcess(Models.LoginInfo li)
        {
            List<String> accts = new List<string>();
            List<String> pwd = new List<string>();
            List<String> name = new List<string>();
            accts.Add("mark");
            pwd.Add("abcd1234");
            name.Add("Ming");

            for (int i = 0; i <= accts.Count - 1; i++) {
                if (li.Acct == accts[i]) {
                    if (li.Pass == pwd[i])
                    {
                        Session.Add("UserName", name[i]);
                        return RedirectToAction("Index","Home");
                    }
                    else return RedirectToAction("Index", new { errorMsg = "lf" });

                }

            }
            
            return View();
        }


    }
}