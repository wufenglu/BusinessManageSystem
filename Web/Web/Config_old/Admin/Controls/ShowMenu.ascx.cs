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

public partial class Admin_Controls_ShowMenu : System.Web.UI.UserControl
{
    private int value;

    /// <summary>
    /// 值
    /// </summary>
    public int Value
    {
        get
        {
            if (DDLMenu.SelectedValue != string.Empty)
            {
                return DDLMenu.SelectedValue.ToInt();
            }
            return 0;
        }
        set {
            this.value = value;
        }
    }

    private string name;
    /// <summary>
    /// 名称
    /// </summary>
    public string Name
    {
        get
        {
            if (DDLMenu.Text != string.Empty)
            {
                return DDLMenu.SelectedItem.Text;
            }
            return "";
        }
        set
        {
            name = value;
        }
    }

    /// <summary>
    /// 名称字段
    /// </summary>
    public string NameField { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// 是否验证
    /// </summary>
    public bool IsValidator { get; set; }

    /// <summary>
    /// 提示文字
    /// </summary>
    public string ToolTip { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDataBind();

            if (value != 0)
            {
                DDLMenu.SelectedValue = value.ToStr();
            }
        }

        if (IsValidator == true)
        {
            DDLMenu.GroupName = "Validator";
            DDLMenu.LabelID = LbMenu.ClientID;
            DDLMenu.ToolTip = ToolTip;
        }
    }

    private string jiantou = "";//记录箭头

    DataTable dt = new DataTable();//记录类别信息，绑定到Repeater，采用递归方式记录

    //加载
    public void LoadDataBind()
    {
        string cmdText = "select * from " + TableName + " where ParentID=0 order by OrderBy asc";

        DataTable list = YK.Core.SqlHelper.SqlConvertHelper.GetInstallSqlHelper().GetDataSet(cmdText).Tables[0];
        foreach (DataRow dr in list.Rows)
        {

            ListItem li = new ListItem();
            li.Value = dr["ID"].ToStr();
            li.Text = dr[NameField].ToStr();

            DDLMenu.Items.Add(li);

            jiantou = "";//记录级别

            //获取子类
            GetChildClass(dr["ID"].ToInt());
        }

        ListItem li2 = new ListItem();
        li2.Text = "==请选择==";
        li2.Value = "0";
        DDLMenu.Items.Insert(0, li2);
    }

    //获取子类
    protected void GetChildClass(int parentID)
    {
        jiantou += "---";

        string cmdText = "select * from " + TableName + " where ParentID="+parentID+" order by OrderBy asc";

        DataTable list = YK.Core.SqlHelper.SqlConvertHelper.GetInstallSqlHelper().GetDataSet(cmdText).Tables[0];
        if (list.Rows.Count > 0)
        {
            foreach (DataRow dr in list.Rows)
            {
                ListItem li = new ListItem();
                li.Value = dr["ID"].ToStr();
                li.Text = jiantou + dr[NameField].ToStr();

                DDLMenu.Items.Add(li);

                //再次查询子类
                GetChildClass(dr["ID"].ToInt());
            }
        }

        //每推出一次循环jiantou减少两个>>符号
        jiantou = jiantou.Substring(0, jiantou.Length - 3);
    }
}
