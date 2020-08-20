namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_COLUMN_NAM_TABLE_PHB_B02_TT137_DUYTB : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_B02_TT137", "NAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_B02_TT137", "NAM");
        }
    }
}
