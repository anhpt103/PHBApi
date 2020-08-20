using System;
using System.Web;
using Microsoft.SharePoint.Administration;
using System.Diagnostics;
using Microsoft.SharePoint;
using System.Web.Security;
using Microsoft.SharePoint.Utilities;
using System.Linq;
using Microsoft.SharePoint.Administration.Claims;

namespace BTS.SP.STC.Code.FBA.Provider
{
    public class FbaSites
    {
        public static string GetMembershipProvider()
        {
            return GetMembershipProvider(SPContext.Current.Site);
        }

        public static string GetRoleProvider()
        {
            return GetRoleProvider(SPContext.Current.Site);
        }

        public static string GetRoleProvider(SPSite site)
        {
            // get role provider of whichever zone in the web app is fba enabled 
            var settings = GetFBAIisSettings(site);
            return settings.FormsClaimsAuthenticationProvider.RoleProvider;
        }

        public static string GetMembershipProvider(HttpContext context)
        {
            using (var site = new SPSite(SPUtility.GetPageUrlPath(context)))
            {
                return GetMembershipProvider(site);
            }
        }

        public static string GetMembershipProvider(SPSite site)
        {
            // get membership provider of whichever zone in the web app is fba enabled 
            var settings = GetFBAIisSettings(site);
            if (settings == null) return null;
            return settings.FormsClaimsAuthenticationProvider.MembershipProvider;
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
                try
                {
                    settings = site.WebApplication.IisSettings[zone];
                    if (settings.UseFormsClaimsAuthenticationProvider)
                        return settings;
                }
                catch
                {
                    // expecting errors here so do nothing                 
                }

            // return null if FBA not enabled
            return null;
        }
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
        public static bool GetSiteProperty(string key, bool defaultValue)
        {
            return GetSiteProperty(key, defaultValue, SPContext.Current.Site);
        }
        public static string GetWebProperty(string key, string defaultValue, SPWeb web, bool save)
        {
            string value = null;
            value = web.Properties[key];
            if (value == null)
            {
                value = defaultValue;
                if (save) SetWebProperty(key, value, web);
            }

            return value;
        }
        public static string GetWebProperty(string key, string defaultValue, SPWeb web)
        {
            return GetWebProperty(key, defaultValue, web, false);
        }

        public static bool GetSiteProperty(string key, bool defaultValue, SPSite site)
        {
            var result = defaultValue;

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (var privSite = new SPSite(site.ID, site.Zone))
                {
                    var web = privSite.RootWeb;
                    result = bool.Parse(GetWebProperty(key, defaultValue.ToString(), web));
                }
            });
            return result;
        }
      

        public static void SetWebProperty(string key, string value, SPWeb web)
        {
            var unsafeUpdates = web.AllowUnsafeUpdates;

            web.AllowUnsafeUpdates = true;
            web.Properties[key] = value;

            web.Properties.Update();
            web.AllowUnsafeUpdates = unsafeUpdates;
        }

        public static void SetWebProperty(string key, string value)
        {
            SetWebProperty(key, value, SPContext.Current.Web);
        }

        public static void SetSiteProperty(string key, bool value, SPSite site)
        {
            SetWebProperty(key, value.ToString(), site.RootWeb);
        }

        public static void SetSiteProperty(string key, bool value)
        {
            SetWebProperty(key, value.ToString(), SPContext.Current.Site.RootWeb);
        }

        public static string GetAbsoluteURL(SPWeb web, string path)
        {
            return SPUtility.ConcatUrls(web.Url, path);
        }

        public static int GetChoiceIndex(SPFieldChoice field, string value)
        {
            if (field == null || value == null) return -1;
            for (var i = 0; i < field.Choices.Count; i++)
                if (field.Choices[i] == value)
                    return i;

            return -1;
        }
    }
}
