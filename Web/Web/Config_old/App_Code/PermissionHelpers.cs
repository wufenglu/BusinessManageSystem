using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using YK.Model;
using YK.Service;
using YK.Common;

public class PermissionHelpers
{
    /// <summary>
    /// 权限
    /// </summary>
    /// <param name="showMenu">展示菜单导航</param>
    /// <returns></returns>
    public List<TB_Admin_Resources> Permission(bool showMenu)
    {
        return new CommonHelpers().Permission();

        List<TB_Admin_Resources> modelList = new List<TB_Admin_Resources>();

        //获取以及资源信息
        List<TB_Admin_Resources> resourcesList = DataToCacheHelper.GetPermission();
        resourcesList = resourcesList.Where(r => r.ParentID == 0).ToList();
        if (showMenu == true)
        {
            resourcesList = resourcesList.Where(r => r.IsShow == true).ToList();
        }

        //遍历信息
        foreach (TB_Admin_Resources resource in resourcesList)
        {
            TB_Admin_Resources model = resource;
            model.ChildTree = new List<TB_Admin_Resources>();

            //获取子集
            List<TB_Admin_Resources> childList = DataToCacheHelper.GetPermission();
            childList = childList.Where(r => r.ParentID == resource.ID).ToList();

            if (showMenu == true)
            {
                childList = childList.Where(r => r.IsShow == true).ToList();
            }

            //判断资源编号是否在用户资源列表中
            foreach (var child in childList)
            {
                model.ChildTree.Add(child);
            }

            modelList.Add(model);
        }

        return modelList;
    }

    /// <summary>
    /// 核查权限
    /// </summary>
    public void CheckPermission()
    {
        List<TB_Admin_Resources> list = Permission(false);

        bool isOk = false;
        foreach (TB_Admin_Resources permission in list)
        {
            foreach (TB_Admin_Resources permission2 in permission.ChildTree)
            {
                string url = System.Web.HttpContext.Current.Request.Url.ToString();
                if (url.ToLower().Contains(permission2.Url.ToLower()))
                {
                    isOk = true;
                }
            }
        }

        if (isOk == false)
        {
            HttpContext.Current.Response.Redirect(CommonClass.AppPath + "Admin/Tishi.aspx");
        }
    }
}