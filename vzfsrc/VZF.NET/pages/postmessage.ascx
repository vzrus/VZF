<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.postmessage" Codebehind="postmessage.ascx.cs" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Register TagPrefix="VZF" TagName="smileys" Src="../controls/smileys.ascx" %>
<%@ Register TagPrefix="VZF" TagName="LastPosts" Src="../controls/LastPosts.ascx" %>
<%@ Register TagPrefix="VZF" TagName="PostOptions" Src="../controls/PostOptions.ascx" %>
<%@ Register TagPrefix="VZF" TagName="PollList" Src="../controls/PollList.ascx" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<VZF:PollList ID="PollList"  ShowButtons="true" PollGroupId='<%# GetPollGroupID() %>'  runat="server"/>
<table align="center" cellpadding="4" cellspacing="1" class="content" width="100%">
	<tr>
		<td align="center" class="header1" colspan="2">
			<asp:Label ID="Title" runat="server" />
		</td>
	</tr>
		  
	<tr id="PreviewRow" runat="server" visible="false">
		<td class="postformheader" valign="top">
			<VZF:LocalizedLabel runat="server" LocalizedTag="previewtitle" />
		</td>
		<td id="PreviewCell" runat="server" class="post previewPostContent" valign="top">
			<VZF:MessagePost ID="PreviewMessagePost" runat="server" />
		</td>
	</tr>
	<tr id="SubjectRow" runat="server">
		<td class="postformheader" width="20%">
			<VZF:LocalizedLabel ID="TopicSubjectLabel" runat="server" LocalizedTag="subject" />
		</td>
		<td class="post" width="80%">
			<asp:TextBox ID="TopicSubjectTextBox" runat="server" CssClass="edit" MaxLength="100" Width="400" AutoCompleteType="Disabled"/>
		</td>
	</tr>
	<tr id="DescriptionRow" visible="false" runat="server">
		<td class="postformheader" width="20%">
			<VZF:LocalizedLabel ID="TopicDescriptionLabel" runat="server" LocalizedTag="description" />
		</td>
		<td class="post" width="80%">
			<asp:TextBox ID="TopicDescriptionTextBox" runat="server" CssClass="edit" MaxLength="100" Width="400" autocomplete="off" />
		</td>
	</tr>
	<tr id="BlogRow" runat="server" visible="false">
		<td class="postformheader" width="20%">
		    <VZF:LocalizedLabel ID="PostToBlogLbl" runat="server" LocalizedTag="POSTTOBLOG" />
		</td>
		<td class="post" width="80%">
			<asp:CheckBox ID="PostToBlog" runat="server" />
			 <VZF:LocalizedLabel ID="PostToBlogPassLbl" runat="server" LocalizedTag="POSTTOBLOG_PASS" />
			<asp:TextBox ID="BlogPassword" runat="server" TextMode="Password" Width="400" />
			<asp:HiddenField ID="BlogPostID" runat="server" />
		</td>
	</tr>
	<tr id="FromRow" runat="server">
		<td class="postformheader" width="20%">
			<VZF:LocalizedLabel runat="server" LocalizedTag="from" />
		</td>
		<td class="post" width="80%">
			<asp:TextBox ID="From" runat="server" CssClass="edit" Width="400" />
		</td>
	</tr>
	<tr id="StatusRow" visible="false" runat="server">
		<td class="postformheader" width="20%">
			<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="Status" />
		</td>
		<td class="post" width="80%">
			<asp:DropDownList ID="TopicStatus" runat="server" CssClass="edit" Width="400">
			</asp:DropDownList>
		</td>
	</tr>	
	<tr id="PriorityRow" runat="server">
		<td class="postformheader" width="20%">
			<VZF:LocalizedLabel runat="server" LocalizedTag="priority" />
		</td>
		<td class="post" width="80%">
			<asp:DropDownList ID="Priority" runat="server" CssClass="edit" Width="400" />
		</td>
	</tr>
	<tr id="StyleRow" runat="server">
		<td class="postformheader" width="20%">
			<VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="STYLES" />
		</td>
		<td class="post" width="80%">
			<asp:TextBox id="TopicStylesTextBox" runat="server" CssClass="edit" Width="400" />
		</td>
	</tr>
    	<tr id="TagsRow" runat="server" visible="false">
		<td class="postformheader" width="20%">
		<VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedPage="POSTMESSAGE" LocalizedTag="TAGS_TOADD" />
		<VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedPage="POSTMESSAGE" LocalizedTag="TAGS_TOADD_DESC" />
		</td>
		<td class="post" width="80%"> 
			<asp:TextBox ID="Tags" runat="server" AutoCompleteType="Disabled" TextMode="SingleLine" Width="400" />
			<asp:HiddenField ID="TagsIds" runat="server" />
		</td>
	</tr>
    <tr id="ImageRow" runat="server" visible="false">
		<td class="postformheader" width="20%">
		<a id="TopicImageAncor" href="" title="" runat="server" Visible="False">
		<img id="TopicImage" class="" src="" alt="image" runat="server" style="border-width:0px;" />
		</a>
        </td>
		<td class="post" width="80%"> 
		</td>
	</tr>
	<tr>
		<td class="postformheader" valign="top" width="20%">
			<VZF:LocalizedLabel runat="server" LocalizedTag="message" />
			<br />
			<VZF:smileys runat="server" OnClick="insertsmiley" />
			<br />
			<VZF:LocalizedLabel ID="LocalizedLblMaxNumberOfPost" runat="server" LocalizedTag="MAXNUMBEROF" />
		</td>
		<td id="EditorLine" runat="server" class="post" width="80%">
			<!-- editor goes here -->
		</td>
	</tr>

	<VZF:PostOptions id="PostOptions1" runat="server">
	</VZF:PostOptions>

	<tr id="tr_captcha1" runat="server" visible="false">
		<td class="postformheader" valign="top">
			<VZF:LocalizedLabel runat="server" LocalizedTag="Captcha_Image" />
		</td>
		<td class="post">
			<asp:Image ID="imgCaptcha" runat="server" />
		</td>
	</tr>
	<tr id="tr_captcha2" runat="server" visible="false">
		<td class="postformheader" valign="top">
			<VZF:LocalizedLabel runat="server" LocalizedTag="Captcha_Enter" />
		</td>
		<td class="post">
			<asp:TextBox ID="tbCaptcha" runat="server" />
		</td>
	</tr>
	<tr id="EditReasonRow" runat="server">
		<td class="postformheader" width="20%">
			<VZF:LocalizedLabel runat="server" LocalizedTag="EditReason" />
		</td>
		<td class="post" width="80%">
			<asp:TextBox ID="ReasonEditor" runat="server" CssClass="edit" Width="400" />
		</td>
	</tr>
	<tr>
		<td class="footer1">
			&nbsp;
		</td>
		<td class="footer1">
			<VZF:ThemeButton ID="Preview" runat="server" CssClass="yafcssbigbutton leftItem"
				OnClick="Preview_Click" TitleLocalizedTag="PREVIEW_TITLE"  TextLocalizedTag="PREVIEW" />
			<VZF:ThemeButton ID="PostReply" TitleLocalizedTag="SAVE_TITLE"  runat="server" CssClass="yafcssbigbutton leftItem"
				OnClick="PostReply_Click" TextLocalizedTag="SAVE" />
			<VZF:ThemeButton ID="Cancel" TitleLocalizedTag="CANCEL_TITLE"  runat="server" CssClass="yafcssbigbutton leftItem" OnClick="Cancel_Click"
				TextLocalizedTag="CANCEL" />
		</td>
	</tr>
</table>
<br />

<script type="text/javascript">

	var prm = Sys.WebForms.PageRequestManager.getInstance();

	prm.add_beginRequest(beginRequest);

	function beginRequest() {
		prm._scrollPosition = null;
	}

</script>

<VZF:LastPosts ID="LastPosts1" runat="server" Visible="false" />
<div id="DivSmartScroller">
	<VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
