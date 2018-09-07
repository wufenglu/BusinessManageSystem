<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminHeader.ascx.cs" Inherits="Admin_Controls_AdminHeader" %>
<%@ Import Namespace="YK.Common" %>

<% 
    string[] arr = Request.Url.ToString().Split('/');
    string thisAddress = Request.Url.ToString().Replace(arr[arr.Length - 1], "");
    %>
<script type="text/javascript">
    var thisAddress = "<%=thisAddress %>";    
</script>

<%= "<link href=" + CommonClass.AppPath + "Admin/css/Style.css" + " rel=\"stylesheet\" type=\"text/css\" />"%>

<script language="javascript" type="Text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.js"></script>

<script language="javascript" type="Text/javascript" src="<%=CommonClass.AppPath %>Admin/js/yk.core.common.js"></script>

<script language="javascript" type="Text/javascript" src="<%=CommonClass.AppPath %>Admin/js/yk.core.grid.js"></script>

<script language="javascript" type="Text/javascript" src="<%=CommonClass.AppPath %>Admin/js/yk.core.validator.js"></script>

<script language="javascript" type="Text/javascript" src="<%=CommonClass.AppPath %>Admin/js/yk.win.js"></script>

<script type="text/javascript" src="<%=CommonClass.AppPath %>Admin/My97DatePicker/WdatePicker.js"></script>

<script language="javascript" type="text/javascript"> 
    //弹出层
    function showLayer(title,url,width,height) {
        window.top.yk.core.layer.showLayer({
            title:title, 
            url:thisAddress + url, 
            width:width, 
            height:height
        });
    }    
    //关闭
    function closeLayer(result)
    {
        window.top.yk.core.layer.closeLayer(result);
    }

    $(function () {
        var windowHeight = $(window).height();
        var bodyDivHeight = $(".BodyDiv").height();
        if (bodyDivHeight > windowHeight - 50) {
            $(".BodyDiv").css("height", windowHeight - 50);
            $(".BodyDiv").css("overflow-y", "scroll");
        }
    });
</script>
<style>
        body,div,table,a,p,span{ margin:0px;padding:0px; text-decoration:none; font-size:12px;}
</style>


