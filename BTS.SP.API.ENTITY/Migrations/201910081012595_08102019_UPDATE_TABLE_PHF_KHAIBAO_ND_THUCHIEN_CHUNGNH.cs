namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08102019_UPDATE_TABLE_PHF_KHAIBAO_ND_THUCHIEN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KHAIBAO_ND_THUCHIEN", "NGUOILAP", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_KHAIBAO_ND_THUCHIEN", "NGAYLAP", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KHAIBAO_ND_THUCHIEN", "NGAYLAP");
            DropColumn("BTSTC.PHF_KHAIBAO_ND_THUCHIEN", "NGUOILAP");
        }
    }
}
