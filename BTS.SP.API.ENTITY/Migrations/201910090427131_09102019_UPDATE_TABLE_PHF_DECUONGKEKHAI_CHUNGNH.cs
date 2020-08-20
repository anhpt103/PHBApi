namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09102019_UPDATE_TABLE_PHF_DECUONGKEKHAI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DECUONGKEKHAI", "NGUOILAP", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_DECUONGKEKHAI", "NGAYLAP", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DECUONGKEKHAI", "NGAYLAP");
            DropColumn("BTSTC.PHF_DECUONGKEKHAI", "NGUOILAP");
        }
    }
}
