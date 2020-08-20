using BTS.SP.AUTHENTICATION.API.Au.AuNguoiDung;
using BTS.SP.AUTHENTICATION.API.Dm.Entities;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.ServiceFunc.DmDBHC;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BTS.SP.AUTHENTICATION.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private IAuNguoiDungService _service;
        private IDM_DBHCService _DBHCService;
        //private IMdLogSigninService _LogSigninService;



        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = new Client();

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }


            using (AuthRepository _repo = new AuthRepository())
            {
                client = _repo.FindClient(context.ClientId);
            }

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == Models.ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != MD5Encrypt.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string madiaban = "";
            var tendiaban = System.Web.HttpContext.Current.Request["tendiaban"];
            switch (tendiaban)
            {
                case "yenbai":
                    madiaban = "15";
                    break;
                case "langson":
                    madiaban = "20";
                    break;
                case "tuyenquang":
                    madiaban = "08";
                    break;
                case "backan":
                    madiaban = "06";
                    break;
                case "ninhbinh":
                    madiaban = "37";
                    break;
                case "hungyen":
                    madiaban = "33";
                    break;
                case "bacninh":
                    madiaban = "27";
                    break;
            }
            _DBHCService = new DM_DBHCService(new UnitOfWork(new AuthContext()));

            var lisDBHC = _DBHCService.GetListDbhc(madiaban);



            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            _service = new AuNguoiDungService(new UnitOfWork(new AuthContext()));
            var user = _service.FindUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");

                return;
            }

            if (user.MA_DONVI == "ERROR")
            {
                //context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.SetError("invalid_grant", user.USERNAME);

                return;
            }
            //var temp = new LogSignin
            //{
            //    Username = user.USERNAME,
            //    DBHC = user.MA_DBHC,
            //    DBHC_Cha = user.MA_DBHC_CHA,
            //    //Ten_DBHC = _dbhcService.Repository.DbSet.Where(dbhc => dbhc.MA_DBHC == user.MA_DBHC).FirstOrDefault().TEN_DBHC
            //};
            //var ipHost = HttpContext.Current.Request.UserHostAddress;
            //temp.DiaChiMay = ipHost;
            //temp.ChucNang = "Đăng nhập hệ thống";
            //temp.ThoiGianTruyCap = DateTime.Now;
            //try
            //{
            //    _LogSigninService.Insert(temp, false);
            //    await _LogSigninService.UnitOfWork.SaveAsync();
            //}
            //catch (Exception)
            //{
            //    //
            //}

            Action<ClaimsIdentity, string> addClaim = (ClaimsIdentity obj, string username) => { return; };
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "MEMBER"));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Country, user.MA_DBHC));
            identity.AddClaim(new Claim("MA_DBHC", user.MA_DBHC));
            identity.AddClaim(new Claim("MA_DONVI", string.IsNullOrEmpty(user.MA_DONVI) ? string.Empty : user.MA_DONVI));
            identity.AddClaim(new Claim("MA_DBHC_CHA", user.MA_DBHC_CHA));
            identity.AddClaim(new Claim("LOAIUSER", user.LOAI.ToString()));
            identity.AddClaim(new Claim("CAPUSER", user.CAP.ToString()));
            identity.AddClaim(new Claim("MA_QHNS_QL", string.IsNullOrEmpty(user.MA_QHNS) ? string.Empty : user.MA_QHNS));
            addClaim.Invoke(identity, user.USERNAME);
            var tempDBHC = _service.UnitOfWork.Repository<DM_DBHC>().DbSet.FirstOrDefault(x => x.MA_DBHC == user.MA_DBHC);

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "email", string.IsNullOrEmpty(user.EMAIL)?string.Empty:user.EMAIL
                    },
                    {
                        "fullName", string.IsNullOrEmpty(user.FULLNAME)?string.Empty:user.FULLNAME
                    },
                    {
                        "phone", string.IsNullOrEmpty(user.PHONE)?string.Empty:user.PHONE
                    },
                    {
                        "chucVu", string.IsNullOrEmpty(user.CHUCVU)?string.Empty:user.CHUCVU
                    },
                    {
                        "maDBHC", string.IsNullOrEmpty(user.MA_DBHC)?string.Empty:user.MA_DBHC
                    },
                    {
                        "tenDBHC", tempDBHC.TEN_DBHC
                    },
                    {
                        "PhanHe", "A"
                    },
                    {
                        "maDiaBanHanhChinhCha", string.IsNullOrEmpty(user.MA_DBHC_CHA)?string.Empty:user.MA_DBHC_CHA
                    },
                    {
                        "dV_QHNS", string.IsNullOrEmpty(user.MA_QHNS)? string.Empty: user.MA_QHNS
                    },
                    {
                        "loai_dv", string.IsNullOrEmpty(user.LOAI.ToString())? string.Empty: user.LOAI.ToString()
                    },
                    {
                        "cap_dv", string.IsNullOrEmpty(user.CAP.ToString())? string.Empty: user.CAP.ToString()
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(identity);

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

    }
}