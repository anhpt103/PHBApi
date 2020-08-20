namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15102019_UPDATE_TABLE_PHF_TIENDO_TUAN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "QUY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "TENQUY", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "MA_TUAN");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "MA_TUAN");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "MA_TUAN", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "MA_TUAN", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "TENQUY");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "QUY");
        }
    }
}
