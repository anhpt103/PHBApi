namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _050819_fix_phb_sys_log_chuc_nang_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_SYS_LOG_CHUCNANG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        DIACHIMAY = c.String(maxLength: 100),
                        THOIGIANTRUYCAP = c.DateTime(nullable: false),
                        CHUCNANG = c.String(maxLength: 500),
                        DBHC = c.String(maxLength: 100),
                        TEN_DBHC = c.String(maxLength: 500),
                        DBHC_CHA = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_SYS_LOG_CHUCNANG");
        }
    }
}
