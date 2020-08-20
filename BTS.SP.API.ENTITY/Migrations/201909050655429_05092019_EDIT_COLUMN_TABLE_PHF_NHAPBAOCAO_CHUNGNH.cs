namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05092019_EDIT_COLUMN_TABLE_PHF_NHAPBAOCAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOILAP", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NOIDUNG");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NOIDUNG", c => c.String(maxLength: 1500));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOILAP");
        }
    }
}
