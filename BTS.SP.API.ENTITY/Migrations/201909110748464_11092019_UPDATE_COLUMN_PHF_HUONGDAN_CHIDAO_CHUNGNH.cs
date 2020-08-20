namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11092019_UPDATE_COLUMN_PHF_HUONGDAN_CHIDAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_HUONGDAN_CHIDAO", "MA_VANBAN", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_HUONGDAN_CHIDAO", "NAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_HUONGDAN_CHIDAO", "NAM");
            DropColumn("BTSTC.PHF_HUONGDAN_CHIDAO", "MA_VANBAN");
        }
    }
}
