namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28082019_EDITOR_COLUMN_NAM_PHF_SOANTHAO_THANHTRA_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "NAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "NAM_THANHTRA");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "NAM_THANHTRA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "NAM");
        }
    }
}
