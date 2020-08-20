namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25072019_CREATE_AUTHORIZE_NHOMQUYEN_TWICE_PHF_ANHPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_AU_NHOMQUYEN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MANHOMQUYEN = c.String(nullable: false, maxLength: 50),
                        TENNHOMQUYEN = c.String(maxLength: 100),
                        MOTA = c.String(maxLength: 200),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("BTSTC.PHF_AU_NGUOIDUNG_QUYEN", "TRANG_THAI", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_AU_VAITRO_CHUCNANG", "TRANG_THAI", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_AU_VAITRO", "TRANG_THAI", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_AU_NGUOIDUNG_QUYEN", "TRANGTHAI");
            DropColumn("BTSTC.PHF_AU_VAITRO_CHUCNANG", "TRANGTHAI");
            DropColumn("BTSTC.PHF_AU_VAITRO", "TRANGTHAI");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_AU_VAITRO", "TRANGTHAI", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_AU_VAITRO_CHUCNANG", "TRANGTHAI", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_AU_NGUOIDUNG_QUYEN", "TRANGTHAI", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_AU_VAITRO", "TRANG_THAI");
            DropColumn("BTSTC.PHF_AU_VAITRO_CHUCNANG", "TRANG_THAI");
            DropColumn("BTSTC.PHF_AU_NGUOIDUNG_QUYEN", "TRANG_THAI");
            DropTable("BTSTC.PHF_AU_NHOMQUYEN");
        }
    }
}
