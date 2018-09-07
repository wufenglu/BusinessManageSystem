using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using YK.Core;
using YK.Core.Enums;
using YK.Core.Helper;
using YK.Core.Model;
using YK.Interfaces.Systems;
using YK.Models.Systems;
using YK.Services.Systems;

namespace Config.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SysModules proxyClass = EntityFactory.New<SysModules>();
            proxyClass.ID = 1;
            proxyClass.Level = 2;
            proxyClass.Name = "3";
            var plist = proxyClass.ChanageProperty;

            Framework<SysModules>.Instance().Update(proxyClass);
            Framework<SysModules>.Instance().Update(proxyClass, w => w.ID == 1);
            Framework<SysModules>.Instance().Update(proxyClass, new List<Expression>() { new Expression("ID", ConditionEnum.Eq, 1) });

            var aaa = Framework<SysModules>.Instance().Get(proxyClass.ID);
            var bbb = Framework<SysModules>.Instance().Get(w => w.ID == proxyClass.ID);
            var ccc = Framework<SysModules>.Instance().Get(new List<Expression>() { new Expression("ID", ConditionEnum.Eq, 1) });

            return null;

            Framework<SysModules>.Instance().Insert(proxyClass);
            
            var x = EntityFactory.New<SysAcions>();
            var y = EntityFactory.New<SysOrganizationModules>();
            var z = EntityFactory.New<SysOrganizations>();

            ServiceContainer.Register<YK.Services.Systems.SysOrganizationsService, YK.Interfaces.Systems.ISysOrganizations>();
            var service = ServiceContainer.GetService<ISysOrganizations>();

            var list = YK.Core.Framework<SysModules>.Instance().Find(w => w.ID == 7);

            SysModules model = new SysModules();
            model.Name = "A";
            model.Code = "A";
            YK.Core.Framework<SysModules>.Instance().Insert(model);
            model.Name = "B";
            YK.Core.Framework<SysModules>.Instance().Update(model, w => w.ID == 7);

            var result = EventHelper.Instance.GetConfig();

            Response.Write(service.GetType().FullName);

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