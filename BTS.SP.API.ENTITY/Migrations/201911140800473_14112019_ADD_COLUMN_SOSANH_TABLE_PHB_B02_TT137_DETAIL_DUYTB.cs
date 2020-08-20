namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14112019_ADD_COLUMN_SOSANH_TABLE_PHB_B02_TT137_DETAIL_DUYTB : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_B02_TT137_DETAIL", "SOSANH", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_B02_TT137_DETAIL", "SOSANH");
        }
    }
}
