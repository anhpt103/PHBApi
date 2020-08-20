namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_BC_NHAPDT_XA_TUANDX : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.BC_NHAPDT_XA", "MA_PK", c => c.String(maxLength: 10));
            AlterColumn("BTSTC.BC_NHAPDT_XA", "NGUOI_TAO", c => c.String(maxLength: 150));
    
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.BC_NHAPDT_XA", "NGUOI_TAO", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("BTSTC.BC_NHAPDT_XA", "MA_PK", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
