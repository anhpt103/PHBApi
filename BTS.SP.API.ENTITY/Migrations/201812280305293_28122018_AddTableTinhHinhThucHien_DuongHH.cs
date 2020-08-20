namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28122018_AddTableTinhHinhThucHien_DuongHH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TH_THUCHIEN_KEHOACH_TT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(),
                        TEN_DOITUONG = c.String(maxLength: 500),
                        TONGSO_CUOC_THANHTRA = c.String(maxLength: 500),
                        TONGSO_DOAN_THANHTRA = c.String(maxLength: 500),
                        TIENDO_DANG_THUCHIEN = c.String(maxLength: 500),
                        TIENDO_DANG_DUTHAO = c.String(maxLength: 500),
                        TIENDO_DA_CONGBO_DUTHAO = c.String(maxLength: 500),
                        TIENDO_DANGTRINH_LANHDAO = c.String(maxLength: 500),
                        TIENDO_DA_LUUHANH = c.String(maxLength: 500),
                        GHI_CHU = c.String(maxLength: 500),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "LOAI_THANHTRA", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "NHOM_THANHTRA", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "DOITUONG_THANHTRA", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "COQUAN_THANHTRA", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO", "DOT_THANHTRA", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "KEHOACH_THANHTRA");
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LOAI_THANHTRA");
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "NHOM_THANHTRA");
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "PHONG_THANHTRA");
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "DOITUONG_THANHTRA");
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LYDO_THANHTRA");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LYDO_THANHTRA", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "DOITUONG_THANHTRA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "PHONG_THANHTRA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "NHOM_THANHTRA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LOAI_THANHTRA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "KEHOACH_THANHTRA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO", "DOT_THANHTRA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "COQUAN_THANHTRA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "DOITUONG_THANHTRA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "NHOM_THANHTRA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "LOAI_THANHTRA", c => c.String(maxLength: 50));
            DropTable("BTSTC.PHF_TH_THUCHIEN_KEHOACH_TT");
        }
    }
}
