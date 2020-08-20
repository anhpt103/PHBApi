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
    [Table("PHF_TT03_BIEU1E")]
    public class PHF_TT03_BIEU1E : DataInfoEntityPHF
    {
        [Column("MABAOCAO")]
        [StringLength(200)]
        [Description("Mã báo cáo")]
        public string MABAOCAO { get; set; }

        [Column("MA_FILE_PK")]
        [StringLength(200)]
        [Description("Mã file pk Template")]
        public string MA_FILE_PK { get; set; }

        [Column("DONVI")]
        [StringLength(500)]
        [Description("Đơn vị")]
        public string DONVI { get; set; }

        [Column("NOIDUNG")]
        [StringLength(500)]
        [Description("Nội dung")]
        public string NOIDUNG { get; set; }

        [Column("COT1")]
        [Description("Số cuộc thanh tra - Tổng số")]
        public decimal? COT1 { get; set; }

        [Column("COT2")]
        [Description("Số cuộc thanh tra - Thành lập đoàn")]
        public decimal? COT2 { get; set; }

        [Column("COT3")]
        [Description("Số cuộc thanh tra - Thanh tra độc lập")]
        public decimal? COT3 { get; set; }

        [Column("COT4")]
        [Description("Số cá nhân được thanh tra")]
        public decimal? COT4 { get; set; }

        [Column("COT5")]
        [Description("Số cá nhân được kiểm tra")]
        public decimal? COT5 { get; set; }

        [Column("COT6")]
        [Description("Số tổ chức được thanh tra")]
        public decimal? COT6 { get; set; }

        [Column("COT7")]
        [Description("Số tổ chức được kiểm tra")]
        public decimal? COT7 { get; set; }

        [Column("COT8")]
        [Description("Số có vi phạm - Tổng số")]
        public decimal? COT8 { get; set; }

        [Column("COT9")]
        [Description("Số có vi phạm - Cá nhân")]
        public decimal? COT9 { get; set; }

        [Column("COT10")]
        [Description("Số có vi phạm - Tổ chức")]
        public decimal? COT10 { get; set; }

        [Column("COT11")]
        [Description("Số QĐ xử phạt được ban hành - Tổng số")]
        public decimal? COT11 { get; set; }

        [Column("COT12")]
        [Description("Số QĐ xử phạt được ban hành - Cá nhân")]
        public decimal? COT12 { get; set; }

        [Column("COT13")]
        [Description("Số QĐ xử phạt được ban hành - Tổ chức")]
        public decimal? COT13 { get; set; }

        [Column("COT14")]
        [Description("Số tiền vi phạm - Tổng số")]
        public decimal? COT14 { get; set; }

        [Column("COT15")]
        [Description("Số tiền vi phạm - Cá nhân")]
        public decimal? COT15 { get; set; }

        [Column("COT16")]
        [Description("Số tiền vi phạm - Tổ chức")]
        public decimal? COT16 { get; set; }

        [Column("COT17")]
        [Description("Số tiền kiến nghị thu hồi")]
        public decimal? COT17 { get; set; }

        [Column("COT18")]
        [Description("Số tiền sử lý tài sản vi phạm - Tổng số")]
        public decimal? COT18 { get; set; }

        [Column("COT19")]
        [Description("Số tiền sử lý tài sản vi phạm - Tịch thu")]
        public decimal? COT19 { get; set; }

        [Column("COT20")]
        [Description("Số tiền sử lý tài sản vi phạm - Tiêu hủy")]
        public decimal? COT20 { get; set; }

        [Column("COT21")]
        [Description("Số tiền xử phạt vi phạm - Tổng số")]
        public decimal? COT21 { get; set; }

        [Column("COT22")]
        [Description("Số tiền xử phạt vi phạm - Tịch thu")]
        public decimal? COT22 { get; set; }

        [Column("COT23")]
        [Description("Số tiền xử phạt vi phạm - Tiêu hủy")]
        public decimal? COT23 { get; set; }

        [Column("COT24")]
        [Description("Số tiền đã thu - Tổng số")]
        public decimal? COT24 { get; set; }

        [Column("COT25")]
        [Description("Số tiền đã thu - Cá nhân")]
        public decimal? COT25 { get; set; }

        [Column("COT26")]
        [Description("Số tiền đã thu - Tổ chức")]
        public decimal? COT26 { get; set; }

        [Column("COT27")]
        [StringLength(1000)]
        [Description("Ghi chú")]
        public string COT27 { get; set; }
    }
}
