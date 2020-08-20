namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20191118_update_2_table_BC_NHAPDT_XA_DETAIL_tuandx : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.BC_NHAPDT_XA", "MA_PK", c => c.String(nullable: false, maxLength: 10));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_PK", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_PK");
            DropColumn("BTSTC.BC_NHAPDT_XA", "MA_PK");
        }
    }
}
