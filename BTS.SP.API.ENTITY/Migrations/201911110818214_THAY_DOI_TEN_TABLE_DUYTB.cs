namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class THAY_DOI_TEN_TABLE_DUYTB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "BTSTC.PHB_B02_TT137_REFID", newName: "PHB_B02_TT137_DETAIL");
        }
        
        public override void Down()
        {
            RenameTable(name: "BTSTC.PHB_B02_TT137_DETAIL", newName: "PHB_B02_TT137_REFID");
        }
    }
}
