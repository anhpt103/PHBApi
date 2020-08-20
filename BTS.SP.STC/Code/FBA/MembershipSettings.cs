using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using Microsoft.SharePoint;
using BTS.SP.STC.Code.FBA.Provider;

namespace BTS.SP.STC.Code.FBA
{
    public enum MembershipStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }
    public struct MembershipOptions
    {
        public const string ENABLEROLES = "EnableRoles";
        public const string REVIEWMEMBERSHIPREQUESTS = "ReviewMembershipRequests";
    }
    public struct MembershipReviewListFields
    {
        public const string REQUESTID = "RequestID";
        public const string FIRSTNAME = "FirstName";
        public const string LASTNAME = "LastNamePhonetic";
        public const string USERNAME = "Title";
        public const string EMAIL = "Email";
        public const string DATESUBMITTED = "_DCDateCreated";
        public const string STATUS = "_Status";
        public const string RECOVERPASSWORDQUESTION = "RecoverPasswordSecretQuestion";
        public const string RECOVERPASSWORDANSWER = "RecoverPasswordSecretAnswer";
        public const string DEFAULTGROUP = "DefaultGroup";
    }
    public struct MembershipReviewSiteURL
    {
        public const string CHANGEPASSWORDPAGE = "ChangePasswordPage";
        public const string PASSWORDQUESTIONPAGE = "PasswordReminderPage";
        public const string THANKYOUPAGE = "ThankYouPage";
    }
    public struct MembershipReviewSiteXSLTEmail
    {
        public const string MEMBERSHIPREPLYTO = "MembershipReplyTo";
        public const string MEMBERSHIPAPPROVED = "MembershipApprovedXSLT";
        public const string MEMBERSHIPPENDING = "MembershipPendingXSLT";
        public const string MEMBERSHIPREJECTED = "MembershipRejectedXSLT";
        public const string PASSWORDRECOVERY = "PasswordRecoveryXSLT";
        public const string RESETPASSWORD = "ResetPasswordXSLT";
    }
    public struct MembershipReviewMigratedFields
    {
        public const string MEMBERSHIPAPPROVED = "MembershipApproved";
        public const string MEMBERSHIPERROR = "MembershipError";
        public const string MEMBERSHIPPENDING = "MembershipPending";
        public const string MEMBERSHIPREJECTED = "MembershipRejected";
        public const string PASSWORDRECOVERY = "PasswordRecovery";
    }
    public struct MembershipList
    {
        public const string MEMBERSHIPREVIEWLIST = "Site Membership Review List";
    }
    public class MembershipSettings
    {
        private readonly SPSite _site;
        private readonly SPWeb _web;

        public MembershipSettings(SPWeb web)
        {
            _web = web;
            _site = _web.Site;
        }

        public bool EnableRoles
        {
            get { return FbaSites.GetSiteProperty(MembershipOptions.ENABLEROLES, false, _site); }

            set { FbaSites.SetSiteProperty(MembershipOptions.ENABLEROLES, value, _site); }
        }

        public bool ReviewMembershipRequests
        {
            get { return FbaSites.GetSiteProperty(MembershipOptions.REVIEWMEMBERSHIPREQUESTS, false, _site); }

            set { FbaSites.SetSiteProperty(MembershipOptions.REVIEWMEMBERSHIPREQUESTS, value, _site); }
        }

        public string ChangePasswordPage
        {
            get
            {
                return FbaSites.GetWebProperty(MembershipReviewSiteURL.CHANGEPASSWORDPAGE, "_Layouts/FBA/ChangePassword.aspx",
                _web);
            }

            set { FbaSites.SetWebProperty(MembershipReviewSiteURL.CHANGEPASSWORDPAGE, value, _web); }
        }

        public string PasswordQuestionPage
        {
            get
            {
                return FbaSites.GetWebProperty(MembershipReviewSiteURL.PASSWORDQUESTIONPAGE, "Pages/PasswordQuestion.aspx",
                _web);
            }

            set { FbaSites.SetWebProperty(MembershipReviewSiteURL.PASSWORDQUESTIONPAGE, value, _web); }
        }

        public string ThankYouPage
        {
            get { return FbaSites.GetWebProperty(MembershipReviewSiteURL.THANKYOUPAGE, "Pages/Thankyou.aspx", _web); }

            set { FbaSites.SetWebProperty(MembershipReviewSiteURL.THANKYOUPAGE, value, _web); }
        }

        public string MembershipReplyToEmailAddress
        {
            get
            {
                return FbaSites.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREPLYTO,
                _web.Site.WebApplication.OutboundMailReplyToAddress, _web);
            }

            set { FbaSites.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREPLYTO, value, _web); }
        }

        public string MembershipApprovedEmail
        {
            get
            {
                return FbaSites.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED,
                GetMigratedXSLT(MembershipReviewMigratedFields.MEMBERSHIPAPPROVED, "MembershipApprovedXSLT"), _web,
                true);
            }

            set { FbaSites.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED, value, _web); }
        }

        public string MembershipPendingEmail
        {
            get
            {
                return FbaSites.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPPENDING,
                GetMigratedXSLT(MembershipReviewMigratedFields.MEMBERSHIPPENDING, "MembershipPendingXSLT"), _web, true);
            }

            set { FbaSites.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPPENDING, value, _web); }
        }

        public string MembershipRejectedEmail
        {
            get
            {
                return FbaSites.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREJECTED,
                GetMigratedXSLT(MembershipReviewMigratedFields.MEMBERSHIPREJECTED, "MembershipRejectedXSLT"), _web,
                true);
            }

            set { FbaSites.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREJECTED, value, _web); }
        }

        public string PasswordRecoveryEmail
        {
            get
            {
                return FbaSites.GetWebProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY,
                GetMigratedXSLT(MembershipReviewMigratedFields.PASSWORDRECOVERY, "PasswordRecoveryXSLT"), _web, true);
            }

            set { FbaSites.SetWebProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY, value, _web); }
        }

        public string ResetPasswordEmail
        {
            get { return FbaSites.GetWebProperty(MembershipReviewSiteXSLTEmail.RESETPASSWORD, "ResetPasswordXSLT", _web, true); }

            set { FbaSites.SetWebProperty(MembershipReviewSiteXSLTEmail.RESETPASSWORD, value, _web); }
        }

        private string GetTemplateDefaultPath(string filename, SPWeb web)
        {
            var path = string.Format("/_layouts/FBA/emails/{0}/{1}", CultureInfo.CurrentUICulture.LCID.ToString(),
                filename);

            //Return the localized path if it exists, otherwise return the default path
            try
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(path))) return path;
            }
            catch
            {
            }

            return string.Format("/_layouts/FBA/emails/{0}", filename);
        }

        /// <summary>
        ///     Returns the contents of the file specified in the migrated key - if it exists
        ///     If the file doesn't exist, then just return the default value
        /// </summary>
        /// <param name="migratedKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private string GetMigratedXSLT(string migratedKey, string defaultValue)
        {
            var result = defaultValue;

            try
            {
                var url = FbaSites.GetWebProperty(migratedKey, "", _web);

                if (!string.IsNullOrEmpty(url))
                {
                    url = FbaSites.GetAbsoluteURL(_web, url);
                    var contents = string.Empty;
                    var request = WebRequest.Create(url);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    using (var response = request.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                        }

                        response.Close();
                    }
                }
            }
            catch
            {
            }

            return result;
        }
    }
}