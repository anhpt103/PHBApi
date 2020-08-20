﻿namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07012019_ADD_TABLE_PHF_TIENDO_THUCHIEN_KH_DINHKEM_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TIENDO_THUCHIEN_KH_DINHKEM",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_PHIEU = c.String(nullable: false, maxLength: 50),
                        MA_DOITUONG = c.String(maxLength: 500),
                        NAM_THANHTRA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        LOAI_FILE = c.String(maxLength: 200),
                        FILE_PATH = c.String(maxLength: 500),
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
            DropTable("BTSTC.PHF_TIENDO_THUCHIEN_KH_DINHKEM");
        }
    }
}
