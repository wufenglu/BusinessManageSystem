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

using YK.Common;
using YK.Model;

public partial class Admin_main : BasePage
{
    public List<TB_Admin_Resources> resources = new List<TB_Admin_Resources>();

    protected void Page_Load(object sender, EventArgs e)
    {
      
        LblLogIP.Text = Request.UserHostAddress;
        LblFbl.Text =Request.Browser.Browser+Request.Browser.Version;
        LblRealName.Text = AdminUserName;

        if (!IsPostBack)
        {
            //RepInfoList.DataSource = YK.Service.EmployeeService.InfoCategoryService.Search();
            //RepInfoList.DataBind();
        }

        PermissionHelpers permission = new PermissionHelpers();
        resources = permission.Permission(true);
    }

    protected void RepInfoList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int CategoryID = ((HiddenField)e.Item.FindControl("CategoryID")).Value.ToInt();
        Repeater rep = (Repeater)e.Item.FindControl("RepList");

        List<Expression> expression = new List<Expression>();
        expression.Add(new Expression("CategoryID","=",CategoryID));

        //rep.DataSource = YK.Service.EmployeeService.InfoService.Search(5, expression);
        //rep.DataBind();
    }
}
