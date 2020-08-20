namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03102019_alter_table_of_phb_c_b04x_detailTSCD_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_C_B04X_DETAIL_TSCD", "STT", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_C_B04X_DETAIL_TSCD", "STT");
        }
    }
}
