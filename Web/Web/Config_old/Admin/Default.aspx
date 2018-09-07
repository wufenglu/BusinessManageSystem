<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>综合管理系统</title>
    <link type="text/css" href="css/Yk.Core.css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
        var entityList = eval('<%=resourcesJson%>');
    </script>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/yk.core.common.js"></script>
    <script type="text/javascript" src="js/yk.core.layer.js"></script>
</head>
<body>
    <div class="topTool">
        <a href="LoginOut.aspx">退出登陆</a>
    </div>
    <div class="parentMenuMain">
        <div class="logo" style="float:left;">
            <img onclick="main.location='main.aspx'" src="images/logo.jpg">
        </div>
        <div id="parentMenu" class="parentMenu"></div> 
        <div class="removeFloatDiv"></div>
    </div>      
    <div id="childMenu" class="childMenu"></div>
    <div class="titlesDiv">
        <div id="titles" class="titles"></div>
        <div style="clear: both;"></div>
    </div>
    <div id="contentDivs">
    </div>
    <script type="text/javascript">
        yk.core.layer.add("首页", "main.aspx", "0");
    </script>
</body>
</html>
