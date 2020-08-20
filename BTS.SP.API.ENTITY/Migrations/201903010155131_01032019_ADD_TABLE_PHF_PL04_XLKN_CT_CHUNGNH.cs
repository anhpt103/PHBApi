namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01032019_ADD_TABLE_PHF_PL04_XLKN_CT_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_PL04_XLKN_CT", "MA_FILE", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_PL04_XLKN_CT", "MA_FILE_PK", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_PL04_XLKN_CT", "MA_BAOCAO");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_PL04_XLKN_CT", "MA_BAOCAO", c => c.String(maxLength: 100));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT_CHA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_PL04_XLKN_CT", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_PL04_XLKN_CT", "MA_FILE");
        }
    }
}
