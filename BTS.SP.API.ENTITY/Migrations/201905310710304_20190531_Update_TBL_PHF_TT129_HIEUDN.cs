namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190531_Update_TBL_PHF_TT129_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT129", "QUY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT129", "TENQUY", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT129", "TENQUY");
            DropColumn("BTSTC.PHF_TT129", "QUY");
        }
    }
}
