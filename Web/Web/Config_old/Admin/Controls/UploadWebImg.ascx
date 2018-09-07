<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadWebImg.ascx.cs" Inherits="Admin_Controls_UploadWebImg" %>
<asp:FileUpload ID="FileUploadFiles" runat="server" Width="260px" />
<asp:Button ID="BtnUpLoad" runat="server" Text="上传" OnClick="BtnUpLoad_Click" />
<div runat="server" id="MsgDiv"></div>