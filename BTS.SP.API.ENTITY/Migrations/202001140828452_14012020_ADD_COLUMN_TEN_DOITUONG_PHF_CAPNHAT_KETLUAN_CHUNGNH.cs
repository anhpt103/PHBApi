namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14012020_ADD_COLUMN_TEN_DOITUONG_PHF_CAPNHAT_KETLUAN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_CAPNHAT_KETLUAN", "TEN_DOITUONG", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_CAPNHAT_KETLUAN", "MA_KLKT", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_CAPNHAT_KETLUAN", "MA_KLKT", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_CAPNHAT_KETLUAN", "TEN_DOITUONG");
        }
    }
}
