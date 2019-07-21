using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chat.Controllers
{
    public class ToolController : Controller
    {
        public FileResult GetProductPic(String userName)
        {
            PubClass myClass = new PubClass();
            DataTable dtTemp = myClass.GetRegistrationData(userName);
            if (dtTemp.Rows.Count < 1) return null;

            DataRow drRow = dtTemp.Rows[0];


            byte[] bb = (byte[]) drRow["img"];
            if (bb == null) bb = new byte[0];

            return File(bb, "image/gif");
        }
    }
}