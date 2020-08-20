namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TBL_PHF_PL05_DANHSACHDONVI_08042019_VUDQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_PL05_DANHSACHDONVI",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        DONVI = c.String(maxLength: 500),
                        NGANHNGHE_KD = c.String(maxLength: 500),
                        VON_DL = c.Decimal(precision: 10, scale: 0),
                        DIA_CHI = c.String(maxLength: 500),
                        TONG_TAISAN = c.Decimal(precision: 18, scale: 2),
                        VON_CHUSOHUU = c.Decimal(precision: 18, scale: 2),
                        TONG_DT_TN = c.Decimal(precision: 18, scale: 2),
                        TONG_CHIPHI = c.Decimal(precision: 18, scale: 2),
                        TONG_LOINHUAN_TT = c.Decimal(precision: 18, scale: 2),
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
            DropTable("BTSTC.PHF_PL05_DANHSACHDONVI");
        }
    }
}
