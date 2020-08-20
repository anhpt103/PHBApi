namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190805_PHF_BM_FILE_QTTTG_ADDCOLUMN_URL_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_FILE_QTTTG", "URL", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_BM_FILE_QTTTG", "URL");
        }
    }
}
