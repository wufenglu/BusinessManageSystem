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

public partial class Admin_Controls_CategoryList : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDataBind();
        }
    }

    private string jiantou = "";//记录箭头

    DataTable dt = new DataTable();//记录类别信息，绑定到Repeater，采用递归方式记录

    protected int cid;

    //加载
    public void LoadDataBind()
    {
        //创建列
        DataColumn column1 = new DataColumn("ID",typeof(string));
        DataColumn column2 = new DataColumn("CategoryName", typeof(string));
        DataColumn column3 = new DataColumn("ParentID", typeof(string));
        DataColumn column4 = new DataColumn("OrderBy", typeof(string));
        DataColumn column5 = new DataColumn("IsVouch", typeof(string));
        DataColumn column6 = new DataColumn("IsHidden", typeof(string));
        DataColumn column7 = new DataColumn("AddDate", typeof(string));
        DataColumn column8 = new DataColumn("Type", typeof(string));

        //添加列
        dt.Columns.Add(column1);
        dt.Columns.Add(column2);
        dt.Columns.Add(column3);
        dt.Columns.Add(column4);
        dt.Columns.Add(column5);
        dt.Columns.Add(column6);
        dt.Columns.Add(column7);
        dt.Columns.Add(column8);

       int cid = CommonClass.ReturnRequestInt("cid",0);
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
            dr["Type"] = model.TypeID == 0 ? "普通" : "团购";
            //添加行
            dt.Rows.Add(dr);

            jiantou = "";//记录级别

            //获取子类
            GetChildClass(model.ID);
        }           

        RepList.DataSource = dt;
        RepList.DataBind();
    }

    //删除
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in RepList.Items)
        {
            CheckBox cb = ((CheckBox)ri.FindControl("CheckBoxChoose"));
            string ID = ((HiddenField)ri.FindControl("HiddenFieldID")).Value;
            if (cb.Checked == true)
            {
                List<Expression> express = new List<Expression>() { 
                        new Expression("ID","=",ID)
                    };
                TB_Product_Categorys model = ProductService.CategoryService.Get(express);
                model.IsDelete = true;
                ProductService.CategoryService.Update(model, express);
            }
        }
        Response.Write("<script>window.location='"+Request.Url.ToString()+"';</script>");
    }

    //状态设置
    protected void DDLVouchSet_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in RepList.Items)
        {
            CheckBox cb = ((CheckBox)ri.FindControl("CheckBoxChoose"));
            string ID = ((HiddenField)ri.FindControl("HiddenFieldID")).Value;
            if (cb.Checked == true)
            {
                List<Expression> express = new List<Expression>() { 
                        new Expression("ID","=",ID)
                    };
                TB_Product_Categorys model = ProductService.CategoryService.Get(express);
                model.VouchType = DDLVouchSet.SelectedValue.ToInt();
                ProductService.CategoryService.Update(model,express);
            }
        }
        Response.Write("<script>window.location='" + Request.Url.ToString() + "';</script>");
    }

    //显示设置
    protected void DDLIsHiddenSet_Click(object sender, EventArgs e)
    {

        foreach (RepeaterItem ri in RepList.Items)
        {
            CheckBox cb = ((CheckBox)ri.FindControl("CheckBoxChoose"));
            string ID = ((HiddenField)ri.FindControl("HiddenFieldID")).Value;
            if (cb.Checked == true)
            {
                List<Expression> express = new List<Expression>() { 
                        new Expression("ID","=",ID)
                    };
                TB_Product_Categorys model = ProductService.CategoryService.Get(express);
                model.IsHidden = CheckBoxIsHiddenSet.Checked;
                ProductService.CategoryService.Update(model, express);
            }
        }
        Response.Write("<script>window.location='" + Request.Url.ToString() + "';</script>");
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
            dr["Type"] = model.TypeID == 0 ? "普通" : "团购";
            //添加行
            dt.Rows.Add(dr);

            //再次查询子类
            GetChildClass(model.ID);
        }

        //每推出一次循环jiantou减少两个>>符号
        jiantou = jiantou.Substring(0, jiantou.Length - 4);
    }
}
