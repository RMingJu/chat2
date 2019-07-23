using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GameRules()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [Filter.MyLoginFilter()]
        public ActionResult Chat(DataTable dtUserInfoTemp)
        {
            Models.UserInfo userInfo = (Models.UserInfo)TempData["userInfo"];
            if (userInfo == null) {
               return RedirectToAction("Index","Login");
            }

            return View(userInfo);
        }




        /// <summary>
        /// 報名表
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// 報名表處理
        /// </summary>
        /// <param name="registrationData"></param>
        /// <param name="userImg"></param>
        /// <returns></returns>
        public ActionResult RegistrationProcessing(Models.Registration registrationData, HttpPostedFileBase userImg)
        {

            PubClass myClass = new PubClass();

            byte[] pic = null;
            if (userImg != null)
            {



                pic = new byte[userImg.ContentLength];
                userImg.InputStream.Read(pic, 0, userImg.ContentLength);

                MemoryStream ms2 = new MemoryStream(pic);
                Image originalImage = Image.FromStream(ms2);
                if (originalImage.PropertyIdList.Contains(0x0112))
                {
                    int rotationValue = originalImage.GetPropertyItem(0x0112).Value[0];
                    switch (rotationValue)
                    {
                        case 1: // landscape, do nothing
                            break;

                        case 8: // rotated 90 right
                                // de-rotate:
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                            break;

                        case 3: // bottoms up
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                            break;

                        case 6: // rotated 90 left
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                            break;
                    }
                }





                Image ii = originalImage;
                Bitmap bp = new Bitmap(ii, 300, 300);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic = ms.GetBuffer();
                ms.Close();
            }



            
            




            bool blnIsSuccessed =  myClass.InsertData(
                                                    registrationData.userName
                                                    , registrationData.phoneNumber
                                                    , registrationData.job
                                                    , registrationData.jobIntroduction
                                                    , pic
                                                    );

            if (blnIsSuccessed){
                return RedirectToAction("RegistrationSuccessed",new { userName = registrationData .userName});
            }
            else {
                return RedirectToAction("RegistrationFaild");
            }
        }

        /// <summary>
        /// 參加表報名成功
        /// </summary>
        /// <returns></returns>
        public ActionResult RegistrationSuccessed(String userName) {
            PubClass myClass = new PubClass();
            DataTable dtData = myClass.GetRegistrationData(userName);
            if (dtData.Rows.Count < 1) {
                return RedirectToAction("Registration");   //回到報名表
            }
            DataRow drRow = dtData.Rows[0];

            Models.Registration registrationData = new Models.Registration();
            registrationData.userName = drRow["userName"].ToString();
            registrationData.phoneNumber = drRow["phoneNumber"].ToString();
            registrationData.job = drRow["job"].ToString();
            registrationData.jobIntroduction = drRow["jobIntroduction"].ToString();
            registrationData.img = (byte[])drRow["img"];
            

            return View(registrationData);
        }


        public ActionResult RegistrationFaild() {
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}