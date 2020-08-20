namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19112019_editLen_kiennt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.BC_NHAPDT_XA", "MA_PK", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_PK", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_PK", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("BTSTC.BC_NHAPDT_XA", "MA_PK", c => c.String(maxLength: 10));
        }
    }
}
