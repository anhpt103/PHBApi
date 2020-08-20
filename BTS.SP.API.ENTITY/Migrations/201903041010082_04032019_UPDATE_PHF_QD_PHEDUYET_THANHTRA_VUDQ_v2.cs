namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04032019_UPDATE_PHF_QD_PHEDUYET_THANHTRA_VUDQ_v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "SO_QUYETDINH", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "SO_QUYETDINH", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
