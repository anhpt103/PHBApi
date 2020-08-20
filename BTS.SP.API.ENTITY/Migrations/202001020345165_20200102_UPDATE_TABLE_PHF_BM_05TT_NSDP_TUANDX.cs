namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200102_UPDATE_TABLE_PHF_BM_05TT_NSDP_TUANDX : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_BM_05TT_NSDP", "QUYETDINH_NGAY", c => c.DateTime());
            AlterColumn("BTSTC.PHF_BM_05TT_NSDP", "THOIGIAN_KC_HT", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_BM_05TT_NSDP", "THOIGIAN_KC_HT", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_BM_05TT_NSDP", "QUYETDINH_NGAY", c => c.String(maxLength: 300));
        }
    }
}
