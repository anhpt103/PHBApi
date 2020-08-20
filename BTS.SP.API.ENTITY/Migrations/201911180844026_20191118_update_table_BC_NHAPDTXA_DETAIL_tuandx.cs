namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20191118_update_table_BC_NHAPDTXA_DETAIL_tuandx : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_DBHC", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_DBHC");
        }
    }
}
