namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04032019_UPDATE_PHF_QD_PHEDUYET_THANHTRA_VUDQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "SO_DUTHAO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "NOIDUNG_DUTHAO", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "NGAY_DUTHAO", c => c.DateTime());
            AddColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "LOAI", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "LOAI");
            DropColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "NGAY_DUTHAO");
            DropColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "NOIDUNG_DUTHAO");
            DropColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "SO_DUTHAO");
        }
    }
}
