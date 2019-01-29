using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YK.Cache;
using YK.Common;
using YK.Common.Extensions;
using YK.Models.Systems;
using YK.Utility;
using YK.Utility.Extensions;

namespace Config.Areas.Systems.Controllers
{
    public class OrganizationsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Systems/Organizations
        public string GetData()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            SysOrganizations entity = new SysOrganizations();
            entity.Name = "A";
            YK.Core.Framework<SysOrganizations>.Instance().Insert(entity);
            entity.Name = "B";
            YK.Core.Framework<SysOrganizations>.Instance().Insert(entity);
            entity.Name = "C";
            YK.Core.Framework<SysOrganizations>.Instance().Insert(entity);
            entity.Name = "D";
            YK.Core.Framework<SysOrganizations>.Instance().Insert(entity);

            var orgList = YK.Core.Framework<SysOrganizations>.Instance().Search(w=>w.ID.In(list));
            var organList = YK.Core.Framework<SysOrganizations>.Instance().Search(w => w.Code.LeftLike("A0"));
            var zhList= YK.Core.Framework<SysOrganizations>.Instance().Search(w => w.ID.In(list) && w.Code.RightLike("2B"));

            var data = BusinessCachesHelper<SysOrganizations>.GetAllEntityCache();
            Result result = new Result();
            result.IsSuccess = true;
            result.Data = data;
            return result.ToJson();
        }

        public ActionResult Edit(int? id)
        {
            return View();
        }
        public string GetEditData(int? id)
        {
            Result result = new Result();
            if (id != null) {
                var data = BusinessCachesHelper<SysOrganizations>.GetAllEntityCache();
                var list = data.Where(w => w.ID == id.Value);
                if (list.Count() == 1) {
                    result.IsSuccess = true;
                    result.Data = list.First();
                }
            }
            return result.ToJson();
        }
        public string Save(SysOrganizations org)
        {
            Result result = new Result();
            if (org != null)
            {
                var data = BusinessCachesHelper<SysOrganizations>.GetAllEntityCache();
                if (data != null)
                {
                    org.ID = data.Select(s => s.ID).Max() + 1;
                }
                else {
                    org.ID = 1;
                }                
                BusinessCachesHelper<SysOrganizations>.AddEntityCache(org.ID, org);
                result.IsSuccess = true;
            }
            return result.ToJson();
        }
    }
}