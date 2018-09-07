<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrgInResource.aspx.cs" Inherits="Admin_AdminModel_User_RoleInResource" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<%@ Import Namespace="YK.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <yk:AdminHeader ID="AdminHeader1" runat="server"></yk:AdminHeader>
</head>
<body>
    <form id="form1" runat="server">
        <div class="BodyDiv"> 
            <div  class="navigation">               
               <a href="ColumnInforList.aspx">角色权限管理</a> > 角色权限列表
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>    
                    <table class="fromTable">
                        <tr>          
                            <th>模块列表：</td>
                            <td>
                                <style>
                                    #CheckBoxRolesList td{ border:0px;}
                                </style>
                                <asp:CheckBoxList ID="CheckBoxResourceList" runat="server" AutoPostBack="true"
                                    onselectedindexchanged="CheckBoxResourceList_SelectedIndexChanged">
                                </asp:CheckBoxList>
                             </td>
                           </tr>
                    </table>  
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="SubmitClass">
                <asp:Button ID="BtnSave" runat="server" Text=" 保存 " CssClass="buttonCss"  OnClick="BtnSave_Click"/>
                 <input type="button" value=" 取消 " class="buttonCss" id="cancleCss" onclick="closeLayer()"  />
            </div>  
            <div runat="Server" id="MessageDiv">            
            </div> 
    </form>
</body>
</html>