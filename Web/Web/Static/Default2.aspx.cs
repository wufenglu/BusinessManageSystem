using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssTest;
using System.Reflection;
using YK.Core;
using YK.Core.Pager;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ServiceContainer.Register<Pager, IPager>();
        IPager service = ServiceContainer.GetService<IPager>();

        Response.Write(service.GetType().FullName);
    }
}

namespace AssTest
{
    public interface Interface2
    {

    }
}