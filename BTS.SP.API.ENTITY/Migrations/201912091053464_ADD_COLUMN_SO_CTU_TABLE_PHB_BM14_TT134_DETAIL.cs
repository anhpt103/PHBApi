namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_COLUMN_SO_CTU_TABLE_PHB_BM14_TT134_DETAIL : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_BM14_TT134_DETAIL", "SO_CTU", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_BM14_TT134_DETAIL", "SO_CTU");
        }
    }
}
