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
using YK.Interface;
using YK.Common;

public partial class Admin_SystemModel_Admin_ResourceView : BasePage
{
    //用于添加子栏目
    public int columnID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绑定模块信息
            LoadModel();
            //加载
            LoadDataBind();
        }
    }

    /// <summary>
    /// 绑定模块信息
    /// </summary>
    public void LoadModel()
    {
        List<Expression> expression = new List<Expression>();
        expression.Add(new Expression("ParentID", "=", "0"));

        DDLParent.DataSource = AdminService.ResourcesService.Search(expression," OrderBy asc");
        DDLParent.DataTextField = "ResourceName";
        DDLParent.DataValueField = "ID";
        DDLParent.DataBind();

        ListItem liP = new ListItem();
        liP.Text = "==根目录==";
        liP.Value = "0";
        DDLParent.Items.Insert(0, liP);
    }

    //加载
    public void LoadDataBind()
    {
        TB_Admin_Resources model = new TB_Admin_Resources();
        int id = CommonClass.ReturnRequestInt("id", 0);
        
        if ( id > 0)
        {
            model = AdminService.ResourcesService.Get(id);
            DDLParent.SelectedValue = model.ParentID.ToStr();
            TbResourceName.Text = model.ResourceName;
            TbUrl.Text = model.Url;
            TbOrderBy.Text = model.OrderBy.ToStr();
            CheckIsShow.Checked = model.IsShow;
            ViewState["id"] = model.ID;
        }
    }

    //保存
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        TB_Admin_Resources model = new TB_Admin_Resources();
        if (ViewState["id"] != null)
        {
            model = AdminService.ResourcesService.Get(ViewState["id"]);
        }

        model.ResourceName = TbResourceName.Text;
        model.ParentID = DDLParent.SelectedValue.ToInt();
        model.Url = TbUrl.Text;
        model.Creater = AdminUserName;
        model.OrderBy = TbOrderBy.Text.ToInt();
        model.IsShow = CheckIsShow.Checked;
        model.AddDate = DateTime.Now;

        IAdmin_Resources Resources = AdminService.ResourcesService;
        if (ViewState["id"] == null)
        {
            if (Resources.Insert(model) == 1)
            {
                CachesHelper.RemoveCache(DataToCacheHelper.CacheNames.AllResources.ToStr());
                MessageDiv.InnerHtml = CommonClass.Reload("数据修改成功");
            }
            else
            {
                MessageDiv.InnerHtml = CommonClass.Alert("数据添加失败");
            }
        }
        else
        {
            if (Resources.Update(model) == 1)
            {
                CachesHelper.RemoveCache(DataToCacheHelper.CacheNames.AllResources.ToStr());
                MessageDiv.InnerHtml = CommonClass.Reload("数据修改成功");
            }
            else
            {
                MessageDiv.InnerHtml = CommonClass.Alert("数据修改失败");
            }           
        }
    }
}
