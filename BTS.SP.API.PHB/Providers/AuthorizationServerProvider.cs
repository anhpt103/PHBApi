using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.ENTITY.Helper;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Oracle.ManagedDataAccess.Client;

namespace BTS.SP.API.PHB.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // try
            try
            {
                var user = new AU_NGUOIDUNG();
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText =
                            "SELECT * FROM AU_NGUOIDUNG WHERE USERNAME='" + context.UserName + "' AND PASSWORD='" + MD5Encrypt.MD5Hash(context.Password) + "' AND TRANGTHAI=1";
                        using (var oracleDataReader = command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            if (!oracleDataReader.Result.HasRows)
                            {
                                user = null;
                            }
                            else
                            {
                                while (oracleDataReader.Result.Read())
                                {
                                    user.USERNAME = oracleDataReader.Result["USERNAME"]?.ToString();
                                    user.FULLNAME = oracleDataReader.Result["FULLNAME"]?.ToString();
                                    user.PHONE = oracleDataReader.Result["PHONE"]?.ToString();
                                    user.EMAIL = oracleDataReader.Result["EMAIL"]?.ToString();
                                    user.MA_DBHC = oracleDataReader.Result["MA_DBHC"]?.ToString();
                                    user.MA_QHNS = oracleDataReader.Result["MA_QHNS"]?.ToString();
                                    user.CHUCVU = oracleDataReader.Result["CHUCVU"]?.ToString();
                                    user.MA_PHONGBAN = oracleDataReader.Result["MA_PHONGBAN"]?.ToString();
                                    user.MA_DONVI = oracleDataReader.Result["MA_DONVI"]?.ToString();
                                }
                            }
                        }
                    }
                }
                Action<ClaimsIdentity, string> addClaim = (ClaimsIdentity obj, string username) => { return; };
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                if (user != null)
                {
                    addClaim.Invoke(identity, user.USERNAME);
                    identity.AddClaim(new Claim(ClaimTypes.Role,"Administrator"));
                    identity.AddClaim(new Claim("MA_DBHC", user.MA_DBHC));
                    identity.AddClaim(new Claim("LST_QHNS", user.MA_QHNS));
                    identity.AddClaim(new Claim("MA_DONVI", user.MA_DONVI));

                    var props = new AuthenticationProperties(
                        new Dictionary<string, string>{
                            {
                                "userName",user.USERNAME
                            },
                            {
                                "email", user.EMAIL
                            },
                            {
                                "fullName", user.FULLNAME
                            },
                            {
                                "phone", user.PHONE
                            },
                            {
                                "maDBHC", user.MA_DBHC
                            },
                            {
                                "lstQHNS",user.MA_QHNS
                            },
                            {
                                "chucVu",user.CHUCVU
                            },
                            {
                                "maPhongBan", string.IsNullOrEmpty(user.MA_PHONGBAN)?string.Empty:user.MA_PHONGBAN
                            },
                           {
                                "maDonVi", string.IsNullOrEmpty(user.MA_DONVI)?string.Empty:user.MA_DONVI
                            }

                    });
                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }
                else
                {
                    context.SetError("invalid_grant", "Sai thông tin đăng nhập !");
                    return;
                }
            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
                context.SetError("invalid_grant", "Lỗi hệ thống !");
                return;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}