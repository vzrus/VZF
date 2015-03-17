<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="emaildigest.ascx.cs"
    Inherits="VZF.Controls.emaildigest" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="VZF.Utils" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="YafHead" runat="server">
    <title>Digest Notification</title>
</head>
<body id="yaf_digest">
    <div class="emailcontainer">
        <div class="top">
            <div>
                <table class="topContainer" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td width="50%">
                            <h1 class="title">
                                <a href="<%=YafBuildLink.GetLink(ForumPages.forum, true)%>">
                                    <%= PageContext.BoardSettings.Name %></a></h1>
                        </td>
                        <td width="50%" align="right">
                            <h1 class="date">
                                <%=this.Get<IDateTime>().FormatDateLong(DateTime.Now)%>
                            </h1>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="main">
            <div>
                <% if (this.NewTopics.Rows.Count > 0)
                   {
                       var forumName = string.Empty;
                       %>
                <h1 class="header">
                    <%=this.GetText("NEWTOPICS")%></h1>
                <%
                    foreach (System.Data.DataRow f in this.NewTopics.Rows)
                    {
                     if (!forumName.Equals(f["ForumName"].ToString()))
                     {
                         forumName = f["ForumName"].ToString();
                         %>              
                <h2 class="forumTitle">
                    <%=forumName%>
                </h2>
                <%}
                   %>
                <div class="topic">
                    <div>
                        <h3>
                            <a class="subject" href="<%=YafBuildLink.GetLink(ForumPages.posts, true, "m={0}#post{0}", f["LastMessageID"])%>"
                                target="_blank">
                                <%=f["Subject"].ToString()%></a> <a class="comments" href="<%=YafBuildLink.GetLink(ForumPages.posts, true, "m={0}#post{0}", f["LastMessageID"])%>"
                                    target="_blank">
                                    <%=this.GetText("COMMENTS").FormatWith(f["Replies"].ToString())%></a></h3>
                        <h5>
                            <%=this.GetText("STARTEDBY").FormatWith(f["StartedUserName"].ToString())%></h5>
                        <div class="shortpost">
                            <span class="text">
                                <%=this.GetMessageFormattedAndTruncated(f["LastMessage"].ToString(), 200)%>
                            </span><a href="<%=YafBuildLink.GetLink(ForumPages.posts, true, "m={0}#post{0}", f["LastMessageID"])%>"
                                class="link" target="_blank">
                                <%=this.GetText("LINK")%></a> <span class="by">
                                    <%=this.GetText("LASTBY").FormatWith(f["LastUserName"])%>
                                </span>
                        </div>
                    </div>
                </div>
                <%
                   }
                   }%>
                <% if (this.ActiveTopics.Rows.Count > 0)
                   {
                       var forumName = string.Empty; 
                       %>
                <h1 class="header">
                    <%=this.GetText("ACTIVETOPICS")%></h1>
                <%
                   foreach (System.Data.DataRow f in this.ActiveTopics.Rows)
                    {
                     if (!forumName.Equals(f["ForumName"].ToString()))
                     {
                         forumName = f["ForumName"].ToString();
                         %>              
                <h2 class="forumTitle">
                    <%=forumName%>
                </h2>
                <%}
                   %>
                <div class="topic">
                    <div>
                        <h3>
                            <a class="subject" href="<%=YafBuildLink.GetLink(ForumPages.posts, true, "m={0}#post{0}", f["LastMessageID"])%>"
                                target="_blank">
                                <%=f["Subject"]%></a> <a class="comments" href="<%=YafBuildLink.GetLink(ForumPages.posts, true, "m={0}#post{0}", f["LastMessageID"])%>"
                                    target="_blank">
                                    <%=this.GetText("COMMENTS").FormatWith(f["Replies"])%></a></h3>
                        <h5>
                            <%=this.GetText("STARTEDBY").FormatWith(f["StartedUserName"])%></h5>
                        <div class="shortpost">
                            <span class="text">
                                <%=this.GetMessageFormattedAndTruncated(f["LastMessage"].ToString(), 200)%>
                            </span><a href="<%=YafBuildLink.GetLink(ForumPages.posts, true, "m={0}#post{0}", f["LastMessageID"])%>"
                                class="link" target="_blank">
                                <%=this.GetText("LINK")%></a> <span class="by">
                                    <%=this.GetText("LASTBY").FormatWith(f["LastUserName"])%>
                                </span>
                        </div>
                    </div>
                </div>
                <%
                   }
                   }%>
            </div>
        </div>
        <div class="bottom">
            <div>
                <%=this.GetText("REMOVALTEXT") %>
                <a href="<%=YafBuildLink.GetLink(ForumPages.cp_subscriptions, true) %>" class="removelink">
                    <%=this.GetText("REMOVALLINK") %></a>
            </div>
        </div>
    </div>
</body>
</html>
