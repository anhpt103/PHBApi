namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200117_Add_2Colunm_table_PHF_GIAMSAT_DOAN_TT_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAY_TRIENKHAI", c => c.DateTime());
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAY_KETTHUC", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAY_KETTHUC");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAY_TRIENKHAI");
        }
    }
}
