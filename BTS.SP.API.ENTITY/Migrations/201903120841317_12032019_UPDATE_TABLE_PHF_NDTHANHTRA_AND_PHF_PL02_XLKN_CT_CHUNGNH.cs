namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12032019_UPDATE_TABLE_PHF_NDTHANHTRA_AND_PHF_PL02_XLKN_CT_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "DONVI_DUOC_THANHTRA", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "DONVI_DUOC_THANHTRA", c => c.String(maxLength: 100));
        }
    }
}
