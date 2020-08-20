namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13022020_UPDATE_DB_PHF_KIENNGHI_KHONGSO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_KHONGSO", "MA_DOITUONG_CHA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_KIENNGHI_KHONGSO", "SO_KETLUAN_THANHTRA", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_KIENNGHI_KHONGSO", "NGAY_KETLUAN_THANHTRA", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KIENNGHI_KHONGSO", "NGAY_KETLUAN_THANHTRA");
            DropColumn("BTSTC.PHF_KIENNGHI_KHONGSO", "SO_KETLUAN_THANHTRA");
            DropColumn("BTSTC.PHF_KIENNGHI_KHONGSO", "MA_DOITUONG_CHA");
        }
    }
}
