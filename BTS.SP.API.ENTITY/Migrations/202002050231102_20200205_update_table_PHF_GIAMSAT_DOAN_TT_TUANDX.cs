namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200205_update_table_PHF_GIAMSAT_DOAN_TT_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "FILEDINHKEM_2", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "URL_2", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "FILEDINHKEM_3", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "URL_3", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "URL_3");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "FILEDINHKEM_3");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "URL_2");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "FILEDINHKEM_2");
        }
    }
}
