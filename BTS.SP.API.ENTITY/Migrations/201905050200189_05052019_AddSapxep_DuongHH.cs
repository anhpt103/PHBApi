namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05052019_AddSapxep_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU3A", "SAPXEP", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT03_BIEU3A", "SAPXEP");
        }
    }
}
