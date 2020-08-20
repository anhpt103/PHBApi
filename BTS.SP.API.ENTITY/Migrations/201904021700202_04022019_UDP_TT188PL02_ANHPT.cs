namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04022019_UDP_TT188PL02_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_CREATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_CREATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_UPDATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_UPDATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_STATE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "UNITCODE", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "UNITCODE");
            DropColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_STATE");
            DropColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_UPDATE_BY");
            DropColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_UPDATE_DATE");
            DropColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_CREATE_BY");
            DropColumn("BTSTC.PHF_TT188_PL02_TEMPLATE", "I_CREATE_DATE");
        }
    }
}
