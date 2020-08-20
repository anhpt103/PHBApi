namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14102019_UPDATE_TABLE_PHF_TIENDO_TUAN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "NGUOILAP", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "NGAYLAP", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "NGAYLAP");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "NGUOILAP");
        }
    }
}
