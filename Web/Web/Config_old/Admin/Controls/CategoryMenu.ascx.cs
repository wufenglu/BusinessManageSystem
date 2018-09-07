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
using YK.Model;
using YK.Common;
using System.Collections.Generic;
using YK.Service;

public partial class Admin_Controls_CategoryMenu : System.Web.UI.UserControl
{
   
    private int categoryID;
    public int CategoryID
    {
        get
        {
            if (DDLCategory.SelectedValue != string.Empty)
            {
                return Convert.ToInt32(DDLCategory.SelectedValue);
            }
            return 0;
        }
        set
        {
            categoryID = value;
        }
    }

    private string categoryName;
    public string CategoryName
    {
        get
        {
            if (DDLCategory.Text != string.Empty)
            {
                return DDLCategory.SelectedItem.Text;
            }
            return "";
        }
        set { categoryName = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDataBind();

            if (categoryID != 0)
            {
                DDLCategory.SelectedValue = categoryID.ToStr();
            }
        }
    }

    private string jiantou = "";//记录箭头

    DataTable dt = new DataTable();//记录类别信息，绑定到Repeater，采用递归方式记录

    //加载
    public void LoadDataBind()
    {
        //创建列
        DataColumn column1 = new DataColumn("ID", typeof(string));
        DataColumn column2 = new DataColumn("CategoryName", typeof(string));
        DataColumn column3 = new DataColumn("ParentID", typeof(string));
        DataColumn column4 = new DataColumn("OrderBy", typeof(string));
        DataColumn column5 = new DataColumn("IsVouch", typeof(string));
        DataColumn column6 = new DataColumn("IsHidden", typeof(string));
        DataColumn column7 = new DataColumn("AddDate", typeof(string));

        //添加列
        dt.Columns.Add(column1);
        dt.Columns.Add(column2);
        dt.Columns.Add(column3);
        dt.Columns.Add(column4);
        dt.Columns.Add(column5);
        dt.Columns.Add(column6);
        dt.Columns.Add(column7);

        int cid = CommonClass.ReturnRequestInt("cid", 0);
        List<Expression> express = new List<Expression>() { 
            new Expression("ParentID","=",cid.ToStr()),
            new Expression("IsDelete","=","0")
        };
        List<TB_Product_Categorys> list = ProductService.CategoryService.Search(express, "OrderBy asc");
        foreach (TB_Product_Categorys model in list)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = model.ID.ToStr();
            dr["CategoryName"] = model.CategoryName;
            dr["ParentID"] = model.ParentID.ToStr();
            dr["OrderBy"] = model.OrderBy.ToStr();
            dr["IsVouch"] = model.VouchType.ToStr();
            dr["IsHidden"] = model.IsHidden.ToStr();
            dr["AddDate"] = model.AddDate.ToStr();
            //添加行
            dt.Rows.Add(dr);

            jiantou = "";//记录级别

            //获取子类
            GetChildClass(model.ID);
        }

        DDLCategory.DataSource = dt;
        DDLCategory.DataTextField = "CategoryName";
        DDLCategory.DataValueField = "ID";
        DDLCategory.DataBind();

        ListItem li = new ListItem();
        li.Text = "==请选择==";
        li.Value = "0";
        DDLCategory.Items.Insert(0,li);
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
            DataRow dr = dt.NewRow();
            dr["ID"] = model.ID.ToStr();
            dr["CategoryName"] = jiantou + model.CategoryName;
            dr["ParentID"] = model.ParentID.ToStr();
            dr["OrderBy"] = model.OrderBy.ToStr();
            dr["IsVouch"] = model.VouchType.ToStr();
            dr["IsHidden"] = model.IsHidden.ToStr();
            dr["AddDate"] = model.AddDate.ToStr();
            //添加行
            dt.Rows.Add(dr);

            //再次查询子类
            GetChildClass(model.ID);
        }

        //每推出一次循环jiantou减少两个>>符号
        jiantou = jiantou.Substring(0, jiantou.Length - 4);
    }
}
