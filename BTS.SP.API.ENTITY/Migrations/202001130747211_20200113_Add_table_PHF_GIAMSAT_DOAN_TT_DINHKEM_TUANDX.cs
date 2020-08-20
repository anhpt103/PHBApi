namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200113_Add_table_PHF_GIAMSAT_DOAN_TT_DINHKEM_TUANDX : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_GIAMSAT_DOAN_TT_DINHKEM",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_DOITUONG = c.String(maxLength: 50),
                        MA_DONVI = c.String(maxLength: 50),
                        NAM_THANHTRA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FILE_PATH = c.String(maxLength: 500),
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
            DropTable("BTSTC.PHF_GIAMSAT_DOAN_TT_DINHKEM");
        }
    }
}
