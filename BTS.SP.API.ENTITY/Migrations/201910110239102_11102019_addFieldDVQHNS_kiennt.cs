namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11102019_addFieldDVQHNS_kiennt : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.SYS_DVQHNS", "NOI_MO_TK", c => c.String(maxLength: 120));
            AddColumn("BTSTC.SYS_DVQHNS", "SO_TK", c => c.String(maxLength: 120));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.SYS_DVQHNS", "SO_TK");
            DropColumn("BTSTC.SYS_DVQHNS", "NOI_MO_TK");
        }
    }
}
