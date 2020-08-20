namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17072019_EditTienDoTuan_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "KETQUA", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "KHOKHAN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "NOI_LAMVIEC", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "NOI_LAMVIEC");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "KHOKHAN");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "KETQUA");
        }
    }
}
