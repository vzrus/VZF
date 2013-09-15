using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VZF.Kernel.Ldap
{
    using System.Collections.Generic;
    using System.DirectoryServices.Protocols;
    using System.Globalization;
    using System.Net;
    using System.Security;


    public class LDAPHelper
    {
        private readonly LdapConnection ldapConnection;

        private readonly string searchBaseDN;

        private readonly int pageSize;

        public LDAPHelper(
            string searchBaseDN,
            string hostName,
            int portNumber,
            AuthType authType,
            string connectionAccountName,
            SecureString connectionAccountPassword,
            int pageSize)
        {

            var ldapDirectoryIdentifier = new LdapDirectoryIdentifier(hostName, portNumber, true, false);

            var networkCredential = new NetworkCredential(connectionAccountName, connectionAccountPassword);

            ldapConnection = new LdapConnection(ldapDirectoryIdentifier, networkCredential) { AuthType = authType };

            ldapConnection.SessionOptions.ProtocolVersion = 3;

            this.searchBaseDN = searchBaseDN;
            this.pageSize = pageSize;
        }

        public IEnumerable<SearchResultEntryCollection> PagedSearch(string searchFilter, string[] attributesToLoad)
        {

            var pagedResults = new List<SearchResultEntryCollection>();

            var searchRequest = new SearchRequest(searchBaseDN, searchFilter, SearchScope.Subtree, attributesToLoad);


            var searchOptions = new SearchOptionsControl(SearchOption.DomainScope);
            searchRequest.Controls.Add(searchOptions);

            var pageResultRequestControl = new PageResultRequestControl(pageSize);
            searchRequest.Controls.Add(pageResultRequestControl);

            while (true)
            {
                var searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);
                var pageResponse = (PageResultResponseControl)searchResponse.Controls[0];

                yield return searchResponse.Entries;
                if (pageResponse.Cookie.Length == 0) break;

                pageResultRequestControl.Cookie = pageResponse.Cookie;
            }


        }

        // usage
        static void Get(string[] args)
        {
            var password = new[] { 'P', 'a', 's', 's', 'w', '@', 'r', 'd' };
            var secureString = new SecureString();
            foreach (var character in password)
                secureString.AppendChar(character);

            var baseOfSearch = "dc=fabrikam,dc=com";
            var ldapHost = "ubuntu.fabrikam.com";
            var ldapPort = 636; //SSL
            var connectAsDN = "cn=admin,dc=fabrikam,dc=com";
            var pageSize = 1000;

            var openLDAPHelper = new LDAPHelper(
                baseOfSearch,
                ldapHost,
                ldapPort,
                AuthType.Basic,
                connectAsDN,
                secureString,
                pageSize);

            var searchFilter = "nextUID=*";
            var attributesToLoad = new[] { "nextUID" };
            var pagedSearchResults = openLDAPHelper.PagedSearch(
                searchFilter,
                attributesToLoad);

            foreach (var searchResultEntryCollection in pagedSearchResults)
                foreach (SearchResultEntry searchResultEntry in searchResultEntryCollection)
                    Console.WriteLine(searchResultEntry.Attributes["nextUID"][0]);

            Console.Read();

        }
    }
}

