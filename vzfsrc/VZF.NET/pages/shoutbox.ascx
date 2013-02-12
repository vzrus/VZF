<%@ Control Language="C#" AutoEventWireup="true" Inherits="YAF.Pages.shoutbox" Codebehind="shoutbox.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="ShoutBox" Src="../controls/ShoutBox.ascx" %>

<VZF:ShoutBox ID="ShoutBox1" runat="server" />

<VZF:LocalizedLabel ID="MustBeLoggedIn" runat="server" Visible="false" LocalizedPage="SHOUTBOX" LocalizedTag="MUSTBELOGGEDIN"></VZF:LocalizedLabel>
