using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAF.Types.Constants;

namespace VZF.Data.Firebird.Search
{
    public class SearchWhat
    {
        public string Build(string searchSql, SearchWhatFlags searchWhatMethod, string toSearchWhat, bool useFullText)
        {
            bool bFirst = true;
            // generate message and topic search sql...
            switch (searchWhatMethod)
            {
                case SearchWhatFlags.AllWords:
                    string[] words = toSearchWhat.Split(' ');
                    if (useFullText)
                    {
                        string ftInner = string.Empty;

                        // make the inner FULLTEXT search
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                ftInner += " AND ";
                            }
                            else
                            {
                                bFirst = false;
                            }

                            ftInner += string.Format(@"""{0}""", word);
                        }

                        // make final string...
                        searchSql +=
                            string.Format(
                                @"( CONTAINS (c.MESSAGE, ' {0} ') OR CONTAINS (a.TOPIC, ' {0} ' ) )",
                                ftInner);
                    }
                    else
                    {
                        foreach (string word in words)
                        {
                            if (!bFirst) searchSql += " AND ";
                            else bFirst = false;
                            searchSql += string.Format(@"(c.MESSAGE like '%{0}%' OR a.TOPIC LIKE '%{0}%' )", word);
                        }
                    }

                    break;
                case SearchWhatFlags.AnyWords:
                    words = toSearchWhat.Split(' ');

                    if (useFullText)
                    {
                        string ftInner = string.Empty;

                        // make the inner FULLTEXT search
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                ftInner += " OR ";
                            }
                            else
                            {
                                bFirst = false;
                            }

                            ftInner += string.Format(@"""{0}""", word);
                        }

                        // make final string...
                        searchSql +=
                            string.Format(
                                @"( CONTAINS (c.MESSAGE, ' {0} ' ) OR CONTAINS (a.TOPIC, ' {0} ' ) )",
                                ftInner);
                    }
                    else
                    {
                        foreach (string word in words)
                        {
                            if (!bFirst) searchSql += " OR ";
                            else bFirst = false;
                            searchSql += string.Format(@"c.MESSAGE LIKE '%{0}%'  OR a.TOPIC LIKE '%{0}%' ", word);
                        }
                    }

                    break;
                case SearchWhatFlags.ExactMatch:
                    if (useFullText)
                    {
                        // searchSql += string.Format(@"( CONTAINS (c.MESSAGE, ' \"{0}\" ' ) OR CONTAINS (a.Topic, ' \"{0}\" '  )", toSearchWhat);
                    }
                    else
                    {
                        searchSql += string.Format(
                            @"c.MESSAGE LIKE '%{0}%'  OR a.TOPIC LIKE '%{0}%'  ",
                            toSearchWhat);
                    }

                    break;
            }

            return searchSql;
        }
    }
}
