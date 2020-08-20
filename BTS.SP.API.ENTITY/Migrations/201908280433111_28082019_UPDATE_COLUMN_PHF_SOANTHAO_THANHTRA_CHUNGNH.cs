namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28082019_UPDATE_COLUMN_PHF_SOANTHAO_THANHTRA_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "DINHKEMFILE", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "URL", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "URL");
            DropColumn("BTSTC.PHF_SOANTHAO_THANHTRA", "DINHKEMFILE");
        }
    }
}
