namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02122019_BO_REQUIRE_5TABLE : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_PBDT_B121", "SU_NGHIEP", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B122", "SU_NGHIEP", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B123", "SU_NGHIEP", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B124", "SU_NGHIEP", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B125", "SU_NGHIEP", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_PBDT_B125", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B124", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B123", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B122", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B121", "SU_NGHIEP", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
