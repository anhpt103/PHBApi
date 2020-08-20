namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20032019addTblNhanDLXa_kienv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_BAOCAO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "NAM_BC", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "THANG_BC", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "DONVI", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "NGAY_TAO", c => c.DateTime());
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "NGUOI_TAO", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MAQHNS");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "CHUONG");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MACTMT");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "KHOAN");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "TIEUMUC");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MANV");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "SOTIEN");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "LOAI");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MUC");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "NHOM");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "TIEUNHOM");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_KHOBAC");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_CAPNGANSACH");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_DBHC");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_DBHC", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_CAPNGANSACH", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_KHOBAC", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "TIEUNHOM", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "NHOM", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MUC", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "LOAI", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "SOTIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MANV", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "TIEUMUC", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "KHOAN", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MACTMT", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "CHUONG", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MAQHNS", c => c.String(maxLength: 10));
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "NGUOI_TAO");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "NGAY_TAO");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "DONVI");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "THANG_BC");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "NAM_BC");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_BAOCAO");
        }
    }
}
