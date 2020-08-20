namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03102019_add_table_of_phb_c_b04x_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_C_B04X_DETAIL_TSCD",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_C_B04X_REFID = c.String(nullable: false, maxLength: 50),
                        CHI_TIEU = c.String(nullable: false, maxLength: 50),
                        CHI_TIEU_OLD = c.String(maxLength: 50),
                        DON_VI_TINH = c.String(maxLength: 50),
                        DAUNAM_SL = c.Decimal(precision: 10, scale: 0),
                        DAUNAM_NG = c.Decimal(precision: 18, scale: 2),
                        TANG_SL = c.Decimal(precision: 10, scale: 0),
                        TANG_NG = c.Decimal(precision: 18, scale: 2),
                        GIAM_SL = c.Decimal(precision: 10, scale: 0),
                        GIAM_NG = c.Decimal(precision: 18, scale: 2),
                        CUOINAM_SL = c.Decimal(precision: 10, scale: 0),
                        CUOINAM_NG = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("BTSTC.PHB_C_B02AX_Template");
        }
        
        public override void Down()
        {
            
            DropTable("BTSTC.PHB_C_B04X_DETAIL_TSCD");
        }
    }
}
