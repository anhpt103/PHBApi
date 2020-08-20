namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05092019_UPDATE_TABLE_PHF_NHAPBAOCAO_CHITIET_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "MADONG", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "TENDONG", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "TENDONG", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "MADONG", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
