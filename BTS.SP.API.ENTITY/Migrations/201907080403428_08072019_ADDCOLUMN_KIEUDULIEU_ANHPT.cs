namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08072019_ADDCOLUMN_KIEUDULIEU_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_BAOCAO_COT", "KIEUDULIEU", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_BAOCAO_COT", "KIEUDULIEU");
        }
    }
}
