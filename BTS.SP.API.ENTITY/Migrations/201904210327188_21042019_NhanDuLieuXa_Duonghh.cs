namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21042019_NhanDuLieuXa_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA_DETAIL", "DIENGIAI", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_NHANDULIEU_XA_DETAIL", "DIENGIAI");
        }
    }
}
