using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace chat.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string tp)
        {
            if (tp == "lf")
                ViewData["Msg"] = "登入失敗!";
            return View();
        }


        public ActionResult ValidLoginProcess(Models.LoginInfo li)
        {


            bool blnLogin = false;
            PubClass myClass = new PubClass();
            blnLogin = myClass.IsLoginSuccessed(li.Acct);


            if (blnLogin)
            {
                DataTable dtUserInfo = myClass.dtReturn;
                DataRow drRowUserInfo = dtUserInfo.Rows[0];

                Models.UserInfo userInfo = new Models.UserInfo();
                userInfo.userName = drRowUserInfo["userName"].ToString();
                userInfo.phoneNumber = drRowUserInfo["phoneNumber"].ToString();
                userInfo.job = drRowUserInfo["job"].ToString();
                userInfo.jobIntroduction = drRowUserInfo["jobIntroduction"].ToString();
                userInfo.imgPath = drRowUserInfo["imgPath"].ToString();

                //HttpCookie Cookie = new HttpCookie("LoginName", userInfo.userName);
                //HttpContext.Response.Cookies.Add(Cookie);
                Session.Add("LoginName", li.Acct);

                TempData["userInfo"] = userInfo;
                return RedirectToAction("Chat", "Home");

                //取出User電腦上是否有原有要瀏覽的網頁，因為未登入而前來登入
                //HttpCookie cc = Request.Cookies["ReUrl"];
                //if (cc == null)
                //{
                //    //return RedirectToAction("Main", "Member");
                //    return RedirectToAction("LoginSuccess");
                //}
                //else
                //{
                //    return RedirectToAction("chat","home",new { ui = userInfo });
                //}
            }
            else
            {
                return RedirectToAction("Index", new { tp = "lf" });
            }
        }


    }
}