using BTS.SP.AUTHENTICATION.API.Dm.Entities;
using BTS.SP.AUTHENTICATION.API.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BTS.SP.AUTHENTICATION.API
{
    public class AuthContext : DataContext
    {
        public AuthContext()
            : base("AuthContext")
        {

        }

        #region Danh mục
        public DbSet<DM_DBHC> DM_DBHCs { get; set; } 
        #endregion

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AU_NGUOIDUNG> AU_NGUOIDUNGs { get; set; }
        public DbSet<AU_NGUOIDUNG_NHOMQUYEN> AU_NGUOIDUNG_NHOMQUYENs { get; set; }
        public DbSet<AU_NGUOIDUNG_QUYEN> AU_NGUOIDUNG_QUYENs { get; set; }
        public DbSet<AU_NHOMQUYEN> AU_NHOMQUYENs { get; set; }
        public DbSet<AU_NHOMQUYEN_CHUCNANG> AU_NHOMQUYEN_CHUCNANGs { get; set; }
        public DbSet<LogSignin> LOG_SIGNINs { get; set; }
        public DbSet<SYS_CHUCNANG> SYS_CHUCNANGs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BTAUTH");
            base.OnModelCreating(modelBuilder);
        }

    }

}