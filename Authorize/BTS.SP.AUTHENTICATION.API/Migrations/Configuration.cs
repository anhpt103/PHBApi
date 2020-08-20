namespace BTS.SP.AUTHENTICATION.API.Migrations
{
    using BTS.SP.AUTHENTICATION.API.Entities;
    using BTS.SP.AUTHENTICATION.API.Helper;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BTS.SP.AUTHENTICATION.API.AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BTS.SP.AUTHENTICATION.API.AuthContext context)
        {
            if (context.Clients.Count() > 0)
            {
                return;
            }

            context.Clients.AddRange(BuildClientsList());
            context.SaveChanges();
        }

        private static List<Client> BuildClientsList()
        {

            List<Client> ClientsList = new List<Client>
            {
                new Client
                {
                    Id = "ngAuthApp",
                    Secret = MD5Encrypt.GetHash("abc@123"),
                    Name = "AngularJS front-end Application",
                    ApplicationType = Models.ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "http://ngauthenticationweb.azurewebsites.net"
                },
                new Client
                {
                    Id = "consoleApp",
                    Secret = MD5Encrypt.GetHash("123@abc"),
                    Name = "Console Application",
                    ApplicationType = Models.ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }

    }
}
