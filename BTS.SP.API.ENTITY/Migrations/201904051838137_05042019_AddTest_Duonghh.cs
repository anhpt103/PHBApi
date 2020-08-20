namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05042019_AddTest_Duonghh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_DM_TEST",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_TEST = c.String(nullable: false, maxLength: 50),
                        TEN_TEST = c.String(nullable: false, maxLength: 256),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHF_DM_TEST");
        }
    }
}
