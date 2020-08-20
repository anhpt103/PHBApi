namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16092019_UPDATE_TABLE_COLUMN_PHF_NHAPBAOCAO_CHITIET_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOIDUNG_CHINHSUA", c => c.String(maxLength: 2000));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOI_DUNG_STRING", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOI_DUNG_STRING", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOIDUNG_CHINHSUA");
        }
    }
}
