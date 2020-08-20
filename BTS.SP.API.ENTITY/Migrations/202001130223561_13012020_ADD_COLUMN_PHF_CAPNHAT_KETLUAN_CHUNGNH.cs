namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13012020_ADD_COLUMN_PHF_CAPNHAT_KETLUAN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_CAPNHAT_KETLUAN", "PHONG_TT", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_CAPNHAT_KETLUAN", "TRUONG_DOAN", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_CAPNHAT_KETLUAN", "TRUONG_DOAN", c => c.String(maxLength: 100));
            DropColumn("BTSTC.PHF_CAPNHAT_KETLUAN", "PHONG_TT");
        }
    }
}
