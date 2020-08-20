namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200104_Update_Table_PHF_BM_FILE_QTTTG_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_FILE_QTTTG", "MA_DOITUONG", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_BM_FILE_QTTTG", "MA_DOITUONG");
        }
    }
}
