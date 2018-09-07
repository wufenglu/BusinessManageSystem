<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceView.aspx.cs" Inherits="Admin_SystemModel_Admin_ResourceView" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <yk:AdminHeader ID="AdminHeader1" runat="server"></yk:AdminHeader>
</head>
<body>
    <form id="form1" runat="server">
        <div class="BodyDiv"> 
        <div  class="navigation">资源管理 >> 资源视图</div>
         <table class="fromTable">
                <tr>          
                    <th>父级：</th>
                    <td>
                        <asp:DropDownList ID="DDLParent" runat="server">
                        </asp:DropDownList>
                    </td>
               </tr>
               <tr>          
                    <th>名称：</th>
                    <td>
                        <asp:TextBox ID="TbResourceName" runat="server" Width="300px"  CssClass="inputCss"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVResourceName" runat="server" ErrorMessage="请输入名称！" ControlToValidate="TbResourceName"></asp:RequiredFieldValidator>
                    </td>
               </tr>
              
               <tr>
                     <th>链接路径：</th>
                    <td>
                        <asp:TextBox ID="TbUrl" runat="server" Width="300px" MaxLength="60"  CssClass="inputCss"></asp:TextBox>
                    </td>
               </tr>                         
               <tr>
                     <th>排序：</th>
                    <td>
                        <asp:TextBox ID="TbOrderBy" runat="server"  CssClass="inputCss"></asp:TextBox>
                    </td>
               </tr>
               <tr>
                     <th>是否作为导航菜单显示：</th>
                    <td>
                        <yk:CheckBox runat="server" ID="CheckIsShow" /> 是
                    </td>
               </tr>
            </table>  
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