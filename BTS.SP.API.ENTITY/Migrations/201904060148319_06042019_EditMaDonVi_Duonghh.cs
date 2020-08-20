namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06042019_EditMaDonVi_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_FILE_QTTTG", "MA_DONVI", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_BM_FILE_QTTTG", "TEN_DONVI");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_BM_FILE_QTTTG", "TEN_DONVI", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_BM_FILE_QTTTG", "MA_DONVI");
        }
    }
}
