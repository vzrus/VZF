/* Yet Another Forum.NET
 * Copyright (C) 2006-2011 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
namespace VZF.Data.Postgre.Search
{
    #region Using
   
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using VZF.Data.DAL;
    using VZF.Utils;

    using YAF.Types;
    using YAF.Types.Constants;

    #endregion

    /// <summary>
    /// The search builder.
    /// </summary>
    public class SearchBuilder
    {
        #region Public Methods

        /// <summary>
        /// The build search sql.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="toSearchWhat">
        /// The to search what.
        /// </param>
        /// <param name="toSearchFromWho">
        /// The to search from who.
        /// </param>
        /// <param name="searchFromWhoMethod">
        /// The search from who method.
        /// </param>
        /// <param name="searchWhatMethod">
        /// The search what method.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="searchDisplayName">
        /// The search display name.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="maxResults">
        /// The max results.
        /// </param>
        /// <param name="useFullText">
        /// The use full text.
        /// </param>
        /// <param name="categoriesIds">
        /// The categoriesIds.
        /// </param>
        /// <param name="ids">
        /// The forum ids.
        /// </param>
        /// <param name="forumIdToStartAt">
        /// The forum Id To Start At.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The build search sql.
        /// </returns>
        public string BuildSearchSql(
            int? mid,
            [NotNull] string toSearchWhat,
            [NotNull] string toSearchFromWho,
            SearchWhatFlags searchFromWhoMethod,
            SearchWhatFlags searchWhatMethod,
            int userId,
            bool searchDisplayName,
            int boardId,
            int maxResults,
            bool useFullText,
            string categoriesIds,
            string ids,
            [NotNull] IEnumerable<int> forumIdToStartAt,
            string culture)
        {
            CodeContracts.ArgumentNotNull(toSearchWhat, "toSearchWhat");
            CodeContracts.ArgumentNotNull(toSearchFromWho, "toSearchFromWho");

            string limitString = string.Empty;
            string orderString = string.Empty;
            var searchSql = new StringBuilder();
            string forumIDs = ids;
            string ftsLanguage = "simple";
            culture = culture.Substring(0, 2);
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                if (ci.Parent.Name == culture)
                {
                    switch (culture)
                    {
                        case "en":
                            ftsLanguage = "english";
                            break;
                        case "ru":
                            ftsLanguage = "russian";
                            break;
                        case "sp":
                            ftsLanguage = "spanish";
                            break;
                        case "fr":
                            ftsLanguage = "french";
                            break;
                        case "de":
                            ftsLanguage = "german";
                            break;
                        case "da":
                            ftsLanguage = "danish";
                            break;
                        case "nl":
                            ftsLanguage = "dutch";
                            break;
                        case "fi":
                            ftsLanguage = "finnish";
                            break;
                        case "hu":
                            ftsLanguage = "hungarian";
                            break;
                        case "it":
                            ftsLanguage = "italian";
                            break;
                        case "nn":
                            ftsLanguage = "norwegian";
                            break;
                        case "pt":
                            ftsLanguage = "portugues";
                            break;
                        case "ro":
                            ftsLanguage = "romanian";
                            break;
                        case "se":
                            ftsLanguage = "swedish";
                            break;
                        case "tr":
                            ftsLanguage = "turkish";
                            break;
                    }
                }

                break;
            }
           
            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();
            searchSql.Append(maxResults == 0 ? "SELECT " : "SELECT DISTINCT ");

            if (maxResults > 0)
            {
                limitString += string.Format(" LIMIT {0} ", maxResults);
            }

            searchSql.Append(
                " a.forumid, a.topicid, a.topic, b.userid, COALESCE(c.username, b.name) as Name, c.messageid as \"MessageID\", c.posted, c.message as \"Message\", c.flags FROM ");
            searchSql.Append(SqlDbAccess.GetVzfObjectName("topic", mid));
            searchSql.Append(" a JOIN ");
            searchSql.Append(SqlDbAccess.GetVzfObjectName("forum", mid));
            searchSql.Append(" f ON f.forumid = a.forumid LEFT JOIN ");
            searchSql.Append(SqlDbAccess.GetVzfObjectName("message", mid));
            searchSql.Append(" c ON a.topicid = c.topicid LEFT JOIN ");
            searchSql.Append(SqlDbAccess.GetVzfObjectName("user", mid));
            searchSql.Append(" b ON c.userid = b.userid join ");
            searchSql.Append(SqlDbAccess.GetVzfObjectName("vaccess", mid));
            searchSql.Append(" x ON x.forumid=a.forumid ");
            searchSql.Append(
                "WHERE x.readaccess<>0 AND x.userid={0} AND c.isapproved IS TRUE AND a.topicmovedid IS NULL AND a.isdeleted IS FALSE AND c.isdeleted IS FALSE "
                    .FormatWith(userId));


            orderString += "ORDER BY a.forumid ";

            string[] words;

            bool bFirst = false;

            if (!string.IsNullOrEmpty(toSearchFromWho) || !string.IsNullOrEmpty(toSearchWhat))
            {
                searchSql.Append("AND (");
                bFirst = true;
            }

            if (!string.IsNullOrEmpty(toSearchFromWho))
            {
                // generate user search sql...
                switch (searchFromWhoMethod)
                {
                    case SearchWhatFlags.AllWords:
                        words = toSearchFromWho.Split(' ');
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                searchSql.Append(" AND ");
                            }
                            else
                            {
                                bFirst = false;
                            }

                            // searchSql += string.Format(" ((c.username IS NULL AND b.name ~* '.*{0}.*') OR (c.username ~* '.*{0}.*'))", word);
                            if (!int.TryParse(word, out userId))
                            {
                                if (searchDisplayName)
                                {
                                    searchSql.Append(
                                        string.Format(
                                            " ((c.userdisplayname IS NULL AND b.displayname  ~* '.*{0}.*') OR (c.userdisplayname ~* '.*{0}.*'))",
                                            word));
                                }
                                else
                                {
                                    searchSql.Append(
                                        string.Format(
                                            " ((c.username IS NULL AND b.name ~* '.*{0}.*') OR (c.username ~* '.*{0}.*'))",
                                            word));
                                }
                            }
                            else
                            {
                                searchSql.Append(string.Format(" (c.userid IN ({0}))", userId));
                            }
                        }

                        break;
                    case SearchWhatFlags.AnyWords:
                        words = toSearchFromWho.Split(' ');
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                searchSql.Append(" OR ");
                            }
                            else
                            {
                                bFirst = false;
                            }

                            if (searchDisplayName)
                            {
                                searchSql.Append(
                                    string.Format(
                                        " ((c.userdisplayname IS NULL AND b.displayname  ~* '.*{0}.*') OR (c.userdisplayname ~* '.*{0}.*'))",
                                        word));
                            }
                            else
                            {
                                searchSql.Append(
                                string.Format(
                                    " ((c.username IS NULL AND b.name ~* '.*{0}.*') OR (c.username ~* '.*{0}.*'))",
                                    word));
                            }
                        }

                        break;
                    case SearchWhatFlags.ExactMatch:

                        // searchSql += string.Format(" ((c.username IS NULL AND b.name = '{0}' ) OR (c.username = '{0}' ))", toSearchFromWho);
                        if (!int.TryParse(toSearchFromWho, out userId))
                        {
                            if (searchDisplayName)
                            {
                                searchSql.Append(
                                    string.Format(
                                        " ((c.userdisplayname IS NULL AND b.displayname = '{0}') OR (c.userdisplayname = '{0}'))",
                                        toSearchFromWho));
                            }
                            else
                            {
                                searchSql.Append(
                                    string.Format(
                                        " ((c.username IS NULL AND b.name = '{0}') OR (c.username = '{0}'))",
                                        toSearchFromWho));
                            }
                        }
                        else
                        {
                            searchSql.Append(string.Format(" (c.userid IN ({0})) ", userId));
                        }

                        break;
                }

                searchSql.Append(") ");
            }

            if (!string.IsNullOrEmpty(toSearchWhat))
            {
                // generate message and topic search sql...
                switch (searchWhatMethod)
                {
                    case SearchWhatFlags.AllWords:
                        words = toSearchWhat.RemoveDoubleWhiteSpaces().Split(' ');
                        if (useFullText)
                        {
                            string ftInner = string.Empty;

                            // make the inner FULLTEXT search
                            foreach (string word in words)
                            {
                                if (!bFirst)
                                {
                                    ftInner += " & ";
                                }
                                else
                                {
                                    bFirst = false;
                                }

                                ftInner += string.Format("{0}", word);
                            }

                            // make final string...
                            searchSql.Append(
                                string.Format(
                                    "select to_tsvector('{0}',c.message) @@ to_tsquery('{0}','{1}') OR to_tsvector('{0}',a.topic) @@ to_tsquery('{0}','{1}') ",
                                     ftsLanguage, ftInner.Trim().Trim('&').Trim()));
                        }
                        else
                        {
                            foreach (string word in words)
                            {
                                if (!bFirst)
                                {
                                    searchSql.Append(" AND ");
                                }
                                else
                                {
                                    bFirst = false;
                                }


                                searchSql.Append(
                                    string.Format("(c.message ~* '.*{0}.*' OR a.topic ~* '.*{0}.*' )", word));
                            }
                        }

                        break;
                    case SearchWhatFlags.AnyWords:
                        words = toSearchWhat.RemoveDoubleWhiteSpaces().Split(' ');

                        if (useFullText)
                        {
                            string ftInner = string.Empty;
                            string ftInnerBody = string.Empty;

                            // make the inner FULLTEXT search
                            foreach (string word in words)
                            {
                                if (!bFirst)
                                {
                                  //  ftInner += " OR ";
                                    ftInnerBody += " | ";
                                }
                                else
                                {
                                    bFirst = false;
                                }

                               // ftInner += string.Format(@"""{0}""", word);
                                ftInnerBody += string.Format("{0}", word);

                                if (int.TryParse(word, out userId))
                                {
                                   // searchSql.Append(string.Format(" (c.userid IN ({0}))", userId));
                                }
                                else
                                {
                                    /* searchSql.Append(
                                        string.Format(
                                            " ((c.username IS NULL AND b.name LIKE '%{0}%') OR (c.username like '{0}%'))",
                                            word)); */
                                }
                            }

                            // make final string...
                            searchSql.Append(
                                string.Format(
                                    "SELECT to_tsvector('{0}',c.message) @@ to_tsquery('{0}','{1}') OR to_tsvector('{0}',a.topic) @@ to_tsquery('{0}','{1}') ",
                                    ftsLanguage,
                                    ftInnerBody.Trim().Trim('|').Trim()));
                        }
                        else
                        {
                            foreach (string word in words)
                            {
                                if (!bFirst) searchSql.Append(" OR ");
                                else bFirst = false;
                                searchSql.Append(
                                    string.Format("c.message ~* '.*{0}.*'  OR a.topic ~* '.*{0}.*' ", word));
                            }
                        }

                        break;
                    case SearchWhatFlags.ExactMatch:
                         words = toSearchWhat.Split(' ');
                        if (useFullText)
                        {
                            string ftInner = string.Empty;

                            // make the inner FULLTEXT search
                            foreach (string word in words)
                            {
                                if (!bFirst)
                                {
                                    ftInner += " & ";
                                }
                                else
                                {
                                    bFirst = false;
                                }

                                ftInner += string.Format("{0}", word);
                            }

                            // make final string...
                            searchSql.Append(
                                string.Format(
                                    "select to_tsvector('{0}',c.message) @@ to_tsquery('{0}','{1}') OR to_tsvector('{0}',a.topic) @@ to_tsquery('{0}','{1}') ",
                                     ftsLanguage, ftInner.Trim().Trim('&').Trim()));
                        }
                        else
                        {
                            searchSql.Append(
                                string.Format("c.message ~* '.*{0}.*'  OR a.topic ~* '.*{0}.*'  ", toSearchWhat));
                        }

                        break;
                }

                searchSql.Append(") ");
            }

            // vzrus
            if (categoriesIds.IsSet())
            {
                searchSql.Append(string.Format(" AND f.categoryid IN ({0})", categoriesIds));
            }

            if (forumIDs.IsSet())
            {
                searchSql.Append(string.Format(" AND a.forumid IN ({0})", forumIDs));
            }

            if (orderString != string.Empty)
            {
                orderString += ", ";
            }

            if (!orderString.Contains("ORDER BY"))
            {
                searchSql.Append("ORDER BY ");
            }

            searchSql.Append(orderString + "c.posted DESC ");

            if (!orderString.Contains("LIMIT"))
            {
                searchSql.Append(limitString);
            }

            return searchSql.ToString();
        }

        #endregion
    }
}