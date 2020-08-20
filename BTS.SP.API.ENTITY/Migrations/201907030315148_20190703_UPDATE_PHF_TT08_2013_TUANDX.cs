namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190703_UPDATE_PHF_TT08_2013_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT08_2013", "QUY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT08_2013", "TENQUY", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT08_2013", "TENQUY");
            DropColumn("BTSTC.PHF_TT08_2013", "QUY");
        }
    }
}
