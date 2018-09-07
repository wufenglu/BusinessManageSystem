using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using YK.Model;
using YK.Interface;
using YK.Service;
using YK.Common;
using YK.Model.Systems;
using YK.Service.Systems;

public partial class Admin_SystemModel_System_Organizations_Edit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategory();
            LoadDataBind();
        }
    }
                   
    //加载
    public void LoadDataBind()
    {
        int ID = CommonClass.ReturnRequestInt("id", 0);
        if (ID > 0)
        {
            var model = SysOrganizationsService.OrganizationsService.Get(ID);
            if (model.ID.ToInt() > 0)
            {
                ViewState["id"] = model.ID;                
                TbName.Text = model.Name;
                TbCode.Text = model.Code;
                DDLDbType.SelectedValue = model.DbType.ToStr();
                TbServer.Text = model.Server;
                TbDatabaseName.Text = model.DatabaseName;
                TbUserName.Text = model.UserName;
                TbPassword.Text = model.Password;
                TbPort.Text = model.Port;
                CheckBoxState.Checked = model.State == 0 ? true : false;
            }
        }
    }

    public void BindCategory()
    {
        List<object> dbTypes = new List<object>();
        dbTypes.Add(new{ name="SqlServer",value= "SqlServer" });
        dbTypes.Add(new { name = "MySql", value = "MySql" });
        dbTypes.Add(new { name = "Oracle", value = "Oracle" });

        DDLDbType.DataSource = dbTypes;
        DDLDbType.DataTextField = "name";
        DDLDbType.DataValueField = "value";
        DDLDbType.DataBind();
    }


    //保存
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        TB_Sys_Organizations model = new TB_Sys_Organizations();
        if (ViewState["id"] != null)
        {
            model = SysOrganizationsService.OrganizationsService.Get(ViewState["id"]);
        }
        model.DbType = DDLDbType.SelectedValue;
        model.Name = TbName.Text.Trim();
        model.Code = TbCode.Text.Trim();
        model.Server = TbServer.Text.Trim();
        model.DatabaseName = TbDatabaseName.Text.Trim();
        model.UserName = TbUserName.Text.Trim();
        model.Password = TbPassword.Text.Trim();
        model.Port = TbPort.Text.Trim();
        model.State = CheckBoxState.Checked ? 0 : 1;
        model.Creater = AdminUserName;
        model.CreatedOn = DateTime.Now;

        if (ViewState["id"] == null)
        {
            if (SysOrganizationsService.OrganizationsService.Insert(model) == 1)
            {
                MessageDiv.InnerHtml = CommonClass.Reload("数据添加成功");
            }
            else
            {
                MessageDiv.InnerHtml = CommonClass.Alert("数据添加失败");
            }
        }
        else
        {
            if (SysOrganizationsService.OrganizationsService.Update(model) == 1)
            {

                MessageDiv.InnerHtml = CommonClass.Reload("数据修改成功");
            }
            else
            {
                MessageDiv.InnerHtml = CommonClass.Alert("数据修改失败");
            }
        }
    }
    
}
