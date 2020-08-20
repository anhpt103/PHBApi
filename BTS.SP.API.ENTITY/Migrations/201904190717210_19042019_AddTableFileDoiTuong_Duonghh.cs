namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19042019_AddTableFileDoiTuong_Duonghh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_DS_FILE_DOITUONG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_DOITUONG = c.String(maxLength: 50),
                        TEN_NGHIEPVU = c.String(maxLength: 500),
                        FILE_DINHKEM = c.String(maxLength: 1000),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHF_DS_FILE_DOITUONG");
        }
    }
}
