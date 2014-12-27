using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAF.Types.Constants;

namespace VZF.Data.Firebird.Search
{
    public class SearchFromWho
    {
        public string Build(string searchSql, SearchWhatFlags searchFromWhoMethod, string toSearchFromWho, bool useFullText, bool searchDisplayName)
        {
            bool bFirst = true;
            int userId;
         // generate user search sql...
                switch (searchFromWhoMethod)
                {
                    case SearchWhatFlags.AllWords:
                      string[] words  = toSearchFromWho.Split(' ');
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                searchSql += " AND ";
                            }
                            else
                            {
                                bFirst = false;
                            }

                            searchSql +=
                                string.Format(
                                    @" ((c.USERNAME IS NULL AND b.NAME LIKE '%{0}%') OR (c.USERNAME LIKE '%{0}%'))",
                                    word);
                            if (int.TryParse(word, out userId))
                            {
                                searchSql += string.Format(" (c.UserID IN ({0}))", userId);
                            }
                            else
                            {
                                if (searchDisplayName)
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.Username IS NULL AND b.DisplayName LIKE '%{0}%') OR (c.Username LIKE '%{0}%'))",
                                            word);
                                }
                                else
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.Username IS NULL AND b.Name LIKE '%{0}%') OR (c.Username LIKE '%{0}%'))",
                                            word);
                                }
                            }
                        }

                        break;
                    case SearchWhatFlags.AnyWords:
                        words = toSearchFromWho.Split(' ');
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                searchSql += " OR ";
                            }
                            else
                            {
                                if (searchDisplayName)
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.USERNAME IS NULL AND b.DISPLAYNAME = '{0}') OR (c.Username = '{0}')",
                                            toSearchFromWho);
                                }
                                else
                                {
                                    searchSql +=
                                        string.Format(
                                            @" ((c.USERNAME IS NULL AND b.NAME LIKE '%{0}%') OR (c.USERNAME LIKE '%{0}%'))",
                                            word);
                                }
                            }
                        }

                        break;
                    case SearchWhatFlags.ExactMatch:
                        if (int.TryParse(toSearchFromWho, out userId))
                        {
                            searchSql += string.Format(" (c.UserID IN ({0}))", userId);
                        }
                        else
                        {
                            searchSql +=
                                string.Format(
                                    @" ((c.USERNAME IS NULL AND b.NAME = '{0}' ) OR (c.USERNAME = '{0}' ))",
                                    toSearchFromWho);
                        }

                        break;
                }
                return searchSql;
        }
    }
   
}
