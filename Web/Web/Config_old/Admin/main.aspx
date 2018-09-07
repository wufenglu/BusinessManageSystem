<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="Admin_main" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <yk:AdminHeader ID="AdminHeader1" runat="server"></yk:AdminHeader>

	<script language="javascript" type="text/javascript">
	    //详细
	    function details(id) {
	        showLayer('详细', 'SystemModel/Employee/Info/InfoDetails.aspx?id=' + id, 900, 600);
	    }
	</script>
    <style>
        .info{ float:left; width:40%; color:Gray; margin-left:5%; line-height:25px;}
        .infoTitle{ border-bottom:1px solid red;}
        .info a{ color:Gray;}
        .info a:hover{ color:red;}
        .menuDiv{ width:160px; height:80px; line-height:80px; float:left; margin:10px; 
                 color:White; text-align:center; font-weight:bold; font-size:16px; vertical-align:middle;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
       <%-- <div class="navigation">           
             当前位置 >> 首页
        </div>--%>
        <table cellspacing="0" cellpadding="0" width="90%" align="center" border="0">
              <tr>
                    <td align=center width=100><img height="100" src="images/admin_p.gif" width="90"></td>
                    <td width="60">&nbsp;</td>
                    <td>
                      <table height="150" cellspacing="0" cellpadding="0" width="100%" border="0">   
                            <tr>
                              <td colspan="2" ><h3>欢迎进入管理员中心 ！</h3></td>
                            </tr>         
                            <tr>
                                <td  style="width:120px;">
                                    当前时间 ：
                                </td>
                                <td><span id="SpTime"></span></td>
                             </tr>                            
                            <tr>
                                <td>当前用户 ：</td>
                                <td style="COLOR: #880000">
                                    <asp:Label ID="LblRealName" runat="server" Text=" "></asp:Label>
                                </td>
                             </tr>
                            <tr>
                                <td>登录IP ：</td>
                                <td style="COLOR: #880000"><asp:Label ID="LblLogIP" runat="server" Text=" "></asp:Label></td>
                            </tr>
                            <tr>
                                <td>浏览器版本 ：</td>
                                <td style="COLOR: #880000"><asp:Label ID="LblFbl" runat="server" Text=" "></asp:Label></td>
                          </tr>
                      </table>
                  </td>
              </tr>
        </table>    
        <div style=" clear:both;"></div>
        <br />
        <yk:Repeater runat="server" ID="RepInfoList" 
            onitemdatabound="RepInfoList_ItemDataBound">
            <ItemTemplate>
                <div class="info">
                    <asp:HiddenField runat="server" ID="CategoryID" Value='<%# Eval("ID")%>' />
                    <div class="infoTitle"><%# Eval("Name")%></div>
                    <ul>                        
                        <yk:Repeater runat="server" ID="RepList">
                            <ItemTemplate>
                                <li><a href="javascript:details(<%# Eval("ID")%>)"><%# Eval("Title")%></a></li>
                            </ItemTemplate>
                        </yk:Repeater>
                    </ul>
                </div> 
            </ItemTemplate>
        </yk:Repeater>
            
    </div>
    </form>
    <script language="javascript" type="text/javascript">
		function Getdate()
		{
			var dateTime=new Date();
			document.getElementById("SpTime").innerText=dateTime.getFullYear()+"年"+(dateTime.getMonth()+1)+"月"+dateTime.getDate()+"日 "+ dateTime .getHours()+'：'+dateTime.getMinutes()+'：'+dateTime .getSeconds();
			setTimeout ("Getdate()",1000);
		}
		Getdate();
    </script>
</body>
</html>