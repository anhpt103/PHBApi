namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11032019_UPDATE_PHF_XD_KH_THANHTRA_BO_CHITIET_VUDQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LOAI_DOITONG", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LOAI_DOITONG");
        }
    }
}
