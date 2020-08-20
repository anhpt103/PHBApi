namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23062019_ADDTABLE_DM_MALENHNV_DUYTB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.DM_MALENHNV",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_LNV = c.String(maxLength: 100),
                        TEN_LNV = c.String(maxLength: 100),
                        GHI_CHU = c.String(maxLength: 375),
                        TRANG_THAI = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.DM_MALENHNV");
        }
    }
}
