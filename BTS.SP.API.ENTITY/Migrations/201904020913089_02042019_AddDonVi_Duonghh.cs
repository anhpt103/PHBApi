namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02042019_AddDonVi_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "MA_DONVI", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "MA_DONVI");
        }
    }
}
