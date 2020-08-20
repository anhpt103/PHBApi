namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26062019_Add_col_PHA_THONGTRI_YDUTOAN_VUDQ_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TONG_TIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TONG_TIEN");
        }
    }
}
