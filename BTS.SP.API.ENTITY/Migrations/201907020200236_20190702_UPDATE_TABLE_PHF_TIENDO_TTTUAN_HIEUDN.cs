namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190702_UPDATE_TABLE_PHF_TIENDO_TTTUAN_HIEUDN : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "BTSTC.PHF_TIENDO_TTTUAN", name: "MAPHONG", newName: "MAPHONGBAN");
        }
        
        public override void Down()
        {
            RenameColumn(table: "BTSTC.PHF_TIENDO_TTTUAN", name: "MAPHONGBAN", newName: "MAPHONG");
        }
    }
}
