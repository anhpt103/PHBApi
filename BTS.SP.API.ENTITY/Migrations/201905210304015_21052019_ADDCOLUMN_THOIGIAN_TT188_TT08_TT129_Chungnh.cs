namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21052019_ADDCOLUMN_THOIGIAN_TT188_TT08_TT129_Chungnh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT08_2013", "THOIGIAN", c => c.String(maxLength: 30));
            AddColumn("BTSTC.PHF_TT129", "THOIGIAN", c => c.String(maxLength: 30));
            AddColumn("BTSTC.PHF_TT188", "THOIGIAN", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT188", "THOIGIAN");
            DropColumn("BTSTC.PHF_TT129", "THOIGIAN");
            DropColumn("BTSTC.PHF_TT08_2013", "THOIGIAN");
        }
    }
}
