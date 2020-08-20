namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25042019_UpdateKienNghi_TamGiu_3_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "SO_KETLUAN_THANHTRA", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "NGAY_KETLUAN_THANHTRA", c => c.DateTime());
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "GHITHU_GHICHI_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "GIAM_DUTOAN_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "GIAM_QUYETTOAN_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "THUVE_COPHAN_HOA_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "KIENNGHI_KHAC_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "TONGSO_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "TONGSO_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "KIENNGHI_KHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "THUVE_COPHAN_HOA_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "GIAM_QUYETTOAN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "GIAM_DUTOAN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "GHITHU_GHICHI_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "NGAY_KETLUAN_THANHTRA");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "SO_KETLUAN_THANHTRA");
        }
    }
}
