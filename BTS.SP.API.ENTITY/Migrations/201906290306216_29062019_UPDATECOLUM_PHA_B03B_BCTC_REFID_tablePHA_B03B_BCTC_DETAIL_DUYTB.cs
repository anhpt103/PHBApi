namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29062019_UPDATECOLUM_PHA_B03B_BCTC_REFID_tablePHA_B03B_BCTC_DETAIL_DUYTB : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "BTSTC.PHA_B03B_BCTC_DETAIL", name: "PHB_B03B_BCTC_REFID", newName: "PHA_B03B_BCTC_REFID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "BTSTC.PHA_B03B_BCTC_DETAIL", name: "PHA_B03B_BCTC_REFID", newName: "PHB_B03B_BCTC_REFID");
        }
    }
}
