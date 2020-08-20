namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09122019_UPDATE_TABLE_PHB_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_L_PC_UB_DETAIL", "MA_BAOCAO_CHA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHB_L_PC_UB", "MA_BAOCAO_CHA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHB_DM_CANBO", "HE_SO_LUONG", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_CANBO", "GIAM_TRU", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_CANBO", "DTCQ", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_CANBO", "DTNR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_CANBO", "DTDD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "MUC_LUONG_TT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "GIAM_TRU", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHXH_CQD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHYT_CQD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHTN_CQD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "KP_CD_CQD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHXH_NLDD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHYT_NLDD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHTN_NLDD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "KP_CD_NLDD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "HE_SOLUONG", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_KV", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_CHUCVU", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_THAMNIEN", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_TRACHNHIEM", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_CONGVU", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_LOAIXA", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_LAUNAM", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_THUHUT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "CKPT_BHXH", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "CKPT_BHYT", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "CKPT_BHYT", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "CKPT_BHXH", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_THUHUT", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_LAUNAM", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_LOAIXA", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_CONGVU", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_TRACHNHIEM", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_THAMNIEN", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_CHUCVU", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PC_KV", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "HE_SOLUONG", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "KP_CD_NLDD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHTN_NLDD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHYT_NLDD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHXH_NLDD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "KP_CD_CQD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHTN_CQD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHYT_CQD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "BHXH_CQD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "GIAM_TRU", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_TIENLUONG", "MUC_LUONG_TT", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_CANBO", "DTDD", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_CANBO", "DTNR", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_CANBO", "DTCQ", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_CANBO", "GIAM_TRU", c => c.Double(nullable: false));
            AlterColumn("BTSTC.PHB_DM_CANBO", "HE_SO_LUONG", c => c.Double(nullable: false));
            DropColumn("BTSTC.PHB_L_PC_UB", "MA_BAOCAO_CHA");
            DropColumn("BTSTC.PHB_L_PC_UB_DETAIL", "MA_BAOCAO_CHA");
        }
    }
}
