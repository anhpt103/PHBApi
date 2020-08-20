namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15012019_DeleteCodePHF_BM_09TT_TCDN_ThanhND : DbMigration
    {
        public override void Up()
        {
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "STT");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "STT_TIEUDE");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "STT_CHA");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "MA_FILE");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "IS_BOLD");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "IS_ITALIC");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "TEN_DONVI");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "DTVTN_HACHTOANTHIEU");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "DTVTN_TANGKHONGDUNG");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "CP_HACHTOANTHIEU");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "CP_TANGKHONGDUNG");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "LNTH_HACHTOANTHIEU");
            DropColumn("BTSTC.PHF_BM_09TT_TCDN", "LNTH_TANGKHONGDUNG");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "LNTH_TANGKHONGDUNG", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "LNTH_HACHTOANTHIEU", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "CP_TANGKHONGDUNG", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "CP_HACHTOANTHIEU", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "DTVTN_TANGKHONGDUNG", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "DTVTN_HACHTOANTHIEU", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "TEN_DONVI", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "IS_ITALIC", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "IS_BOLD", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "MA_FILE", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "STT_TIEUDE", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHF_BM_09TT_TCDN", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
    }
}
