namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11022019_UPDATE_PHF_KIENNGHI_TAMGIU_VUDQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "LOAI_KIEN_NGHI", c => c.String(maxLength: 70));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "ND_KIEN_NGHI", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "ND_THUC_HIEN", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "ND_THUC_HIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "ND_KIEN_NGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "LOAI_KIEN_NGHI");
        }
    }
}
