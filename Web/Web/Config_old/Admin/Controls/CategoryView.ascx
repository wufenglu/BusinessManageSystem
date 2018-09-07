<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryView.ascx.cs" Inherits="Admin_Controls_CategoryView" %>
<%@ Import Namespace="YK.Common" %>
<%@ Register Src="FileUpload.ascx" TagName="FileUpload" TagPrefix="uc1" %>      
    <div class="BodyDiv">         
    <table class="fromTable">
         <tr>
            <th>类别：</th>
            <td>
                <asp:DropDownList ID="DDLCategory" runat="server">
                </asp:DropDownList>
            </td> 
        </tr>
         <tr>  
             <th>名称：</th>
            <td>
                <yk:TextBox ID="TbCategoryName" runat="server"  CssClass="inputCss" Width="150px" GroupName="Validator" LabelID="name" ToolTip="名称"></yk:TextBox>
                <span id="name"></span>
            </td>
        </tr>
         <tr>  
            <th>类型：</th>
            <td>
                <asp:DropDownList ID="DDLType" runat="server">
                    <asp:ListItem Text="普通" Value="0"></asp:ListItem>
                    <asp:ListItem Text="团购" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>            
         </tr>
         <tr>
            <th> 图片：</th>
            <td>
                <uc1:FileUpload ID="FileUploadImg" runat="server" />  
            </td>   
        </tr>
         <tr>           
            <th>推荐：</th>
                <td>
                    <asp:DropDownList ID="DDLVouch" runat="server">
                        <asp:ListItem Text="不推荐" Value="0"></asp:ListItem>
                        <asp:ListItem Text="推荐" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
         </tr>
         <tr>  
            <th>排序：</th>
            <td>
                <asp:TextBox ID="TbOrder" runat="server"  CssClass="inputCss" Width="50px" ></asp:TextBox><asp:Label ID="Tishi" ForeColor="red" Text="" runat="Server"></asp:Label>
            </td> 
        </tr>
         <tr>                                 
            <th > 描述：</th>
            <td >                 
                <asp:TextBox ID="TbDescription" runat="server"  CssClass="inputCss" Width="300px" MaxLength="100" Rows="3" TextMode="MultiLine"></asp:TextBox>                
            </td>
         </tr>
         <tr>  
                 <th>是否隐藏：</th>
                 <td>
                     <asp:CheckBox ID="CheckBoxIsHidden" runat="server" />隐藏
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