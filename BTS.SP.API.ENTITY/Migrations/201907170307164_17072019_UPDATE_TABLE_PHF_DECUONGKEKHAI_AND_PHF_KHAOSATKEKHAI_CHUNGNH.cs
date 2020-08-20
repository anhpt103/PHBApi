namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17072019_UPDATE_TABLE_PHF_DECUONGKEKHAI_AND_PHF_KHAOSATKEKHAI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DECUONGKEKHAI", "NOIDUNG", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "NOIDUNG", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "NOIDUNG");
            DropColumn("BTSTC.PHF_DECUONGKEKHAI", "NOIDUNG");
        }
    }
}
