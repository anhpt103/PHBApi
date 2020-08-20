namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200113_Add_table_PHF_GIAMSAT_DOAN_TT_CT_And_Edit_PHF_GIAMSAT_DOAN_TT_TUANDX : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_GIAMSAT_DOAN_TT_CT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAQD = c.String(maxLength: 50),
                        MA_PK = c.String(maxLength: 50),
                        FILEDINHKEM = c.String(maxLength: 500),
                        URL = c.String(maxLength: 250),
                        NGAYLAP = c.DateTime(),
                        NGAYQDGS = c.DateTime(),
                        THOIGIAN_CAPNHAT = c.String(maxLength: 30),
                        TOTRUONG = c.String(maxLength: 250),
                        THANHVIEN = c.String(maxLength: 500),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "MA_PK", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "FILEDINHKEM");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "URL");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAYLAP");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAYQDGS");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THOIGIAN_CAPNHAT");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "TOTRUONG");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THANHVIEN");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THANHVIEN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "TOTRUONG", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THOIGIAN_CAPNHAT", c => c.String(maxLength: 30));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAYQDGS", c => c.DateTime());
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAYLAP", c => c.DateTime());
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "URL", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "FILEDINHKEM", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "MA_PK");
            DropTable("BTSTC.PHF_GIAMSAT_DOAN_TT_CT");
        }
    }
}
