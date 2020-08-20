namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10012018_EDIT_PHF_PL01_XLKN_VUDQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_PL01_XLKN", "NAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_PL01_XLKN", "PHONG_THANHTRA", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_PL01_XLKN", "DOT_BAOCAO", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_PL01_XLKN", "DOT_BAOCAO");
            DropColumn("BTSTC.PHF_PL01_XLKN", "PHONG_THANHTRA");
            DropColumn("BTSTC.PHF_PL01_XLKN", "NAM");
        }
    }
}
