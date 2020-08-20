namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12022019_CapNhat_TheoDoiCanBo_DuongHH : DbMigration
    {
        public override void Up()
        {
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "CHUCVU");
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "GIOI_TINH");
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "NGAY_SINH");
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "PHONGBAN");
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "SO_MAY_LE");
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "SO_DI_DONG");
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "TEN_DOT_THANHTRA");
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "SO_QUYETDINH");
            DropColumn("BTSTC.PHF_THEODOI_CANBO_CT", "STT");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "STT", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "SO_QUYETDINH", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "TEN_DOT_THANHTRA", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "SO_DI_DONG", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "SO_MAY_LE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "PHONGBAN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "NGAY_SINH", c => c.DateTime());
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "GIOI_TINH", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_THEODOI_CANBO_CT", "CHUCVU", c => c.String(maxLength: 500));
        }
    }
}
