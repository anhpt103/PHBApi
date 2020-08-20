namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24072019_add_field_to_pha_b04_bctc_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "MA_SO_IMPORT_XML_BCTC_107_GT", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "MA_SO_IMPORT_XML_BCTC_107_GT");

        }
    }
}
