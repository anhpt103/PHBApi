namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02042019_EditQDTT_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "SO_DUTHAO_CHA", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "SO_DUTHAO_CHA");
        }
    }
}
