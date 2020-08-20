namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20082019_EDIT_PHF_NHAPBAOCAO_COLUMN_NGUOIKY_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOIKY", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOIKY", c => c.DateTime(nullable: false));
        }
    }
}
