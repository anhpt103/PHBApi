namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05042019duyaddcolumdmnguon : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.DM_NGUON_DIA_PHUONG", "LOAI_NGUON", c => c.String(maxLength: 50));
            AddColumn("BTSTC.DM_NGUON_DIA_PHUONG", "TRANG_THAI", c => c.String(maxLength: 50));
            AddColumn("BTSTC.DM_NGUON_DIA_PHUONG", "MA_NGUON_CHA", c => c.String(maxLength: 4));
            AddColumn("BTSTC.DM_NGUON_DIA_PHUONG", "GHI_CHU", c => c.String(maxLength: 4));
            AddColumn("BTSTC.DM_NGUON_DIA_PHUONG", "USER_NAME", c => c.String(maxLength: 4));
            AddColumn("BTSTC.DM_NGUON_DIA_PHUONG", "NGAY_PS", c => c.DateTime());
            AddColumn("BTSTC.DM_NGUON_DIA_PHUONG", "NGAY_SD", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.DM_NGUON_DIA_PHUONG", "NGAY_SD");
            DropColumn("BTSTC.DM_NGUON_DIA_PHUONG", "NGAY_PS");
            DropColumn("BTSTC.DM_NGUON_DIA_PHUONG", "USER_NAME");
            DropColumn("BTSTC.DM_NGUON_DIA_PHUONG", "GHI_CHU");
            DropColumn("BTSTC.DM_NGUON_DIA_PHUONG", "MA_NGUON_CHA");
            DropColumn("BTSTC.DM_NGUON_DIA_PHUONG", "TRANG_THAI");
            DropColumn("BTSTC.DM_NGUON_DIA_PHUONG", "LOAI_NGUON");
        }
    }
}
