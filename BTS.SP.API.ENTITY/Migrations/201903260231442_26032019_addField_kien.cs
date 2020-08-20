namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26032019_addField_kien : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "LOAI_BC", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "TEN_DONVI", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_DBHC", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "TEN_DBHC", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_BAOCAOPK");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_BAOCAOPK", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "TEN_DBHC");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_DBHC");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "TEN_DONVI");
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "LOAI_BC");
        }
    }
}
