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
using YK.Common;
using YK.Service;

public partial class Admin_SystemModel_Admin_ResourceList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {         
        ButtonDelete.Attributes.Add("onclick", "return confirm('确认删除这条信息吗？')");
        if (!IsPostBack)
        {            
            ViewState["PageIndex"] = 1;
            ViewState["search"] = "";    
            LoadDataBind();       
        }
    }

     //加载所有信息
    public void LoadDataBind()
    {
        int pageSize = AspNetPager1.PageSize;
        int recordCount = 0;
        List<Expression> expression = new List<Expression>();
        expression.Add(new Expression("ParentID","=","0"));
        List<TB_Admin_Resources> list=AdminService.ResourcesService.Search(pageSize, 1, expression,"OrderBy asc", ref recordCount);
        RepList.DataSource = CommonClass.EntityListToDataTable<TB_Admin_Resources>(list); 
        RepList.DataBind();        
    }

    //删除
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in RepList.Items)
        {
            CheckBox cb = ((CheckBox)ri.FindControl("CheckBoxChoose"));
            int ID = Convert.ToInt32(((HiddenField)ri.FindControl("HiddenFieldID")).Value);
            if (cb.Checked == true)
            {
                AdminService.RoleInResourcesService.Delete(d => d.ResourceID == ID);
                AdminService.ResourcesService.Delete(ID);
            }
        }
        //重新加载
        LoadDataBind();
    }

    //分页
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        LoadDataBind();
    }

    //绑定事件
    protected void RepList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string ID = ((HiddenField)e.Item.FindControl("HiddenFieldID")).Value;
        Repeater rep = (Repeater)e.Item.FindControl("RepChildList");
        List<Expression> expression = new List<Expression>();
        expression.Add(new Expression("ParentID", "=", ID));
        rep.DataSource = AdminService.ResourcesService.Search(expression, "orderBy asc");
        rep.DataBind(); 
    }

    //绑定事件
    protected void RepChildList_ItemDataBound(object sender, RepeaterCommandEventArgs e)
    {
        int id = e.CommandArgument.ToInt();
        AdminService.RoleInResourcesService.Delete(d => d.ResourceID == id);
        AdminService.ResourcesService.Delete(id);
        LoadDataBind();
    }
}
