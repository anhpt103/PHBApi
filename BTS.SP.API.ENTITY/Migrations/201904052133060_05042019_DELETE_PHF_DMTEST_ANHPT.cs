namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05042019_DELETE_PHF_DMTEST_ANHPT : DbMigration
    {
        public override void Up()
        {
          
            DropTable("BTSTC.PHF_DM_TEST");
        }
        
        public override void Down()
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
    }
}
