using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using System.Web.Security;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;


namespace BTS.SP.STC.Code.FBA.Provider
{
    public class FbaRoles: RoleProvider
    {
        public static RoleProvider BaseRoleProvider()
        {
            return Roles.Providers[GetRoleProvider()];
        }
        public static string GetRoleProvider()
        {
            return GetRoleProvider(SPContext.Current.Site);
        }
        public static string GetRoleProvider(SPSite site)
        {
            // get role provider of whichever zone in the web app is fba enabled 
            SPIisSettings settings = GetFBAIisSettings(site);
            return settings.FormsClaimsAuthenticationProvider.RoleProvider;
        }
        private static SPIisSettings GetFBAIisSettings(SPSite site)
        {
            SPIisSettings settings = null;
            // try and get FBA IIS settings from current site zone
            try
            {
                settings = site.WebApplication.IisSettings[site.Zone];
                if (settings.UseFormsClaimsAuthenticationProvider)
                    return settings;
            }
            catch
            {
                // expecting errors here so do nothing                 
            }
            // check each zone type for an FBA enabled IIS site
            foreach (SPUrlZone zone in Enum.GetValues(typeof(SPUrlZone)))
            {
                try
                {
                    settings = site.WebApplication.IisSettings[(SPUrlZone)zone];
                    if (settings.UseFormsClaimsAuthenticationProvider)
                        return settings;
                }
                catch
                {
                    // expecting errors here so do nothing                 
                }
            }
            // return null if FBA not enabled
            return null;
        }
        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[]{};

            //for (int i = 0; i < UserData.UserRoleDB.Count(); i++)
            //{
            //    string userEntry = UserData.UserRoleDB[i];
            //    string[] userAttribs = userEntry.Split(':');
            //    if (username == userAttribs[0] && userAttribs.Count()>1)
            //    {
            //        roles = userEntry.Substring(userEntry.IndexOf(':') + 1).Split(':');
            //        break;
            //    }
            //}

            return roles;
        }
        public override bool RoleExists(string roleName)
        {
            //foreach (string role in UserData.RoleDB)
            //{
            //    if (role == roleName)
            //        return true;
            //}

            return false;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     check current site to see if a provider has been specified in the web.config
        /// </summary>
        /// <returns></returns>
        public static bool IsProviderConfigured()
        {
            // attempt to get current users details
            int numUsers;
            try
            {
                FbaMembers.BaseMembershipProvider().GetAllUsers(0, 1, out numUsers);
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
