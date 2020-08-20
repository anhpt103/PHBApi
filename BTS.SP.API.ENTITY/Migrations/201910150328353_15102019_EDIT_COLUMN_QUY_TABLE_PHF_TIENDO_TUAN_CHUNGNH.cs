namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15102019_EDIT_COLUMN_QUY_TABLE_PHF_TIENDO_TUAN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TIENDO_TUAN", "QUY", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TIENDO_TUAN", "QUY", c => c.String(maxLength: 50));
        }
    }
}
