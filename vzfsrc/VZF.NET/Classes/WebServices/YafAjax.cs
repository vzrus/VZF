using System.Globalization;
using System.Threading;
using VZF.Utils.Extensions;


namespace YAF.Classes
{
    #region Using

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.Script.Services;
    using System.Web.Security;
    using System.Web.Services;
    using System.Xml;

    using VZF.Data.Common;
    
    using YAF.Core;
    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;    
    using VZF.Kernel;

    #endregion

    /// <summary>
    /// Class for JS jQuery  Ajax Methods
    /// </summary>
    [WebService(Namespace = "http://yetanotherforum.net/yafajax")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class YafAjax : WebService, IHaveServiceLocator
    {
        #region Properties

        /// <summary>
        ///   Gets ServiceLocator.
        /// </summary>
        public IServiceLocator ServiceLocator
        {
            get
            {
                return YafContext.Current.ServiceLocator;
            }
        }

        #endregion

        /// <summary>
        /// Handles the multi quote Button.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        /// <param name="multiquoteButton">The Multiquote Button Checkbox checked</param>
        /// <param name="messageId">The message id.</param>
        /// <param name="buttonCssClass">The button CSS class.</param>
        /// <returns>Returns the Message Id and the Updated CSS Class for the Button</returns>
        [WebMethod(EnableSession = true)]
        public YafAlbum.ReturnClass HandleMultiQuote([NotNull]string buttonId, [NotNull]bool multiquoteButton, [NotNull]int messageId, [NotNull]string buttonCssClass)
        {
            if (multiquoteButton)
            {
                if (this.Get<IYafSession>().MultiQuoteIds != null)
                {
                    if (!this.Get<IYafSession>().MultiQuoteIds.Contains(messageId))
                    {
                        this.Get<IYafSession>().MultiQuoteIds.Add(messageId);
                    }
                }
                else
                {
                    this.Get<IYafSession>().MultiQuoteIds = new ArrayList { messageId };
                }

                buttonCssClass += " Checked";
            }
            else
            {
                if (this.Get<IYafSession>().MultiQuoteIds != null)
                {
                    if (this.Get<IYafSession>().MultiQuoteIds.Contains(messageId))
                    {
                        this.Get<IYafSession>().MultiQuoteIds.Remove(messageId);
                    }
                }

                buttonCssClass = "MultiQuoteButton";
            }

            return new YafAlbum.ReturnClass { Id = buttonId, NewTitle = buttonCssClass };
        }

        /// <summary>
        /// Spell check via google api.
        /// </summary>
        /// <param name="text">
        /// The text to check.
        /// </param>
        /// <param name="lang">
        /// The langauage of the text.
        /// </param>
        /// <param name="engine">
        /// The engine.
        /// </param>
        /// <param name="suggest">
        /// The suggest words.
        /// </param>
        /// <returns>
        /// Returns List of Suggest Words.
        /// </returns>
        [WebMethod(EnableSession = true)]
        public string SpellCheck([NotNull]string text, [NotNull]string lang, string engine, string suggest)
        {
            if (suggest.Equals("undefined", StringComparison.OrdinalIgnoreCase))
            {
                suggest = string.Empty;
            }

            string xml;

            List<string> result;

            if (string.IsNullOrEmpty(suggest))
            {
                xml = GetSpellCheckRequest(text, lang);
                result = GetListOfMisspelledWords(xml, text);
            }
            else
            {
                xml = GetSpellCheckRequest(suggest, lang);
                result = GetListOfSuggestWords(xml, suggest);
            }

            return new JavaScriptSerializer().Serialize(result);
        }

        /// <summary>
        /// SSO Login From Facebook
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="first_name">
        /// The first name.
        /// </param>
        /// <param name="last_name">
        /// The last name.
        /// </param>
        /// <param name="link">
        /// The link.
        /// </param>
        /// <param name="username">
        /// The user name.
        /// </param>
        /// <param name="birthday">
        /// The birthday.
        /// </param>
        /// <param name="hometown">
        /// The hometown.
        /// </param>
        /// <param name="gender">
        /// The gender.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="timezone">
        /// The timezone.
        /// </param>
        /// <param name="locale">
        /// The locale.
        /// </param>
        /// <param name="remember">
        /// The remember.
        /// </param>
        /// <returns>
        /// Returns the Login Status
        /// </returns>
        [WebMethod(EnableSession = true)]
        public string LoginFacebookUser(
            string id,
            string name,
            string first_name,
            string last_name,
            string link,
            string username,
            string birthday,
            string hometown,
            string gender,
            string email,
            string timezone,
            string locale,
            bool remember)
        {
            return YafSingleSignOnUser.LoginFacebookUser(
                id,
                name,
                first_name,
                last_name,
                link,
                username,
                birthday,
                hometown,
                gender,
                email,
                timezone,
                locale,
                remember); 
        }

        /// <summary>
        /// The change album title.
        /// </summary>
        /// <param name="albumID">
        /// The album id.
        /// </param>
        /// <param name="newTitle">
        /// The New title.
        /// </param>
        /// <returns>
        /// the return object.
        /// </returns>
        [WebMethod(EnableSession = true)]
        public YafAlbum.ReturnClass ChangeAlbumTitle(int albumID, [NotNull] string newTitle)
        {
            return YafAlbum.ChangeAlbumTitle(albumID, newTitle);
        }

        /// <summary>
        /// The change image caption.
        /// </summary>
        /// <param name="imageID">
        /// The Image id.
        /// </param>
        /// <param name="newCaption">
        /// The New caption.
        /// </param>
        /// <returns>
        /// the return object.
        /// </returns>
        [WebMethod(EnableSession = true)]
        public YafAlbum.ReturnClass ChangeImageCaption(int imageID, [NotNull] string newCaption)
        {
            return YafAlbum.ChangeImageCaption(imageID, newCaption);
        }

        /// <summary>
        /// The refresh shout box.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The refresh shout box js.
        /// </returns>
        [WebMethod]
        public int RefreshShoutBox(int boardId)
        {           
            var messages = this.Get<IDataCache>().GetOrSet(
                "{0}_basic".FormatWith(Constants.Cache.Shoutbox),
                () => CommonDb.shoutbox_getmessages(YafContext.Current.PageModuleID, boardId, 1, false).AsEnumerable(),
                TimeSpan.FromMilliseconds(1000));

            var message = messages.FirstOrDefault();

            return message != null ? message.Field<int>("ShoutBoxMessageID") : 0;
        }

        #region Favorite Topic Function

        /// <summary>
        /// The add favorite topic.
        /// </summary>
        /// <param name="topicId">
        /// The topic ID.
        /// </param>
        /// <returns>
        /// The add favorite topic js.
        /// </returns>
        [WebMethod(EnableSession = true)]
        public int AddFavoriteTopic(int topicId)
        {
            return this.Get<IFavoriteTopic>().AddFavoriteTopic(topicId);
        }

        /// <summary>
        /// The remove favorite topic.
        /// </summary>
        /// <param name="topicId">
        /// The favorite topic id.
        /// </param>
        /// <returns>
        /// The remove favorite topic js.
        /// </returns>
        [WebMethod(EnableSession = true)]
        public int RemoveFavoriteTopic(int topicId)
        {
            return this.Get<IFavoriteTopic>().RemoveFavoriteTopic(topicId);
        }

        #region Thanks Functions

        /// <summary>
        /// Add Thanks to post
        /// </summary>
        /// <param name="msgID">
        /// The msg id.
        /// </param>
        /// <returns>
        /// Returns ThankYou Info
        /// </returns>
        [CanBeNull]
        [WebMethod(EnableSession = true)]
        public ThankYouInfo AddThanks([NotNull] object msgID)
        {
            var messageId = msgID.ToType<int>();

            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
            {
                var username =
                    CommonDb.message_AddThanks(YafContext.Current.PageModuleID, UserMembershipHelper.GetUserIDFromProviderUserKey(membershipUser.ProviderUserKey), messageId, this.Get<YafBoardSettings>().EnableDisplayName);

                // if the user is empty, return a null object...
                return username.IsNotSet()
                           ? null
                           : YafThankYou.CreateThankYou(
                               this.Get<HttpServerUtilityBase>().HtmlEncode(username),
                               "BUTTON_THANKSDELETE",
                               "BUTTON_THANKSDELETE_TT",
                               messageId);
            }

            return null;
        }

        /// <summary>
        /// This method is called asynchronously when the user clicks on "Remove Thank" button.
        /// </summary>
        /// <param name="msgID">
        /// Message Id
        /// </param>
        /// <returns>
        /// Returns ThankYou Info
        /// </returns>
        [NotNull]
        [WebMethod(EnableSession = true)]
        public ThankYouInfo RemoveThanks([NotNull] object msgID)
        {
            var messageID = msgID.ToType<int>();

            var username =
                CommonDb.message_RemoveThanks(YafContext.Current.PageModuleID, UserMembershipHelper.GetUserIDFromProviderUserKey(Membership.GetUser().ProviderUserKey), messageID, this.Get<YafBoardSettings>().EnableDisplayName);

            return YafThankYou.CreateThankYou(username, "BUTTON_THANKS", "BUTTON_THANKS_TT", messageID);
        }

        /// <summary>
        /// This method is called asynchronously when the user clicks on "Remove Thank" button.
        /// </summary>
        /// <param name="msgID">
        /// Message Id
        /// </param>
        /// <returns>
        /// Returns ThankYou Info
        /// </returns>
        [NotNull]
        [WebMethod(EnableSession = true)]
        public ThankYouInfo GetForums([NotNull] object frmID, int? currentNode)
        {
            var messageID = frmID.ToType<int>();

            var username =
                CommonDb.message_RemoveThanks(YafContext.Current.PageModuleID, UserMembershipHelper.GetUserIDFromProviderUserKey(Membership.GetUser().ProviderUserKey), messageID, this.Get<YafBoardSettings>().EnableDisplayName);

            return YafThankYou.CreateThankYou(username, "BUTTON_THANKS", "BUTTON_THANKS_TT", messageID);
        }

       
        #endregion


        [CanBeNull]
        [WebMethod(EnableSession = true)]
        public string FancyTreeGetNodes(int boardId1, string inodeId, int viiew1, string ctive1, string elected1,
            bool boardFirst, string forumUrl, bool showLinks1, [NotNull] int showAccessRow1)
        {
            // show access masks list
            int? amdd = null;
            // var userId = YafContext.Current.CurrentUserData.UserID;

            int access = boardId1;

            if (ctive1.IsSet())
            {
                this.Get<IYafSession>().NntpTreeActiveNode = ctive1;
            }

            if (elected1.IsSet())
            {
                this.Get<IYafSession>().SearchTreeSelectedNodes = elected1.Split('!');
            }


            if (viiew1.ToType<int>() == 0)
            {
                access = 1;
            }




            if (inodeId != "-100" || inodeId.IsNotSet())
            {
                inodeId = boardId1.ToString(CultureInfo.InvariantCulture);
                return new JavaScriptSerializer().Serialize(FancyTree.GetForumsJumpTreeNodesLevel(
                    inodeId,
                    viiew1,
                    access,
                    ctive1,
                    boardFirst,
                    forumUrl,
                    showLinks1,
                    showAccessRow1.ToType<int>()));

            }

            return null;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Gets the list of suggest words.
        /// </summary>
        /// <param name="xml">
        /// The XML.
        /// </param>
        /// <param name="suggest">
        /// The suggest.
        /// </param>
        /// <returns>
        /// The get list of suggest words.
        /// </returns>
        private static List<string> GetListOfSuggestWords(string xml, string suggest)
        {
            if (string.IsNullOrEmpty(xml) || string.IsNullOrEmpty(suggest))
            {
                return null;
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            if (!xmlDoc.HasChildNodes)
            {
                return null;
            }

            XmlNodeList nodeList = xmlDoc.SelectNodes("//c");

            if (null == nodeList || 0 >= nodeList.Count)
            {
                return null;
            }

            List<string> list = new List<string>();

            foreach (XmlNode node in nodeList)
            {
                list.AddRange(node.InnerText.Split('\t'));
                return list;
            }

            return list;
        }

        /// <summary>
        /// Gets the list of misspelled words.
        /// </summary>
        /// <param name="xml">
        /// The XML.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The get list of misspelled words.
        /// </returns>
        private static List<string> GetListOfMisspelledWords(string xml, string text)
        {
            if (string.IsNullOrEmpty(xml) || string.IsNullOrEmpty(text))
            {
                return null;
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            if (!xmlDoc.HasChildNodes)
            {
                return null;
            }

            var nodeList = xmlDoc.SelectNodes("//c");

            if (null == nodeList || 0 >= nodeList.Count)
            {
                return null;
            }

            return (from XmlNode node in nodeList
                    let offset = node.Attributes["o"].Value.ToType<int>()
                    let length = node.Attributes["l"].Value.ToType<int>()
                    select text.Substring(offset, length)).ToList();
        }

        /// <summary>
        /// Requests the spell check and get the result back.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="lang">
        /// The lang.
        /// </param>
        /// <returns>
        /// The get spell check request.
        /// </returns>
        private static string GetSpellCheckRequest(string text, string lang)
        {
            var requestUrl = ConstructRequestUrl(text, lang);
            var requestContentXml = ConstructSpellRequestContentXml(text);

            byte[] buffer = Encoding.UTF8.GetBytes(requestContentXml);

            var webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "text/xml");
            byte[] response = webClient.UploadData(requestUrl, "POST", buffer);
            return Encoding.UTF8.GetString(response);
        }

        /// <summary>
        /// Constructs the request URL.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="lang">
        /// The lang.
        /// </param>
        /// <returns>
        /// The construct request url.
        /// </returns>
        private static string ConstructRequestUrl(string text, string lang)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            lang = string.IsNullOrEmpty(lang) ? "en" : lang;

            return "https://www.google.com/tbproxy/spell?lang={0}&text={1}".FormatWith(lang, text);
        }

        /// <summary>
        /// Constructs the spell request content XML.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The construct spell request content xml.
        /// </returns>
        private static string ConstructSpellRequestContentXml(string text)
        {
            var doc = new XmlDocument();
            var declaration = doc.CreateXmlDeclaration("1.0", null, null);

            doc.AppendChild(declaration);

            var root = doc.CreateElement("spellrequest");

            root.SetAttribute("textalreadyclipped", "0");
            root.SetAttribute("ignoredups", "0");
            root.SetAttribute("ignoredigits", "1");
            root.SetAttribute("ignoreallcaps", "1");

            doc.AppendChild(root);

            var textElement = doc.CreateElement("text");

            textElement.InnerText = text;
            root.AppendChild(textElement);

            return doc.InnerXml;
        }

        #endregion
    }
}