namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28032019_UpdateNDTT_GIMFILE_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NDTHANHTRA", "GIMFILE", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_NDTHANHTRA", "URLFILE", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NDTHANHTRA", "URLFILE");
            DropColumn("BTSTC.PHF_NDTHANHTRA", "GIMFILE");
        }
    }
}
