using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YK.Models.Systems;
using Newtonsoft;
using YK.Services.Systems;

namespace Config.Areas.Systems.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Systems/Modules
        public ActionResult Index()
        {
            return View();
        }

        public string GetModules() {
            SysModules entity = new SysModules();
            entity.Name = "hello" + DateTime.Now.Millisecond;
            entity.Code = entity.Name;
            SysModulesService service = new SysModulesService();
            var et = service.GetById(1);
            service.Save(entity);
            List<SysModules> list = service.GetAllModules();
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}