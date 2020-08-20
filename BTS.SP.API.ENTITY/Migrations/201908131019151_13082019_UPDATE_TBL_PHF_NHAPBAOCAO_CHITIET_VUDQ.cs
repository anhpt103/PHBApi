namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13082019_UPDATE_TBL_PHF_NHAPBAOCAO_CHITIET_VUDQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "SOTHUTU_HIENTHI", c => c.String());
            AddColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "DINH_DANG", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOI_DUNG_STRING", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOI_DUNG_INT", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOI_DUNG_INT");
            DropColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOI_DUNG_STRING");
            DropColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "DINH_DANG");
            DropColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "SOTHUTU_HIENTHI");
        }
    }
}
