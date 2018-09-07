<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUpload.ascx.cs" Inherits="Admin_Controls_FileUpload" %>
<script language="javascript" type="text/javascript">
  function LargeImgShow(url)
  {
    document.getElementById('LargeImg').src=url;
    document.getElementById('LargeImg').style.display='block';
  }
   function LargeImgClose()
  {
    document.getElementById('LargeImg').style.display='none';
  }
</script>
<asp:FileUpload ID="FileUploadFiles" runat="server" Width="260px" />
<asp:TextBox ID="TbFileUrl" runat="server" Visible="false"></asp:TextBox>
<asp:Button ID="BtnUpLoad" runat="server" Text=" 上传 " OnClick="BtnUpLoad_Click"  Height="23px"/>
<asp:Button ID="BtnDelete" runat="server" Text="删除" Visible="false" OnClick="BtnDelete_Click" />
<img id="ImageShow" runat="server" visible="false" style="width:30px; height:30px" alt="" />
<img src="" id="LargeImg" alt="" style="position:absolute; z-index:2; display:none;"/>
<asp:Label ID="LbTishi" runat="server" Text="" ForeColor="red"></asp:Label>