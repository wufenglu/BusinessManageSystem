<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductCategory.ascx.cs" Inherits="Admin_Controls_ProductCategory" %>
<yk:DropDownList runat="server" ID="DDLOneLevel" AutoPostBack="true" 
    onselectedindexchanged="DDLOneLevel_SelectedIndexChanged"></yk:DropDownList>
<yk:DropDownList runat="server" ID="DDLTwoLevel" AutoPostBack="true" 
    OnSelectedIndexChanged="DDLTwoLevel_SelectedIndexChanged"></yk:DropDownList>
<yk:DropDownList runat="server" ID="DDLThreeLevel" LabelID="LbCategory"
    GroupName="Validator" CssClass="inputCss" title="商品类别"></yk:DropDownList>
<% if(IsValidatior==true){ %>
<span id="LbCategory">*</span>     
<% } %>                        