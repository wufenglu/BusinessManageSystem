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

public partial class Admin_Controls_Match : System.Web.UI.UserControl
{
    public string newGuid = Guid.NewGuid().ToString().Replace("-", "");
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }
    /// <summary>
    /// 字段名
    /// </summary>
    public string FieldName { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    public string Value { get { return HiddenKey.Value; } }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get { return TbText.Text; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        TbText.Attributes.Add("onkeyup", "matchDiv" + newGuid + "()");
    }
}