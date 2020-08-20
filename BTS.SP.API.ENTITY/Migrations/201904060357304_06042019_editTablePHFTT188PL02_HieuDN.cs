namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06042019_editTablePHFTT188PL02_HieuDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TT188_PL02", "KETQUA_NAMTRUOC", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT188_PL02", "KEHOACH_NAM", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT188_PL02", "KETQUA_NAM", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT188_PL02", "DOICHIEU_NAMTRUOC", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT188_PL02", "DOICHIEU_KEHOACH", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TT188_PL02", "DOICHIEU_KEHOACH", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT188_PL02", "DOICHIEU_NAMTRUOC", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT188_PL02", "KETQUA_NAM", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT188_PL02", "KEHOACH_NAM", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT188_PL02", "KETQUA_NAMTRUOC", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
