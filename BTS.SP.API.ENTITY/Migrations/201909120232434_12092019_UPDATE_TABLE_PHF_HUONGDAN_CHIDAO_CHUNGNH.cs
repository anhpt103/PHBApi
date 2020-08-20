namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12092019_UPDATE_TABLE_PHF_HUONGDAN_CHIDAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_HUONGDAN_CHIDAO", "NGAY_HIEULUC", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_HUONGDAN_CHIDAO", "NGAY_HIEULUC", c => c.DateTime(nullable: false));
        }
    }
}
