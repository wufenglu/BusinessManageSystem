<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryList.ascx.cs" Inherits="Admin_Controls_CategoryList" %>
<%@ Import Namespace="YK.Common" %>
<%@ Register Src="FileUpload.ascx" TagName="FileUpload" TagPrefix="uc1" %>         
    <asp:Repeater ID="RepList" runat="server">
        <HeaderTemplate>
            <table class="fromTable" width="100%" style="text-align:center;" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                    <td><input type="checkbox" onclick="yk.core.grid.changeCheckBox(this)" /></td>                    
                    <td>名称</td>
                    <td>类型</td>
                    <td>排序</td>
                    <td> 状态</td>
                    <td>显示</td>
                    <td> 添加日期</td>
                    <td> 编辑</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBoxChoose" runat="server"  />
                        <asp:HiddenField ID="HiddenFieldID" runat="server" Value='<%# Eval("ID") %>' />
                    </td>
                    <td > <%# Eval("CategoryName") %></td>
                    <td><%# Eval("Type") %></td>
                    <td><%# Eval("OrderBy") %></td>
                    <td> <%# DDLVouchSet.Items[Eval("IsVouch").ToInt()].Text %></td>
                    <td> <%# Eval("IsHidden").ToIsHidden() %></td>
                    <td> <%# Convert.ToDateTime(Eval("AddDate").ToString()).ToShortDateString() %></td>
                    <td>
                        <a href='javascript:update(<%# Eval("ID") %>,<%=cid %>)'>
                            <img src="<%=CommonClass.AppPath   %>Admin/images/edit.gif" style="border:0px;" title="编辑" />
                        </a>
                        
                       
                        <a href='CategoryToBrandList.aspx?cid=<%# Eval("ID") %>'>
                            品牌设置
                        </a>
                       
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
<div  class="bottomSet">
     <input type="button" value=" 全 选 " class="buttonCss" onclick="quanxuan()" />
     <input type="button" value=" 反 选 " class="buttonCss" onclick="fanxuan()"  />
    <asp:Button ID="ButtonDelete" runat="server"  Text="删除" OnClick="ButtonDelete_Click"  CssClass="buttonCss"/>
    <span >推荐设置：
    <asp:DropDownList ID="DDLVouchSet" runat="server">
        <asp:ListItem Text="不推荐" Value="0"></asp:ListItem>
        <asp:ListItem Text="推荐" Value="1"></asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="BtnVouchSet" runat="server" Text=" 设 置 " OnClick="DDLVouchSet_Click"  CssClass="buttonCss" />
    </span>
    是否隐藏：
    <asp:CheckBox ID="CheckBoxIsHiddenSet" runat="server" BackColor="White" />
    <asp:Button ID="BtnHiddenSet" runat="server" Text=" 设 置 " OnClick="DDLIsHiddenSet_Click"  CssClass="buttonCss" />
</div>