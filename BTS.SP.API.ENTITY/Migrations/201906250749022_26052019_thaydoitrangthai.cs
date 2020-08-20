namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26052019_thaydoitrangthai : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.DM_MALENHNV", "TRANG_THAI", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.DM_MALENHNV", "TRANG_THAI", c => c.Decimal(precision: 10, scale: 0));
        }
    }
}
