namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08032019_INSERTCOLUMN__MABAOCAO_TABLE_PHF_TT03_BIEU1A_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU1A", "MABAOCAO", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT03_BIEU1A", "MABAOCAO");
        }
    }
}
