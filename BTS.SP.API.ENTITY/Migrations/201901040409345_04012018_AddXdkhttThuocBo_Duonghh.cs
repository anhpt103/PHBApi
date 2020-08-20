namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04012018_AddXdkhttThuocBo_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "MA_DOITUONG", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "MA_DOITUONG_CHA", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "MA_DOITUONG_CHA");
            DropColumn("BTSTC.PHF_XD_KH_TT_THUOC_BO_CT", "MA_DOITUONG");
        }
    }
}
