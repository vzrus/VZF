<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumCategoryListNew.ascx.cs" Inherits="YAF.Controls.ForumCategoryListNew" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="VZF.Utils" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Classes" %>
<asp:Repeater runat="server" id="Categories">
  <ItemTemplate>
    Name: <%# Eval("CategoryName") %>
    Employees:
    <asp:Repeater runat="server" DataSource='<%# Eval("Forums") %>'>
      <ItemTemplate><%# Eval("Name") %></ItemTemplate>
      <SeparatorTemplate>,</SeparatorTemplate>
    </asp:Repeater>
  </ItemTemplate>
</asp:Repeater>