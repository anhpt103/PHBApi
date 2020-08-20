namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20032019addTblNhanDLXa_kienv3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_BAOCAOPK", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "MA_BAOCAOPK");
        }
    }
}
