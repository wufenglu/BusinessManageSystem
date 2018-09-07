using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Controls_ReportChart : System.Web.UI.UserControl
{
    /// <summary>
    /// 标题
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public string data { get; set; }
    /// <summary>
    /// 总数
    /// </summary>
    public int count { get; set; }
    /// <summary>
    /// x轴坐标
    /// </summary>
    public string xTitle { get; set; }
    /// <summary>
    /// y轴坐标
    /// </summary>
    public string yTitle { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(xTitle))
        {
            xTitle = "日期";
        }
        if (string.IsNullOrEmpty(yTitle))
        {
            yTitle = "数量";
        }
    }
}