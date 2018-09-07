using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YK.Model;
using YK.Common;

public partial class Admin_Default : YK.Common.BasePage
{
    public string resourcesJson = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        PermissionHelpers permission = new PermissionHelpers();
        List<TB_Admin_Resources> resources = permission.Permission(true);
        resourcesJson = JsonHelper.GetJSON<List<TB_Admin_Resources>>(resources);
    }
}