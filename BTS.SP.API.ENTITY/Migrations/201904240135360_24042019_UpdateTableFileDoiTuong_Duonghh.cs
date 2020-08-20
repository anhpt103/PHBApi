namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24042019_UpdateTableFileDoiTuong_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DS_FILE_DOITUONG", "SO_FILE", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DS_FILE_DOITUONG", "NGAY_XUAT_FILE", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DS_FILE_DOITUONG", "NGAY_XUAT_FILE");
            DropColumn("BTSTC.PHF_DS_FILE_DOITUONG", "SO_FILE");
        }
    }
}
