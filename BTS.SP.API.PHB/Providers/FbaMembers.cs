using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.Diagnostics;
using System.Reflection;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Collections;

namespace BTS.SP.API.PHB.Providers
{
    public class FbaMembers
    {
        //TODO: Inherit from Sharepoint membership provider and fix all unimplemented functions/properties.  Then go through code and get rid 
        //of all references to Utils that are used to work around this.

        public static MembershipProvider BaseMembershipProvider()
        {
            return Membership.Providers["FBAMembershipSTCProvider"];
        }

        public static RoleProvider BaseRoleProvider()
        {
            return Roles.Providers["FBARoleSTCProvider"];
        }
       

        /// <summary>
        /// check current site to see if a provider has been specified in the web.config
        /// </summary>
        /// <returns></returns>
        public static bool IsProviderConfigured()
        {
            // attempt to get current users details
            int numUsers;
            try
            {
                BaseMembershipProvider().GetAllUsers(0, 1, out numUsers);
            }
            catch
            {
                // if fails membership provider is not configured correctly
                return false;
            }

            // if no error provider is ok
            return true;
        }

    }
}
