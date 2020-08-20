namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09102019_UPDATE_TABLE_PHF_KHAOSATKEKHAI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "NGUOILAP", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "NGAYLAP", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "NGAYLAP");
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "NGUOILAP");
        }
    }
}
