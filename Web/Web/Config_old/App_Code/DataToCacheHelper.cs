using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using YK.Model;
using YK.Service;
using YK.Common;
using System.ComponentModel;

public class DataToCacheHelper
{
    public enum CacheNames 
    { 
        /// <summary>
        /// 所有权限资源信息
        /// </summary>
        [Description("所有权限资源")]
        AllResources,
    }
    
    /// <summary>
    /// 获取所有权限资源
    /// </summary>
    /// <returns></returns>
    public static List<TB_Admin_Resources> GetPermission()
    {
        List<TB_Admin_Resources> list = new List<TB_Admin_Resources>();
        if (CachesHelper.GetCache(CacheNames.AllResources.ToStr()) != null)
        {
            list = (List<TB_Admin_Resources>)CachesHelper.GetCache(CacheNames.AllResources.ToStr());
        }
        else
        {
            list=AdminService.ResourcesService.Search().OrderBy(r => r.OrderBy).ToList();
            CachesHelper.AddCache(CacheNames.AllResources.ToStr(), list,null);
        }
        return list;
    }
}
