namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09042019_EditTableDoiTuong_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_DOITUONG", "MA_DOITUONG_CHA", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_DOITUONG", "MA_DOITUONG_CHA");
        }
    }
}
