namespace VZF.Kernel
{
    using System.DirectoryServices.AccountManagement;

    using VZF.Types.Objects;

    public class DsAccountManagement
    {
        public bool IsAuthenticated(string userName)
        {
           
            // set up domain context
            var ctx = new PrincipalContext(ContextType.Domain);
           
            // find a user
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userName);

            if (user != null)
            {
                // do something here.... 
                return true;
            }
            return false;
        }

        public UserLdap GetUserPrincipal(string userName)
        {
            // set up domain context
            var ctx = new PrincipalContext(ContextType.Domain);
          
            // find a user
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userName);
            if (user != null)
            {
                return new UserLdap { DisplayName = user.DisplayName, Name = user.Name, Email = user.EmailAddress };
            }
            return null;
        }
    }
}
