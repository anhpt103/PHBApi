namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190805_ADD_TABLE_PHF_BC_DINHKY_TEMPLATE_TUANDX : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BC_DINHKY_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 50),
                        TIEU_CHI = c.String(maxLength: 200),
                        NOI_DUNG = c.String(maxLength: 300),
                        DINH_DANG = c.String(maxLength: 500),
                        TUNGAY = c.DateTime(nullable: false),
                        DENNGAY = c.DateTime(nullable: false),
                        NAM = c.String(maxLength: 50),
                        QUY = c.String(maxLength: 50),
                        TENQUY = c.String(maxLength: 50),
                        MAPHONGBAN = c.String(maxLength: 50),
                        THOIGIAN = c.String(maxLength: 30),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHF_BC_DINHKY_TEMPLATE");
        }
    }
}
