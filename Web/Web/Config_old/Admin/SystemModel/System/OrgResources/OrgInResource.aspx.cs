using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using YK.Model;
using YK.Service;
using YK.Common;

public partial class Admin_AdminModel_User_RoleInResource : BasePage
{
    public int classID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            LoadDataBind();            
        }
    }

    //加载
    public void LoadDataBind()
    {
        int id = CommonClass.ReturnRequestInt("id",0);
        ViewState["id"] = id;
        List<Expression> expression = new List<Expression>();
        expression.Add(new Expression("ParentID", "=", "0"));
        List<TB_Admin_Resources> list= AdminService.ResourcesService.Search( expression, " OrderBy asc");

        foreach (TB_Admin_Resources model in list)
        {
            ListItem item = new ListItem();
            item.Text = model.ResourceName;
            item.Value = model.ID.ToStr();
            CheckBoxResourceList.Items.Add(item);

            List<Expression> expression2 = new List<Expression>();
            expression2.Add(new Expression("ParentID", "=", model.ID.ToStr()));
            List<TB_Admin_Resources> list2 = AdminService.ResourcesService.Search(expression2, " OrderBy asc");
            foreach (TB_Admin_Resources model2 in list2)
            {
                ListItem item2 = new ListItem();
                item2.Text = model2.ResourceName;
                item2.Value = model2.ID.ToStr();
                item2.Attributes.Add("style", "margin-left:20px;");
                CheckBoxResourceList.Items.Add(item2);
            }
        }

        //获取角色对应的资源
        List<Expression> express = new List<Expression>();
        express.Add(new Expression("RoleID", "=", id.ToStr()));
        List<TB_Admin_RoleInResources> ds = AdminService.RoleInResourcesService.Search(express);
        //选取管理员角色
        foreach (ListItem li in CheckBoxResourceList.Items)
        {
                foreach(TB_Admin_RoleInResources model in ds)
                {
                    if (li.Value == model.ResourceID.ToStr())
                    {
                        li.Selected = true;
                    }
                }
        }
    }

    //保存
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        TB_Admin_RoleInResources model = new TB_Admin_RoleInResources();
        model.RoleID = ViewState["id"].ToInt();

        //获取角色对应的资源
        List<Expression> express = new List<Expression>();
        express.Add(new Expression("RoleID", "=", model.RoleID.ToStr()));
        int recordCount = 0;
        List<TB_Admin_RoleInResources> ds = AdminService.RoleInResourcesService.Search(express);
        List<int> sources = new List<int>();
        foreach (TB_Admin_RoleInResources entity in ds)
        {
            //加入泛型数组中
            sources.Add(entity.RoleID.ToInt());
        }

        List<Expression> express2 = new List<Expression>();
        express2.Add(new Expression("RoleID", "=", model.RoleID.ToStr()));

        //如果不选择，可能管理员角色列表中存在，就将其删除
        AdminService.RoleInResourcesService.Delete(express2);

        foreach (ListItem li in CheckBoxResourceList.Items)
        {
            if (li.Selected == true)
            {
                model.ResourceID = li.Value.ToInt();
                AdminService.RoleInResourcesService.Insert(model);
            }
        }

        MessageDiv.InnerHtml = CommonClass.Reload("数据保存成功");
    }

    //选择
    protected void CheckBoxResourceList_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in CheckBoxResourceList.Items)
        {
            int id = li.Value.ToInt();
            TB_Admin_Resources model = AdminService.ResourcesService.Get(id);
            //如果存在父级，才进行选择
            if (model.ParentID > 0)
            {
                li.Attributes.Add("style", "margin-left:20px;");
                //当二级被选择，父级也要被选择
                if (li.Selected == true)
                {
                    foreach (ListItem li2 in CheckBoxResourceList.Items)
                    {
                        if (li2.Value.ToInt() == model.ParentID)
                        {
                            li2.Selected = true;
                        }
                    }
                }
            }
        }
    }
}
