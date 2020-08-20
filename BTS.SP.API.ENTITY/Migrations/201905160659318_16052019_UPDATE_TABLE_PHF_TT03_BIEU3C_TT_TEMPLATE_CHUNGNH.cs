namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16052019_UPDATE_TABLE_PHF_TT03_BIEU3C_TT_TEMPLATE_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "SAPXEP", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "DONVI", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "NOIDUNG", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "GHICHU", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "TEN_DOITUONG");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "TEN_DOITUONG", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "GHICHU");
            DropColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "NOIDUNG");
            DropColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "DONVI");
            DropColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "SAPXEP");
        }
    }
}
