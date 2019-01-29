using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using YK.Cache;
using YK.Models.Systems;
using YK.Utility;
using YK.Utility.Extensions;
using YK.Utility.Views;
using YK.Utility.Views.Models;

namespace Config.Areas.Systems.Controllers
{
    public class GridController : Controller
    {
        public ActionResult Index()
        {
            string a= CommonClass.AppPath;
            string b = CommonClass.FullAppPath;
            string c = CommonClass.PhysicalApplicationPath;
            Grid grid = GridHelper.GetData();

            return View();
        }

        // GET: Systems/Organizations
        public string GetData(int id)
        {            
            Result result = new Result();
            result.IsSuccess = true;
            result.Data = null;
            return result.ToJson();
        }        
    }
}