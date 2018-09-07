<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Admin_SystemModel_System_Organizations_Edit" %>

<%@ Import Namespace="YK.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <yk:AdminHeader ID="AdminHeader1" runat="server"></yk:AdminHeader>
</head>
<body>
    <form id="form1" runat="server">         
         <div class="BodyDiv">         
                <table class="fromTable">
                    <tr>          
                        <th>租户名称：</th>
                        <td>
                            <yk:TextBox ID="TbName" runat="server"  Width="300px" GroupName="Validator" CssClass="inputCss"
                            title="名称" LabelID="LbName"></yk:TextBox>
                            <span id="LbName">*</span> 
                        </td>
                    </tr> 
                    <tr>          
                        <th>租户编码：</th>
                        <td>
                            <yk:TextBox ID="TbCode" runat="server"  Width="300px" GroupName="Validator" CssClass="inputCss"
                            title="租户编码" LabelID="LbCode"></yk:TextBox>
                            <span id="LbCode">*</span> 
                        </td>
                    </tr> 
                    <tr>
                        <th>数据库类型：</th>
                        <td>
                            <yk:DropDownList runat="server" ID="DDLDbType" title="数据库类型" LabelID="LbDbType" GroupName="Validator">                                
                            </yk:DropDownList>
                            <span id="LbDbType">*</span> 
                        </td>
                    </tr> 
                    <tr>          
                        <th>数据库地址：</th>
                        <td>
                            <yk:TextBox ID="TbServer" runat="server"  Width="300px" GroupName="Validator" CssClass="inputCss"
                            title="数据库地址" LabelID="LbServer"></yk:TextBox>
                            <span id="LbServer">*</span> 
                        </td>
                    </tr>  
                    <tr>          
                        <th>数据库名称：</th>
                        <td>
                            <yk:TextBox ID="TbDatabaseName" runat="server"  Width="300px" GroupName="Validator" CssClass="inputCss"
                            title="数据库名称" LabelID="LbDatabaseName"></yk:TextBox>
                            <span id="LbDatabaseName">*</span> 
                        </td>
                    </tr> 
                    <tr>          
                        <th>用户名：</th>
                        <td>
                            <yk:TextBox ID="TbUserName" runat="server"  Width="300px" GroupName="Validator" CssClass="inputCss"
                            title="用户名" LabelID="LbUserName"></yk:TextBox>
                            <span id="LbUserName">*</span> 
                        </td>
                    </tr>  
                    <tr>          
                        <th>密码：</th>
                        <td>
                            <yk:TextBox ID="TbPassword" runat="server"  Width="300px" GroupName="Validator" CssClass="inputCss"
                            title="密码" LabelID="LbPassword"></yk:TextBox>
                            <span id="LbPassword">*</span> 
                        </td>
                    </tr>  
                    <tr>          
                        <th>端口：</th>
                        <td>
                            <yk:TextBox ID="TbPort" runat="server"  Width="300px" GroupName="Validator" CssClass="inputCss"
                            title="端口" LabelID="LbPort"></yk:TextBox>
                            <span id="LbPort">*</span> 
                        </td>
                    </tr>                     
                    <tr>
                         <th>是否禁用：</th>
                        <td>
                            <asp:CheckBox ID="CheckBoxState" runat="server" Checked="true" />
                        </td>
                   </tr>
                </table>
         </div>
                  
        <div class="SubmitClass">
            <asp:Button ID="BtnSave" runat="server" Text=" 保存 " CssClass="buttonCss"  OnClick="BtnSave_Click" OnClientClick="return yk.core.validator.VerificationForm('Validator')"/>
                     <input type="button" value=" 取消 " class="buttonCss" id="cancleCss" onclick="closeLayer()"  />
        </div> 
        <div runat="Server" id="MessageDiv">            
        </div> 
    </form>
</body>
</html>
