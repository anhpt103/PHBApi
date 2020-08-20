namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17102019_ADD_COLUMN_PHF_TIENDO_TUAN_CHITIET_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "TIENDO", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "CONGVIEC_TUANTOI", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "CONGVIEC_DUKIEN", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "CONGVIEC_DUKIEN");
            DropColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "CONGVIEC_TUANTOI");
            DropColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "TIENDO");
        }
    }
}
