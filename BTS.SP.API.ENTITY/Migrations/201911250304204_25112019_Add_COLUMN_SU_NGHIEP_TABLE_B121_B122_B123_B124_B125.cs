namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25112019_Add_COLUMN_SU_NGHIEP_TABLE_B121_B122_B123_B124_B125 : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_B121", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
            AddColumn("BTSTC.PHB_PBDT_B122", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
            AddColumn("BTSTC.PHB_PBDT_B123", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
            AddColumn("BTSTC.PHB_PBDT_B124", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
            AddColumn("BTSTC.PHB_PBDT_B125", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_PBDT_B125", "SU_NGHIEP");
            DropColumn("BTSTC.PHB_PBDT_B124", "SU_NGHIEP");
            DropColumn("BTSTC.PHB_PBDT_B123", "SU_NGHIEP");
            DropColumn("BTSTC.PHB_PBDT_B122", "SU_NGHIEP");
            DropColumn("BTSTC.PHB_PBDT_B121", "SU_NGHIEP");
        }
    }
}
