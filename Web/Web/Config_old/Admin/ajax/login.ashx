<%@ WebHandler Language="C#" Class="login" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

using YK.Model;
using YK.Service;
using YK.Common;

public class login : IHttpHandler,IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string userName=context.Request["userName"].TrimEnd();
        string userPwd = context.Request["userPwd"].ToEncryptStr(); 
        string verCode = context.Request["verCode"];

        if (context.Session["VerifyCode"] == null)
        {
            context.Response.Write("验证失效，请点击重新获取！");
            return;
        }
        if (context.Session["VerifyCode"].ToStr() != verCode)
        {
            context.Response.Write("验证码错误，请重新输入！");
            return;
        }
        
        List<YK.Model.Expression> express = new List<Expression>();
        express.Add(new Expression("UserName", "=", userName));
        express.Add(new Expression("Userpwd", "=", userPwd));
        TB_Admin_User model = AdminService.UserService.Get(express);
        if (model.ID > 0)
        {
            HttpCookie cookie = new HttpCookie("AdminInfo");
            cookie.Values["ID"] = model.ID.ToStr();
            cookie.Values["UserName"] = userName;
            cookie.Values["UserPwd"] = userPwd;
            cookie.Expires = DateTime.Now.AddDays(1);
            context.Response.Cookies.Add(cookie);
            //操作日志
            AdminService.LogService.Insert(OperationType.用户操作, model.ID, "用户" + model.UserName + "后台登录", userName);
            context.Response.Write("1");
            return;
        }
        else
        {
            context.Response.Write("用户名或密码错误！");
            return;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}