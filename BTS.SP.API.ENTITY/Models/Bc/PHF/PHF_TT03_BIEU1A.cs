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
    [Table("PHF_TT03_BIEU1A")]
    public class PHF_TT03_BIEU1A : DataInfoEntityPHF
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

        [Column("COT1")]
        [Description("Số cuộc thanh tra - Tổng số")]
        public decimal? COT1 { get; set; }

        [Column("COT2")]
        [Description("Số cuộc thanh tra -  Đang thực hiện - Kỳ trước chuyển sang")]
        public decimal? COT2 { get; set; }

        [Column("COT3")]
        [Description("Số cuộc thanh tra - Đang thực hiện - Triển khai trong kỳ báo cáo")]
        public decimal? COT3 { get; set; }

        [Column("COT4")]
        [Description("Số cuộc thanh tra - Hình thức - Theo kế hoạch")]
        public decimal? COT4 { get; set; }

        [Column("COT5")]
        [Description("Số cuộc thanh tra - Hình thức - Đột xuất")]
        public decimal? COT5 { get; set; }

        [Column("COT6")]
        [Description("Số cuộc thanh tra - Tiến độ - Kết thúc thanh tra trực tiếp")]
        public decimal? COT6 { get; set; }

        [Column("COT7")]
        [Description("Số cuộc thanh tra - Tiến độ - Đã ban hành kết luận")]
        public decimal? COT7 { get; set; }

        [Column("COT8")]
        [Description("Số đơn vị được thanh tra")]
        public decimal? COT8 { get; set; }

        [Column("COT9")]
        [Description("Số đơn vị có vi phạm")]
        public decimal? COT9 { get; set; }

        [Column("COT10")]
        [Description("Tổng vi phạm - Tiền ( hoặc tài sản quy thanh tiền)")]
        public decimal? COT10 { get; set; }

        [Column("COT11")]
        [Description("Tổng vi phạm - Đất(m2)")]
        public decimal? COT11 { get; set; }

        [Column("COT12")]
        [Description("Kiến nghị thu hồi - Tiền(Tr.đ)")]
        public decimal? COT12 { get; set; }

        [Column("COT13")]
        [Description("Kiến nghị thu hồi - Đất(m2)")]
        public decimal? COT13 { get; set; }

        [Column("COT14")]
        [Description("Kiến nghị khác - Tiền(Tr.đ)")]
        public decimal? COT14 { get; set; }

        [Column("COT15")]
        [Description("Kiến nghị khác - Đất(m2)")]
        public decimal? COT15 { get; set; }

        [Column("COT16")]
        [Description("Kiến nghị xử lý - Hành chính - Tổ chức")]
        public decimal? COT16 { get; set; }

        [Column("COT17")]
        [Description("Kiến nghị xử lý - Hành chính - cá nhân")]
        public decimal? COT17 { get; set; }

        [Column("COT18")]
        [Description("Kiến nghị xử lý - Chuyển cơ quan điều tra - vụ")]
        public decimal? COT18 { get; set; }

        [Column("COT19")]
        [Description("Kiến nghị xử lý - Chuyển cơ quan điều tra - Đối tượng")]
        public decimal? COT19 { get; set; }

        [Column("COT20")]
        [Description("Đã thu - Tiền(Tr.đ)")]
        public decimal? COT20 { get; set; }

        [Column("COT21")]
        [Description("Đã thu - Đất(m2)")]
        public decimal? COT21 { get; set; }

        [Column("COT22")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Tổng số KLTT và QĐ xử lý đã kiểm tra, đôn đốc")]
        public decimal? COT22 { get; set; }

        [Column("COT23")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Kết quả kiểm tra, đôn đốc - Tiền(Tr.đ) - Phải thu")]
        public decimal? COT23 { get; set; }

        [Column("COT24")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Kết quả kiểm tra, đôn đốc - Tiền(Tr.đ) - Đã thu")]
        public decimal? COT24 { get; set; }

        [Column("COT25")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Kết quả kiểm tra, đôn đốc - Đất(m2) - Phải thu")]
        public decimal? COT25 { get; set; }

        [Column("COT26")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Kết quả kiểm tra, đôn đốc - Đất(m2) - Đã thu")]
        public decimal? COT26 { get; set; }

        [Column("COT27")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Kết quả kiểm tra, đôn đốc - Đã xử lý hành chính - Tổ chức")]
        public decimal? COT27 { get; set; }

        [Column("COT28")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Kết quả kiểm tra, đôn đốc - Đã xử lý hành chính - Cá nhân")]
        public decimal? COT28 { get; set; }

        [Column("COT29")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Kết quả kiểm tra, đôn đốc - Đã khởi tố - Vụ")]
        public decimal? COT29 { get; set; }

        [Column("COT30")]
        [Description("Kiểm tra, đôn đốc việc thực hiện kết luận thanh tra, quyết định xử lý về thanh tra - Kết quả kiểm tra, đôn đốc - Đã khởi tố - Đối tượng")]
        public decimal? COT30 { get; set; }

        [Column("COT31")]
        [StringLength(1000)]
        [Description("Ghi chú")]
        public string COT31 { get; set; }
    }
}
