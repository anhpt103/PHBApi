namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14012018_ADD_TBL_PL01_XLKN_VUDQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_PL01_XLKN_CT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO = c.String(maxLength: 100),
                        STT = c.String(maxLength: 50),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.String(maxLength: 50),
                        TEN_DOITUONG = c.String(maxLength: 500),
                        TONG_CUOC_THANHTRA = c.String(maxLength: 500),
                        TONG_DOAN_THANHTRA = c.String(maxLength: 500),
                        TD_DANGTHUCHIEN = c.String(maxLength: 500),
                        TD_DANGDUTHAO_KL = c.String(maxLength: 500),
                        TD_DACONGBODUTHAOKL = c.String(maxLength: 500),
                        TD_DANGTRINHLANHDAOBO = c.String(maxLength: 500),
                        DALUHANH_KL = c.String(maxLength: 500),
                        GHI_CHU = c.String(maxLength: 500),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_PL01_XLKN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO = c.String(maxLength: 100),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PHONG_THANHTRA = c.String(maxLength: 500),
                        DOT_BAOCAO = c.String(maxLength: 500),
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
            DropTable("BTSTC.PHF_PL01_XLKN");
            DropTable("BTSTC.PHF_PL01_XLKN_CT");
        }
    }
}
