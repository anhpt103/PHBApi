namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01072019_CREATE_PHF_DM_BAOCAO_ANHPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_DM_BAOCAO_COT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(nullable: false, maxLength: 50),
                        MACOT = c.String(nullable: false, maxLength: 50),
                        TENCOT = c.String(nullable: false, maxLength: 200),
                        DODAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SOTHUTU = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_DM_BAOCAO_DONG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(nullable: false, maxLength: 50),
                        MADONG = c.String(nullable: false, maxLength: 50),
                        TENDONG = c.String(nullable: false, maxLength: 500),
                        SOTHUTU = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_DM_BAOCAO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(nullable: false, maxLength: 50),
                        TENBAOCAO = c.String(nullable: false, maxLength: 250),
                        MOTA = c.String(maxLength: 500),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRANGTHAI = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHF_DM_BAOCAO");
            DropTable("BTSTC.PHF_DM_BAOCAO_DONG");
            DropTable("BTSTC.PHF_DM_BAOCAO_COT");
        }
    }
}
