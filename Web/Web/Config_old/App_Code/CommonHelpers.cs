using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml;
using YK.Model;
using YK.Service;
using YK.Common;

    public class CommonHelpers
    {
        /// <summary>
        /// 获取目录路径
        /// </summary>
        public string AppPath = System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

        /// <summary>
        /// 获取所有权限菜单列表
        /// </summary>
        /// <returns></returns>
        public List<TB_Admin_Resources> Permission()
        {
            List<TB_Admin_Resources> list = new List<TB_Admin_Resources>();
            string result = String.Empty;

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/PermissionDictionaries.xml"));
            XmlNodeList xnl = xmldoc.SelectSingleNode("Dictionaries").ChildNodes;
            int orderBy = 0;
            foreach (XmlNode xn in xnl)
            {
                orderBy++;
                TB_Admin_Resources entity = new TB_Admin_Resources();
                entity.ID = xn.Attributes["ID"].Value.ToInt();
                entity.ResourceName = xn.Attributes["Name"].Value;
                entity.OrderBy = orderBy;
                entity.ChildTree = new List<TB_Admin_Resources>();
                entity.IsShow = xn.Attributes["IsShow"].Value.ToLower() == "true" ? true : false;

                int childOrderBy = 0;

                XmlNodeList childlist = xn.ChildNodes;
                foreach (XmlNode child in childlist)
                {
                    childOrderBy++;

                    TB_Admin_Resources entity2 = new TB_Admin_Resources();
                    entity2.ID = child.Attributes["ID"].Value.ToInt();
                    entity2.ResourceName = child.InnerText;
                    entity2.Url = child.Attributes["Url"].Value;
                    entity2.IsShow = child.Attributes["IsShow"] == null ? false : (child.Attributes["IsShow"].Value.ToLower() == "true" ? true : false);
                    entity2.OrderBy = childOrderBy;

                    entity.ChildTree.Add(entity2);
                }

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// 用户所属角色对应的权限
        /// </summary>
        /// <param ResourceName="IsShowMenu">是否展示菜单</param>
        /// <returns></returns>
        public List<TB_Admin_Resources> GetUserPermission(bool IsShowMenu)
        {
            List<int> resourcesIds = new List<int>();
            int AdminUserID = System.Web.HttpContext.Current.Request.Cookies["AdminInfo"].Values["ID"].ToInt();
            List<Expression> expression = new List<Expression>();
            expression.Add(new Expression("AdminUserID", "=", AdminUserID.ToString()));
            List<TB_Admin_UserInRole> userInRoles = AdminService.UserInRoleService.Search(expression);
            //循环用户角色
            foreach (TB_Admin_UserInRole userRole in userInRoles)
            {
                //通过角色查询出角色对应的资源
                List<Expression> expression2 = new List<Expression>();
                expression2.Add(new Expression("RoleID", "=", userRole.RoleID.ToString()));
                IEnumerable<int> roleInfoResources = AdminService.RoleInResourcesService.Search(expression2).Select(r => r.ResourceID);

                resourcesIds.AddRange(roleInfoResources);
            }

            List<TB_Admin_Resources> list = new List<TB_Admin_Resources>();
            string result = String.Empty;

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/PermissionDictionaries.xml"));
            XmlNodeList xnl = xmldoc.SelectSingleNode("Dictionaries").ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                if (IsShowMenu == true)
                {
                    if (xn.Attributes["isShow"] == null)
                    {
                        continue;
                    }
                    if (xn.Attributes["isShow"].Value.ToLower() == "false")
                    {
                        continue;
                    }
                }

                if (resourcesIds.Contains(xn.Attributes["ID"].Value.ToInt().ToInt()))
                {                    
                    TB_Admin_Resources entity = new TB_Admin_Resources();
                    entity.ID = xn.Attributes["ID"].Value.ToInt();
                    entity.ResourceName = xn.Attributes["Name"].Value;
                    entity.ChildTree = new List<TB_Admin_Resources>();
                    entity.IsShow = (xn.Attributes["IsShow"].Value.ToLower() == "true" ? true : false);

                    XmlNodeList childlist = xn.ChildNodes;
                    foreach (XmlNode child in childlist)
                    {
                        if (IsShowMenu == true)
                        {
                            if (child.Attributes["isShow"] == null)
                            {
                                continue;
                            }
                            if (child.Attributes["isShow"].Value.ToLower() == "false")
                            {
                                continue;
                            }
                        }

                        if (resourcesIds.Contains(child.Attributes["ID"].Value.ToInt().ToInt()))
                        {
                            TB_Admin_Resources entity2 = new TB_Admin_Resources();
                            entity2.ID = child.Attributes["ID"].Value.ToInt();
                            entity2.ResourceName = child.InnerText;
                            entity2.Url = child.Attributes["Url"].Value;
                            entity2.IsShow = child.Attributes["IsShow"] == null ? false : (child.Attributes["IsShow"].Value.ToLower() == "true" ? true : false);
                            entity.ChildTree.Add(entity2);
                        }
                    }

                    list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 核查权限
        /// </summary>
        public void CheckPermission()
        {

            List<TB_Admin_Resources> list = GetUserPermission(false);

            bool isOk = false;
            foreach (TB_Admin_Resources TB_Admin_Resources in list)
            {
                foreach (TB_Admin_Resources TB_Admin_Resources2 in TB_Admin_Resources.ChildTree)
                {
                    string url = System.Web.HttpContext.Current.Request.Url.ToString();
                    if (url.Contains(TB_Admin_Resources2.Url))
                    {
                        isOk = true;
                    }
                }
            }

            if (isOk == false)
            {
                HttpContext.Current.Response.Redirect(AppPath + "Admin/Tishi.aspx");
            }
        }
    }
