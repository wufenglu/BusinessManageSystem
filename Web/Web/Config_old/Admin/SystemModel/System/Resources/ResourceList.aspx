<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceList.aspx.cs" Inherits="Admin_SystemModel_Admin_ResourceList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Import Namespace="YK.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <yk:AdminHeader ID="AdminHeader1" runat="server"></yk:AdminHeader>

    <script language="javascript" type="text/javascript">
        //添加
        function add() {
            showLayer('添加', 'ResourceView.aspx', 600, 300);
        }

        //编辑
        function update(id) {
            showLayer('编辑', 'ResourceView.aspx?id=' + id, 600, 300);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="navigation">
        <a href="ColumnInforList.aspx">资源管理</a> > 资源列表
    </div>
    <div class="topOpration">
        <input type="button" class="buttonCss" id="addBtn" value=" 添加 " onclick="add()"></input>
        <asp:Button ID="BtnDelete" runat="server" Text=" 删除 " OnClick="ButtonDelete_Click"
            CssClass="buttonCss" />
    </div>
    <div>
        <table class="gridView">
            <tr>
                <th>
                    <input type="checkbox" onclick="yk.core.grid.changeCheckBox(this)" />
                </th>
                <th>
                    名称
                </th>
                <th>
                    路径
                </th>
                <th>
                    排序
                </th>
                <th>
                    作为导航菜单
                </th>
                <th>
                    创建者
                </th>
                <th>
                    添加日期
                </th>
                <th>
                    编辑
                </th>
            </tr>
            <asp:Repeater ID="RepList" runat="server" OnItemDataBound="RepList_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CheckBoxChoose" runat="server" />
                            <asp:HiddenField ID="HiddenFieldID" runat="server" Value='<%# Eval("ID") %>' />
                        </td>
                        <td >
                            <%# Eval("ResourceName")%>
                        </td>
                        <td >
                            <%# Eval("Url")%>
                        </td>
                        <td>
                            <%# Eval("OrderBy")%>
                        </td>
                        <td>
                            <%# Eval("IsShow").ToBoolStr()%>
                        </td>
                        <td>
                            <%# Eval("Creater")%>
                        </td>
                        <td>
                            <%# Convert.ToDateTime(Eval("AddDate").ToString()).ToString("yyyy-MM-dd") %>
                        </td>
                        <td >
                            <a href="javascript:update(<%# Eval("ID") %>)">
                                <img src="../../images/edit.gif" title="编辑" class="edit" /></a>
                        </td>
                    </tr>
                    <asp:Repeater ID="RepChildList" runat="server" OnItemCommand="RepChildList_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                   &nbsp;
                                </td>
                                <td >
                                    <%# Eval("ResourceName")%>
                                </td>
                                <td >
                                    <%# Eval("Url")%>
                                </td>
                                <td>
                                    <%# Eval("OrderBy")%>
                                </td>
                                 <td>
                                    <%# Eval("IsShow").ToBoolStr()%>
                                </td>
                                <td>
                                    <%# Eval("Creater")%>
                                </td>
                                <td>
                                    <%# Convert.ToDateTime(Eval("AddDate").ToString()).ToString("yyyy-MM-dd") %>
                                </td>
                                <td >
                                    <a href="javascript:update(<%# Eval("ID") %>)">
                                        <img src="../../images/edit.gif" title="编辑" class="edit" /></a>
                                    <asp:LinkButton ID="LinkBtn" CommandName="delete" CommandArgument='<%# Eval("ID") %>' runat="server">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging"
        PageSize="20">
    </webdiyer:AspNetPager>
    <div class="bottomSet">
        <input type="button" class="buttonCss" onclick="quanxuan()" value="全选" />
        <input type="button" class="buttonCss" onclick="fanxuan()" value="反选" />
        <asp:Button ID="ButtonDelete" runat="server" Text="删除" OnClick="ButtonDelete_Click"
            CssClass="buttonCss" />
    </div>
    </form>
</body>
</html>
