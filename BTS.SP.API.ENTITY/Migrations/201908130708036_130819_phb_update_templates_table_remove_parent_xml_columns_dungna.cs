namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _130819_phb_update_templates_table_remove_parent_xml_columns_dungna : DbMigration
    {
        public override void Up()
        {
            DropColumn("BTSTC.PHA_B01_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME");
            DropColumn("BTSTC.PHA_B02_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME");
            DropColumn("BTSTC.PHA_B03B_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME");
            DropColumn("BTSTC.PHB_B03A_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHB_B03A_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
            AddColumn("BTSTC.PHA_B03B_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
            AddColumn("BTSTC.PHA_B02_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
            AddColumn("BTSTC.PHA_B01_BCTC_TEMPLATE", "XML_PARENT_FIELD_NAME", c => c.String());
        }
    }
}
