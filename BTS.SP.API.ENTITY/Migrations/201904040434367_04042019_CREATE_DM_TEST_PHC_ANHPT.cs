namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04042019_CREATE_DM_TEST_PHC_ANHPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.DM_TEST_DANHMUC",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA = c.String(maxLength: 20),
                        TEN = c.String(maxLength: 500),
                        TRANG_THAI = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.DM_TEST_DANHMUC");
        }
    }
}
