using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.STC.Code.FBA
{
    public class APIFunction
    {
        public static string GetToken(string userName, string password)
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["url_Api"].ToString() + "/token";
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", userName ),
                        new KeyValuePair<string, string> ( "Password", password )
                    };

            //var pairs = new List<KeyValuePair<string, string>>
            //        {
            //            new KeyValuePair<string, string>( "grant_type", "password" ),
            //            new KeyValuePair<string, string>( "username", userName ),
            //            new KeyValuePair<string, string> ( "password", password ),
            //            new KeyValuePair<string, string> ( "client_id", "koFeApp" ),
            //            new KeyValuePair<string, string> ( "donvi", "null" )
            //        };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        public static string GetAppSettingUsingConfigurationManager(string customField)
        {
            return System.Configuration.ConfigurationManager.AppSettings[customField];
        }
        public static string GetAppSetting(string customField)
        {
            System.Configuration.Configuration config =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            if (config.AppSettings.Settings.Count > 0)
            {
                var customSetting = config.AppSettings.Settings[customField].ToString();
                if (!string.IsNullOrEmpty(customSetting))
                {
                    return customSetting;
                }
            }
            return null;
        }
        //    <appSettings>
        //    <add key = "webpages:Version" value="3.0.0.0" />
        //    <add key = "webpages:Enabled" value="false" />
        //    <add key = "ClientValidationEnabled" value="true" />
        //    <add key = "UnobtrusiveJavaScriptEnabled" value="true" />
        //    <add key = "DatabaseINILocation" value="C:\config\config.ini" />
        //    <add key = "testPort" value="49610" />
        //</appSettings>
    }
}
