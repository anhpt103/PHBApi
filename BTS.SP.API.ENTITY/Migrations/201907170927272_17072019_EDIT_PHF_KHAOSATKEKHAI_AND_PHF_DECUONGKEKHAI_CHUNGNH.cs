namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17072019_EDIT_PHF_KHAOSATKEKHAI_AND_PHF_DECUONGKEKHAI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            DropColumn("BTSTC.PHF_DECUONGKEKHAI", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_DECUONGKEKHAI", "TEN_FILE");
            DropColumn("BTSTC.PHF_DECUONGKEKHAI", "STT");
            DropColumn("BTSTC.PHF_DECUONGKEKHAI", "NGAY_LAPPHIEU");
            DropColumn("BTSTC.PHF_DECUONGKEKHAI", "THOIGIAN");
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "TEN_FILE");
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "STT");
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "NGAY_LAPPHIEU");
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "THOIGIAN");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "THOIGIAN", c => c.String(maxLength: 30));
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "NGAY_LAPPHIEU", c => c.DateTime());
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "TEN_FILE", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_DECUONGKEKHAI", "THOIGIAN", c => c.String(maxLength: 30));
            AddColumn("BTSTC.PHF_DECUONGKEKHAI", "NGAY_LAPPHIEU", c => c.DateTime());
            AddColumn("BTSTC.PHF_DECUONGKEKHAI", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_DECUONGKEKHAI", "TEN_FILE", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_DECUONGKEKHAI", "MA_FILE_PK", c => c.String(maxLength: 200));
        }
    }
}
