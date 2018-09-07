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
using YK.Service;
using YK.Model;

public partial class Admin_Controls_CategoryView : System.Web.UI.UserControl
{
    private string tableName;
    public string TableName
    {
        get { return tableName; }
        set { tableName = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDataBind();
        }
    }

    private string jiantou = "";//记录箭头

    //加载
    public void LoadDataBind()
    {

       int classID = CommonClass.ReturnRequestInt("classID",0);
        List<Expression> express = new List<Expression>() { 
            new Expression("ParentID","=",classID.ToStr()),
            new Expression("IsDelete","=","0")
        };
        List<TB_Product_Categorys> list = ProductService.CategoryService.Search(express, "OrderBy asc");
        foreach (TB_Product_Categorys model in list)
        {
            ListItem li = new ListItem();
            li.Text = model.CategoryName;
            li.Value = model.ID.ToStr();
            DDLCategory.Items.Add(li);

            //获取子类
            GetChildClass(model.ID);
        }           


        //添加项
        ListItem lit = new ListItem();
        lit.Value = classID.ToString();
        lit.Text = "根栏目";
        lit.Selected = true;
        DDLCategory.Items.Insert(0, lit);

        if (Request.QueryString["id"] != null)
        {
            int id;
            if (int.TryParse(Request.QueryString["id"].ToString(), out id))
            {
                List<Expression> express2 = new List<Expression>() { 
                    new Expression("ID","=",id.ToStr())
                };
                TB_Product_Categorys model = ProductService.CategoryService.Get(express2);
                DDLType.SelectedValue = model.TypeID.ToStr();
                DDLCategory.SelectedValue = model.ParentID.ToStr();
                TbCategoryName.Text = model.CategoryName;
                TbOrder.Text = model.OrderBy.ToStr();
                FileUploadImg.Url = model.PicUrl;
                TbDescription.Text = model.Description;
                CheckBoxIsHidden.Checked = model.IsHidden;
                DDLVouch.SelectedValue = model.VouchType.ToStr();
                ViewState["id"] = id;
            }
        }
    }

    //保存
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        TB_Product_Categorys model = new TB_Product_Categorys();
        model.CategoryName = TbCategoryName.Text.Trim();
        model.ParentID = DDLCategory.SelectedValue.ToInt();
        model.TypeID = DDLType.Text.ToInt();
        model.OrderBy = TbOrder.Text.ToInt();
        model.PicUrl =  FileUploadImg.Url;
        model.Description = TbDescription.Text;
        model.IsHidden = CheckBoxIsHidden.Checked;
        model.VouchType = DDLVouch.SelectedValue.ToInt();
        model.Creater = BasePage.AdminUserName;
        model.AddDate = DateTime.Now;
        if (ViewState["id"] == null)
        {
            ProductService.CategoryService.Insert(model);
        }
        else
        {
            List<Expression> express = new List<Expression>() { 
                new Expression("ID","=",ViewState["id"].ToStr())
            };
            ProductService.CategoryService.Update(model,express);
        }
        MessageDiv.InnerHtml = CommonClass.Reload("数据保存成功");
    }

    //获取子类
    protected void GetChildClass(int parentID)
    {
        jiantou += "----";

        List<Expression> express = new List<Expression>() { 
            new Expression("ParentID","=",parentID.ToStr()),
            new Expression("IsDelete","=","0")
        };
        List<TB_Product_Categorys> list = ProductService.CategoryService.Search(express, "OrderBy asc");
        foreach (TB_Product_Categorys model in list)
        {
            ListItem li = new ListItem();
            li.Text = jiantou + model.CategoryName;
            li.Value = model.ID.ToStr();
            DDLCategory.Items.Add(li);

            //再次查询子类
            GetChildClass(model.ID);
        }

        //每推出一次循环jiantou减少两个>>符号
        jiantou = jiantou.Substring(0, jiantou.Length - 4);
    }
}
