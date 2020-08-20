namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190423_UPDATE_PHF_BIEU05_TONGHOP_XUPHAT_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_BM_FILE_TTTCQuy", "NAM", c => c.String(maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_BM_FILE_TTTCQuy", "NAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
    }
}
