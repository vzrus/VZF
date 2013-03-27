<%@ Control Language="c#" CodeBehind="../../../pages/postmessage.ascx.cs" AutoEventWireup="True"Inherits="YAF.Pages.postmessage" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Register TagPrefix="VZF" TagName="PollList" Src="../../../controls/PollList.ascx" %>
<%@ Register TagPrefix="VZF" TagName="smileys" Src="../../../controls/smileys.ascx" %>
<%@ Register TagPrefix="VZF" TagName="LastPosts" Src="../../../controls/LastPosts.ascx" %>
<%@ Register TagPrefix="VZF" TagName="PostOptions" Src="../../../controls/PostOptions.ascx" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<VZF:PollList ID="PollList" ShowButtons="true" PollGroupId='<%# GetPollGroupID() %>'
    runat="server" />
<table align="center" cellpadding="4" cellspacing="1" class="content" width="100%">
    <tr>
        <td align="center" class="header1" colspan="2">
            <asp:Label ID="Title" runat="server" />
        </td>
    </tr>
    <tr id="PreviewRow" runat="server" visible="false">
        <td id="PreviewCell" runat="server" class="post" valign="top" colspan="2">
            <VZF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="previewtitle" />
            <br />
            <VZF:MessagePost ID="PreviewMessagePost" runat="server" />
        </td>
    </tr>
    <tr id="SubjectRow" runat="server">
        <td class="postformheader"  width="20%" colspan="2">
            <VZF:LocalizedLabel ID="TopicSubjectLabel" runat="server" LocalizedTag="subject" />
            <br />
            <asp:TextBox ID="TopicSubjectTextBox" runat="server" CssClass="edit" MaxLength="100"
                Width="400" />
        </td>
    </tr>
    <tr id="DescriptionRow" visible="false" runat="server">
		<td class="postformheader"  width="20%" colspan="2">
			<VZF:LocalizedLabel ID="TopicDescriptionLabel" runat="server" LocalizedTag="description" />
            <br />
            <asp:TextBox ID="TopicDescriptionTextBox" runat="server" CssClass="edit" MaxLength="100" Width="400" />
		</td>
	</tr>
    <tr id="BlogRow" runat="server" visible="false">
        <td class="postformheader" width="20%" colspan="2">
            Post to blog?
            <br />
            <asp:CheckBox ID="PostToBlog" runat="server" />
            Blog Password:
            <asp:TextBox ID="BlogPassword" runat="server" TextMode="Password" Width="400" />
            <asp:HiddenField ID="BlogPostID" runat="server" />
        </td>
    </tr>
    <tr id="FromRow" runat="server">
        <td class="postformheader" width="20%" colspan="2">
            <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="from" />
            <br />
            <asp:TextBox ID="From" runat="server" CssClass="edit" Width="400" />
        </td>
    </tr>
    <tr id="StatusRow" visible="false" runat="server">
		<td class="postformheader" width="20%"  colspan="2">
			<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="Status" />
            <br />
            <asp:DropDownList ID="TopicStatus" runat="server" CssClass="edit" Width="400">
              <asp:ListItem Text="INFORMATIC" Value="informatic"></asp:ListItem>
              <asp:ListItem Text="QUESTION" Value="question"></asp:ListItem>
              <asp:ListItem Text="SOLVED" Value="solved"></asp:ListItem>
              <asp:ListItem Text="ISSUE" Value="issue"></asp:ListItem>
              <asp:ListItem Text="FIXED" Value="fixed"></asp:ListItem>
            </asp:DropDownList>
		</td>
	</tr>	
    <tr id="PriorityRow" runat="server">
        <td class="postformheader" width="20%" colspan="2">
            <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="priority" />
            <br />
            <asp:DropDownList ID="Priority" runat="server" />
        </td>
    </tr>
    <tr id="StyleRow" runat="server">
		<td class="postformheader" width="20%" colspan="2">
			<VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="STYLES" />
            <br />
			<asp:TextBox id="TopicStylesTextBox" runat="server" CssClass="edit" Width="400" />
		</td>
	</tr>
        <tr id="TagsRow" runat="server" visible="false">
		<td class="postformheader" width="20%">
		<VZF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedPage="POSTMESSAGE" LocalizedTag="TAGS_TOADD" />
		<VZF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedPage="POSTMESSAGE" LocalizedTag="TAGS_TOADD_DESC" />
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
        <td id="EditorLine" runat="server" class="post" width="80%" colspan="2">
            <b>
                <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="message" />
            </b>
            <br />
            <asp:PlaceHolder runat="server" Visible="false" ID="SmilesHolder">
                <VZF:smileys ID="Smileys1" runat="server" OnClick="insertsmiley" />
                <br />
                <VZF:LocalizedLabel ID="LocalizedLblMaxNumberOfPost" runat="server" LocalizedTag="MAXNUMBEROF" />
            </asp:PlaceHolder>
            <!-- editor goes here -->
        </td>
    </tr>
    <VZF:PostOptions ID="PostOptions1" runat="server"></VZF:PostOptions>
    <tr id="tr_captcha1" runat="server" visible="false">
        <td class="postformheader" valign="top" colspan="2">
            <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="Captcha_Image" />
            <br />
            <asp:Image ID="imgCaptcha" runat="server" />
        </td>
    </tr>
    <tr id="tr_captcha2" runat="server" visible="false">
        <td class="postformheader" valign="top" colspan="2">
            <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="Captcha_Enter" />
            <br />
            <asp:TextBox ID="tbCaptcha" runat="server" />
        </td>
    </tr>
    <tr id="EditReasonRow" runat="server">
        <td class="postformheader" width="20%" colspan="2">
            <VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="EditReason" />
            <br />
            <asp:TextBox ID="ReasonEditor" runat="server" CssClass="edit" Width="400" />
        </td>
    </tr>
    <tr>
        <td class="footer1" colspan="2">
            <VZF:ThemeButton ID="Preview" runat="server" CssClass="yafcssbigbutton leftItem"
                OnClick="Preview_Click" TextLocalizedTag="PREVIEW" />
            <VZF:ThemeButton ID="PostReply" runat="server" CssClass="yafcssbigbutton leftItem"
                OnClick="PostReply_Click" TextLocalizedTag="SAVE" />
            <VZF:ThemeButton ID="Cancel" runat="server" CssClass="yafcssbigbutton leftItem" OnClick="Cancel_Click"
                TextLocalizedTag="CANCEL" />
        </td>
    </tr>
</table>
<script type="text/javascript">

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_beginRequest(beginRequest);

    function beginRequest() {
        prm._scrollPosition = null;
    }

</script>
<asp:PlaceHolder ID="LastPostsHolder" runat="server" Visible="false">
    <VZF:LastPosts ID="LastPosts1" runat="server" Visible="false" />
</asp:PlaceHolder>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
