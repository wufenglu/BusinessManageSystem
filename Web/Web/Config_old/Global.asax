<%@ Application Language="C#" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Security" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 在应用程序启动时运行的代码
        Application["AccessCount"] = 10000;

        log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

        //核查实体对应表
        //YK.Common.EntityReflectionDataBase exist = new YK.Common.EntityReflectionDataBase();
        //exist.StartReflection();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码 
        HttpContext Context = ((HttpApplication)sender).Context;
        foreach (Exception ex in Context.AllErrors)
        {         
            YK.Common.TxtFileHelper.AppendLogTxt(ex.Message);
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码
        Application.Lock();
        Application["AccessCount"] = Convert.ToInt32(Application["AccessCount"]) + 1;
        Application.UnLock();
    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。
        //Application.Lock();
        //Application["AccessCount"] = Convert.ToInt32(Application["AccessCount"]) - 1;
        //Application.UnLock();
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        HttpApplication appl = sender as HttpApplication;
        HttpContext context = appl.Context;
        if (context.Request.IsAuthenticated == true)
        {
            //窗体标识
            FormsIdentity identity = context.User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = identity.Ticket;
            string[] role = ticket.UserData.Split(',');
            context.User = new GenericPrincipal(identity, role);
        }
    }
       
</script>
