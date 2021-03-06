﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.QuyTrinhTTGia
{
    [Table("PHF_BM04_TONGHOP_SANXUAT")]
    public class PHF_BM04_TONGHOP_SANXUAT : DataInfoEntityPHF
    {
        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("STT_TIEUDE")]
        [Description("Số thứ tự tiêu đề")]
        [StringLength(5)]
        public string STT_TIEUDE { get; set; }

        [Column("STT_CHA")]
        [Description("Số thứ tự cha")]
        public int STT_CHA { get; set; }

        [Column("MA_FILE")]
        [StringLength(200)]
        [Description("Mã file Template")]
        public string MA_FILE { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_SANPHAM")]
        [StringLength(500)]
        [Description("Tên sản phẩm")]
        public string TEN_SANPHAM { get; set; }

        [Column("SANLUONG_BAN")]
        [Description("Sản lượng bán")]
        public int? SANLUONG_BAN { get; set; }

        [Column("COT3")]
        [Description("Cột 3")]
        public decimal? COT3 { get; set; }

        [Column("COT4")]
        [Description("Cột 4")]
        public decimal? COT4 { get; set; }

        [Column("COT5")]
        [Description("Cột 5")]
        public decimal? COT5 { get; set; }

        [Column("COT6")]
        [Description("Cột 6")]
        public decimal? COT6 { get; set; }

        [Column("COT7")]
        [Description("Cột 7")]
        public decimal? COT7 { get; set; }

        [Column("COT8")]
        [Description("Cột 8")]
        public decimal? COT8 { get; set; }

        [Column("COT9")]
        [Description("Cột 9")]
        public decimal? COT9 { get; set; }

        [Column("COT10")]
        [Description("Cột 10")]
        public decimal? COT10 { get; set; }

        [Column("COT11")]
        [Description("Cột 11")]
        public decimal? COT11 { get; set; }

        [Column("COT12")]
        [Description("Cột 12")]
        public decimal? COT12 { get; set; }

        [Column("COT13")]
        [Description("Cột 13")]
        public decimal? COT13 { get; set; }

        [Column("COT14")]
        [Description("Cột 14")]
        public decimal? COT14 { get; set; }

        [Column("COT17")]
        [Description("Cột 17")]
        public decimal? COT17 { get; set; }
    }
}
