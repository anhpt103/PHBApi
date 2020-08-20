namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _050819_add_phb_sys_log_chucnang_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_SYS_LOG_CHUCNANG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        DIACHIMAY = c.String(maxLength: 100),
                        THOIGIANTRUYCAP = c.DateTime(nullable: false),
                        CHUCNANG = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "CHUYEN_VIEN", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TRUONG_PHONG", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TRUONG_PHONG_NS", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "GIAM_DOC", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "CHUONG", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "KHOAN", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TIEU_MUC", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TIEU_MUC");
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "KHOAN");
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "CHUONG");
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "GIAM_DOC");
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TRUONG_PHONG_NS");
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TRUONG_PHONG");
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "CHUYEN_VIEN");
            DropTable("BTSTC.PHB_SYS_LOG_CHUCNANG");
        }
    }
}
