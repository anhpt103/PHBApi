namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17012020_ADD_TABLE_PHF_BAOCAO_HANGTHANG_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BAOCAO_HANGTHANG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO = c.String(nullable: false, maxLength: 50),
                        TEN_BAOCAO = c.String(maxLength: 50),
                        MA_PHONGBAN = c.String(maxLength: 50),
                        MA_DOITUONG = c.String(maxLength: 50),
                        MA_CANBO = c.String(maxLength: 50),
                        NAM = c.String(maxLength: 50),
                        TU_NGAY = c.DateTime(nullable: false),
                        DEN_NGAY = c.DateTime(nullable: false),
                        MA_QUY = c.String(maxLength: 50),
                        TEN_QUY = c.String(maxLength: 50),
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
            DropTable("BTSTC.PHF_BAOCAO_HANGTHANG");
        }
    }
}
