using System;
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