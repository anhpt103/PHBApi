namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190419_EDIT_PHF_BM04_TONGHOP_SANXUAT_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "SANLUONG", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT15", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT19", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "SANLUONG_BAN");
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT4");
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT6");
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT8");
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT10");
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT12");
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT14");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT14", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT12", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT10", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT8", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT6", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT4", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "SANLUONG_BAN", c => c.Decimal(precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT19");
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "COT15");
            DropColumn("BTSTC.PHF_BM04_TONGHOP_SANXUAT", "SANLUONG");
        }
    }
}
