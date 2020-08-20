namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11122019_ADD_TABLE_PHB_L_PC_D_AND_DETAIL_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_L_PC_D_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO_CHA = c.String(maxLength: 50),
                        PHB_L_PC_UB_REFID = c.String(maxLength: 50),
                        LOAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        HO_VATEN = c.String(maxLength: 50),
                        CHUC_DANH = c.String(maxLength: 250),
                        HE_SOLUONG = c.Decimal(precision: 18, scale: 2),
                        PC_KV = c.Decimal(precision: 18, scale: 2),
                        PC_CHUCVU = c.Decimal(precision: 18, scale: 2),
                        PC_THAMNIEN = c.Decimal(precision: 18, scale: 2),
                        PC_TRACHNHIEM = c.Decimal(precision: 18, scale: 2),
                        PC_CONGVU = c.Decimal(precision: 18, scale: 2),
                        PC_LOAIXA = c.Decimal(precision: 18, scale: 2),
                        PC_LAUNAM = c.Decimal(precision: 18, scale: 2),
                        PC_THUHUT = c.Decimal(precision: 18, scale: 2),
                        CKPT_BHXH = c.Decimal(precision: 18, scale: 2),
                        CKPT_BHYT = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_L_PC_D",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO_CHA = c.String(maxLength: 50),
                        REFID = c.String(maxLength: 50),
                        MA_CHUONG = c.String(maxLength: 3),
                        MA_QHNS = c.String(maxLength: 10),
                        NAM_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        KY_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DBHC = c.String(maxLength: 5),
                        MA_DBHC_CHA = c.String(maxLength: 5),
                        TEN_QHNS = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_L_PC_D");
            DropTable("BTSTC.PHB_L_PC_D_DETAIL");
        }
    }
}
