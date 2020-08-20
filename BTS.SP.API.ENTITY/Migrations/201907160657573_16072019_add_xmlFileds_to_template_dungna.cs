namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16072019_add_xmlFileds_to_template_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_B01_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
            AddColumn("BTSTC.PHA_B02_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
            AddColumn("BTSTC.PHA_B03B_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME_1", c => c.String());
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME_2", c => c.String());
            AddColumn("BTSTC.PHA_B01_BSTT_1_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
            AddColumn("BTSTC.PHB_B01_BSTT_2_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
            AddColumn("BTSTC.PHB_B03A_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_B03A_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME");
            DropColumn("BTSTC.PHB_B01_BSTT_2_TEMPLATE", "XML_PARENT_FIELD_NAME");
            DropColumn("BTSTC.PHA_B01_BSTT_1_TEMPLATE", "XML_PARENT_FIELD_NAME");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME_2");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME_1");
            DropColumn("BTSTC.PHA_B03B_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME");
            DropColumn("BTSTC.PHA_B02_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME");
            DropColumn("BTSTC.PHA_B01_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME");
        }
    }
}
