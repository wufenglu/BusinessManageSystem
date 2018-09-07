using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YK.Common;
using YK.Service;
using YK.Model;

public partial class Admin_Controls_ProductCategory : System.Web.UI.UserControl
{
    /// <summary>
    /// 类别编号
    /// </summary>
    private int categoryId;

    /// <summary>
    /// 类别编号
    /// </summary>
    public int CategoryId { 
        get {
            if (!string.IsNullOrEmpty(DDLThreeLevel.SelectedValue))
            {
                return DDLThreeLevel.SelectedValue.ToInt();
            }
            return 0;    
        }
        set {
            categoryId = value;
        }
    }

    /// <summary>
    /// 是否验证
    /// </summary>
    public bool IsValidatior = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategory();

            //当categoryId不为空时
            if (categoryId > 0)
            {
                TB_Product_Categorys entity = ProductService.CategoryService.Get(categoryId);
                TB_Product_Categorys parentEntity = ProductService.CategoryService.Get(entity.ParentID);                
                GetTwoCategory(parentEntity.ParentID);
                GetThreeCategory(parentEntity.ID);

                DDLOneLevel.SelectedValue = parentEntity.ParentID.ToStr();
                DDLTwoLevel.SelectedValue = parentEntity.ID.ToStr();
                DDLThreeLevel.SelectedValue = entity.ID.ToStr();
            }
        }
    }

    /// <summary>
    /// 绑定类别
    /// </summary>
    public void BindCategory()
    {
        List<Expression> expression = new List<Expression>();
        expression.Add(new Expression("ParentId", "=", "0"));
        DDLOneLevel.DataSource = ProductService.CategoryService.Search(expression).OrderBy(c => c.OrderBy);
        DDLOneLevel.DataTextField = "CategoryName";
        DDLOneLevel.DataValueField = "ID";
        DDLOneLevel.DataBind();

        ListItem li = new ListItem();
        li.Text = "==请选择==";
        li.Value = "";
        DDLOneLevel.Items.Insert(0, li);
    }

    protected void DDLOneLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTwoCategory(DDLOneLevel.SelectedValue.ToInt());
        DDLThreeLevel.Items.Clear(); 
    }

    protected void DDLTwoLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetThreeCategory(DDLTwoLevel.SelectedValue.ToInt());
    }

    /// <summary>
    /// 获取二级类别
    /// </summary>
    public void GetTwoCategory(int parentId)
    {
        DDLTwoLevel.Items.Clear();

        if (parentId > 0)
        {
            List<Expression> expression = new List<Expression>();
            expression.Add(new Expression("ParentId", "=", parentId));
            DDLTwoLevel.DataSource = ProductService.CategoryService.Search(expression).OrderBy(c => c.OrderBy);
            DDLTwoLevel.DataTextField = "CategoryName";
            DDLTwoLevel.DataValueField = "ID";
            DDLTwoLevel.DataBind();
        }
        ListItem li = new ListItem();
        li.Text = "==请选择==";
        li.Value = "";
        DDLTwoLevel.Items.Insert(0, li);
    }

    /// <summary>
    /// 获取三级类别
    /// </summary>
    public void GetThreeCategory(int parentId)
    {
        DDLThreeLevel.Items.Clear();

        if (parentId > 0)
        {
            List<Expression> expression = new List<Expression>();
            expression.Add(new Expression("ParentId", "=", parentId));
            DDLThreeLevel.DataSource = ProductService.CategoryService.Search(expression).OrderBy(c => c.OrderBy);
            DDLThreeLevel.DataTextField = "CategoryName";
            DDLThreeLevel.DataValueField = "ID";
            DDLThreeLevel.DataBind();
        }

        ListItem li = new ListItem();
        li.Text = "==请选择==";
        li.Value = "";
        DDLThreeLevel.Items.Insert(0, li);
    }
}