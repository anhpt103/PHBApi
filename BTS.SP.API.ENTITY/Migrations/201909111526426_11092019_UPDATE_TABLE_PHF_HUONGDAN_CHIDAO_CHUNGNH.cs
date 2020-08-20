namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11092019_UPDATE_TABLE_PHF_HUONGDAN_CHIDAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_HUONGDAN_CHIDAO", "URL", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_HUONGDAN_CHIDAO", "URL");
        }
    }
}
