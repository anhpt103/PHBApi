namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19082019_UPDATE_PHF_NHAPBAOCAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "HANBAOCAO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "SOBAOCAO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "CHUCVU", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOILAP");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOILAP", c => c.String(maxLength: 100));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "CHUCVU");
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "SOBAOCAO");
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "HANBAOCAO");
        }
    }
}
