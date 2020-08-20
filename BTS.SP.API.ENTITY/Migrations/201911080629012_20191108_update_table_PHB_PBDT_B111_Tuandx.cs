namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20191108_update_table_PHB_PBDT_B111_Tuandx : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_B111", "FILEDINHKEM", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.PHB_PBDT_B111", "URL", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_PBDT_B111", "URL");
            DropColumn("BTSTC.PHB_PBDT_B111", "FILEDINHKEM");
        }
    }
}
