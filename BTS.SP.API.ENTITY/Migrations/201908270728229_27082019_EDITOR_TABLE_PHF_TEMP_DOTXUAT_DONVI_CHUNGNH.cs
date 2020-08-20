namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_EDITOR_TABLE_PHF_TEMP_DOTXUAT_DONVI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_CHUONG", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_LOAI", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_KHOAN", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_MUC", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_TIEUMUC", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "MA_CAP", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_CAP", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "NGAY_KET_SO", c => c.DateTime());
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "NGAY_HIEU_LUC", c => c.DateTime());
            AlterColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_DVQHNS", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_DVQHNS", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "NGAY_HIEU_LUC");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "NGAY_KET_SO");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_CAP");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "MA_CAP");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_TIEUMUC");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_MUC");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_KHOAN");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_LOAI");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_CHUONG");
        }
    }
}
