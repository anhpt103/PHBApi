using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHF
{
    [Table("PHF_TT188_PL04")]
    public class PHF_TT188_PL04 : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(50)]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("TEN_FILE")]
        [StringLength(100)]
        public string TEN_FILE { get; set; }

        [Column("MADONG")]
        [StringLength(50)]
        public string MADONG { get; set; }

        [Column("DONVI")]
        [StringLength(500)]
        [Description("Đơn vị")]
        public string DONVI { get; set; }

        [Column("COT1")]
        [Description("Thông tin phát hiện lãng phí nhận được, vụ việc lãng phí trong kỳ báo cáo - Tổng số")]
        public decimal? COT1 { get; set; }

        [Column("COT2")]
        [Description("Thông tin phát hiện lãng phí nhận được, vụ việc lãng phí trong kỳ báo cáo -  Trong đó - Thông tin phát hiện lãng phí nhận được")]
        public decimal? COT2 { get; set; }

        [Column("COT3")]
        [Description("Thông tin phát hiện lãng phí nhận được, vụ việc lãng phí trong kỳ báo cáo - Trong đó - Vụ việc lãng phí")]
        public decimal? COT3 { get; set; }

        [Column("COT4")]
        [Description("Tổng số vụ việc đã giải quyết")]
        public decimal? COT4 { get; set; }

        [Column("COT5")]
        [Description("Đã xử lý - Bồi thường thiệt hại - Số người phải bồi thường")]
        public decimal? COT5 { get; set; }

        [Column("COT6")]
        [Description("Đã xử lý - Bồi thường thiệt hại - Số tiền bồi thường (triệu đồng)")]
        public decimal? COT6 { get; set; }

        [Column("COT7")]
        [Description("Đã xử lý - Xử lý hành chính - Số vụ việc")]
        public decimal? COT7 { get; set; }

        [Column("COT8")]
        [Description("Đã xử lý - Xử lý hành chính - Số người bị xử lý")]
        public decimal? COT8 { get; set; }

        [Column("COT9")]
        [Description("Đã xử lý - Xử lý kỷ luật - Số vụ việc")]
        public decimal? COT9 { get; set; }

        [Column("COT10")]
        [Description("Đã xử lý - Xử lý kỷ luật - Số người bị xử lý")]
        public decimal? COT10 { get; set; }

        [Column("COT11")]
        [Description("Đã xử lý - Chuyển hồ sơ xử lý hình sự - Số vụ việc đã chuyển hồ sơ xử lý hình sự")]
        public decimal? COT11 { get; set; }

        [Column("COT12")]
        [Description("Đã xử lý - Chuyển hồ sơ xử lý hình sự - Số vụ đã khởi tố")]
        public decimal? COT12 { get; set; }

        [Column("COT13")]
        [Description("Đã xử lý - Chuyển hồ sơ xử lý hình sự - Số đối tượng đã khởi tố")]
        public decimal? COT13 { get; set; }

        [Column("COT14")]
        [Description("Chưa xử lý - Số vụ chưa xử lý")]
        public decimal? COT14 { get; set; }

        [Column("COT15")]
        [Description("Chưa xử lý - Số người chưa xử lý")]
        public decimal? COT15 { get; set; }

        [Column("COT16")]
        [Description("Chưa xử lý - Nguyên nhân")]
        public decimal? COT16 { get; set; }

        [Column("GHICHU")]
        [StringLength(300)]
        public string GHICHU { get; set; }
    }
}
