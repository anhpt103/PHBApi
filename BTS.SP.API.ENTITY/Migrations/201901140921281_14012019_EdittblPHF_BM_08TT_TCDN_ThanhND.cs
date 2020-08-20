namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14012019_EdittblPHF_BM_08TT_TCDN_ThanhND : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TONGNO_PHAITHU", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TREN_6THANG", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TREN_1NAM", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TU_2NAM", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TREN_3NAM", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TONGCONG", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TRICHLAP_DUPHONG", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "XULY_NO", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "NOIDUNG");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "SOTIEN_VAY");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "THOIHAN_TRA");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "NTN_KHAUHAO");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "NTN_LOINHUAN");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "NTN_KHAC");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TNTN_KHAUHAO");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TNTN_LOINHUAN");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TNTN_KHAC");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "CHENHLECH");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "CHENHLECH", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TNTN_KHAC", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TNTN_LOINHUAN", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "TNTN_KHAUHAO", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "NTN_KHAC", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "NTN_LOINHUAN", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "NTN_KHAUHAO", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "THOIHAN_TRA", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "SOTIEN_VAY", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "NOIDUNG", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "XULY_NO");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TRICHLAP_DUPHONG");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TONGCONG");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TREN_3NAM");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TU_2NAM");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TREN_1NAM");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TREN_6THANG");
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "TONGNO_PHAITHU");
        }
    }
}
