namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190710_UPDATE_TABLE_PHF_DINHKEMFILE1_2_3_HIEUDN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_DINHKEMFILE3",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TENBAOCAO = c.String(maxLength: 500),
                        DINHKEMFILE = c.String(maxLength: 200),
                        URL = c.String(maxLength: 250),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("BTSTC.PHF_DINHKEMFILE2", "THONGTU");
            DropColumn("BTSTC.PHF_DINHKEMFILE", "THONGTU");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_DINHKEMFILE", "THONGTU", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_DINHKEMFILE2", "THONGTU", c => c.String(maxLength: 200));
            DropTable("BTSTC.PHF_DINHKEMFILE3");
        }
    }
}
