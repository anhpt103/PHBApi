namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10072019_UPDATE_TABLE_NHAPBAOCAO_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NOIDUNG", c => c.String(maxLength: 1500));
            AlterColumn("BTSTC.PHF_DM_BAOCAO_COT", "TENCOT", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "TENDONG", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "TENDONG", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHF_DM_BAOCAO_COT", "TENCOT", c => c.String(nullable: false, maxLength: 200));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NOIDUNG");
        }
    }
}
