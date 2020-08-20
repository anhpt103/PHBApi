namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27032019_addField_kien : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_DBHC_NHAP", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "TEN_DBHC_NHAP", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "TEN_DBHC_NHAP");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_DBHC_NHAP");
        }
    }
}
