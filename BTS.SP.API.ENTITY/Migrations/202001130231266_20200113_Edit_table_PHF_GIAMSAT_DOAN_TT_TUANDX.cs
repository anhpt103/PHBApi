namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200113_Edit_table_PHF_GIAMSAT_DOAN_TT_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "TENQD", c => c.String(maxLength: 250));
            AlterColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NAM", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "TENQD");
        }
    }
}
