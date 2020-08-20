namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTbl_SYS_DVQHNS_QUANLY_kiennt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.SYS_DVQHNS_QUANLY",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_DVQHNS = c.String(maxLength: 20),
                        TEN_DVQHNS = c.String(maxLength: 240),
                        MA_CHUONG = c.String(maxLength: 3),
                        MA_NGANHKT = c.String(maxLength: 10),
                        SU_NGHIEP = c.String(maxLength: 10),
                        TRANG_THAI = c.String(maxLength: 1),
                        NOI_MO_TK = c.String(maxLength: 120),
                        SO_TK = c.String(maxLength: 120),
                        MA_DBHC = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "CHI_DAN", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "CHI_DAN", c => c.String(maxLength: 250));
            DropTable("BTSTC.SYS_DVQHNS_QUANLY");
        }
    }
}
