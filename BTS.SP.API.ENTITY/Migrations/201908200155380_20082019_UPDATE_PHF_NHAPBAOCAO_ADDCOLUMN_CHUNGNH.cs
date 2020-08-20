namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20082019_UPDATE_PHF_NHAPBAOCAO_ADDCOLUMN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "CHUCVU_NGUOILAP", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "CHUCVU_NGUOIKY", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "CHUCVU");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "CHUCVU", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "CHUCVU_NGUOIKY");
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "CHUCVU_NGUOILAP");
        }
    }
}
