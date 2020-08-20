namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10102019_UPDATE_TABLE_PHF_BIENBAN_LAMVIEC_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BIENBAN_LAMVIEC", "NGUOILAP", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BIENBAN_LAMVIEC", "NGAYLAP", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_BIENBAN_LAMVIEC", "NGAYLAP");
            DropColumn("BTSTC.PHF_BIENBAN_LAMVIEC", "NGUOILAP");
        }
    }
}
