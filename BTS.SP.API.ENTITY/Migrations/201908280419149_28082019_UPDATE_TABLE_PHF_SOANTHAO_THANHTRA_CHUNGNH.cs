namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28082019_UPDATE_TABLE_PHF_SOANTHAO_THANHTRA_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "TUNGAY", c => c.DateTime(nullable: false));
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "DENNGAY", c => c.DateTime(nullable: false));
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "QUY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "TENQUY", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "MA_DOITUONG_TT");
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "DOT_THANHTRA");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "DOT_THANHTRA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "MA_DOITUONG_TT", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "TENQUY");
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "QUY");
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "DENNGAY");
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "TUNGAY");
        }
    }
}
