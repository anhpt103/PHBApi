namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16072019_UPDATE_TABLE_PHF_KHAOSATKEKHAI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "LINHVUC", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "NGAY_LAPPHIEU", c => c.DateTime());
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "URL", c => c.String(maxLength: 250));
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "MALINHVUC");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_KHAOSATKEKHAI", "MALINHVUC", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "URL");
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "NGAY_LAPPHIEU");
            DropColumn("BTSTC.PHF_KHAOSATKEKHAI", "LINHVUC");
        }
    }
}
