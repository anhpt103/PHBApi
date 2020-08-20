namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052019_AddCoQuanThu_Duonghh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.DM_COQUANTHU",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_COQUANTHU = c.String(maxLength: 50),
                        TEN_COQUANTHU = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.DM_COQUANTHU");
        }
    }
}
