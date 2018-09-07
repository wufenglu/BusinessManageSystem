<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocUpload.ascx.cs" Inherits="Admin_Controls_DocUpload" %>
<asp:FileUpload ID="FileUploadFiles" runat="server" Width="260px" />
<asp:TextBox ID="TbFileUrl" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="TbHouZhui" runat="server" Visible="false"></asp:TextBox>
<asp:Button ID="BtnUpLoad" runat="server" Text=" 上传 " OnClick="BtnUpLoad_Click" Height="23px" />
<asp:Button ID="BtnDelete" runat="server" Text="删除" Visible="false" OnClick="BtnDelete_Click" />
<asp:Label ID="LbTishi" runat="server" Text="" ForeColor="red"></asp:Label>