using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF
{
    [Table("PHF_BM_05ATT_TCDN")]
    public class PHF_BM_05ATT_TCDN : DataInfoEntityPHF
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

        [Column("NOIDUNG")]
        [StringLength(1000)]
        [Description("Nội dung")]
        public string NOIDUNG { get; set; }

        [Column("TONGSO")]
        [StringLength(1000)]
        [Description("Tổng số")]
        public string TONGSO { get; set; }

        [Column("TONGSO_NTH")]
        [StringLength(1000)]
        [Description("Nợ trong hạn - Tổng số")]
        public string TONGSO_NTH { get; set; }

        [Column("THOIGIANTRA_NTH")]
        [StringLength(1000)]
        [Description("Nợ trong hạn - Trong đó do cơ cấu lại thời gian trả")]
        public string THOIGIANTRA_NTH { get; set; }

        [Column("TONGSO_NQH")]
        [StringLength(1000)]
        [Description("Nợ quá hạn thanh toán - Tổng số")]
        public string TONGSO_NQH { get; set; }

        [Column("NOQUAHAN_6TDUOI1N")]
        [StringLength(1000)]
        [Description("Nợ quá hạn thanh toán - Từ 6 tháng - dưới 1 năm")]
        public string NOQUAHAN_6TDUOI1N { get; set; }

        [Column("NOQUAHAN_1NDEN2N")]
        [StringLength(1000)]
        [Description("Nợ quá hạn thanh toán - Từ 1 năm - 2 năm")]
        public string NOQUAHAN_1NDEN2N { get; set; }

        [Column("NOQUAHAN_2NDEN3N")]
        [StringLength(1000)]
        [Description("Nợ quá hạn thanh toán - Từ 2 năm - 3 năm")]
        public string NOQUAHAN_2NDEN3N { get; set; }

        [Column("NOQUAHAN_TREN3N")]
        [StringLength(1000)]
        [Description("Nợ quá hạn thanh toán - Nợ trên 3 năm")]
        public string NOQUAHAN_TREN3N { get; set; }

        [Column("NOQUAHAN_CHOXULY")]
        [StringLength(1000)]
        [Description("Nợ quá hạn thanh toán - Nợ được khoanh nợ chờ xử lý")]
        public string NOQUAHAN_CHOXULY { get; set; }

        [Column("LAIPHAITRA2")]
        [StringLength(1000)]
        [Description("Lãi phải trả 2")]
        public string LAIPHAITRA2 { get; set; }

        [Column("LAIPHAITRA3")]
        [StringLength(1000)]
        [Description("Lãi phải trả 3")]
        public string LAIPHAITRA3 { get; set; }

        [Column("SAIMUCDICH")]
        [StringLength(1000)]
        [Description("Sử dụng sai mục đích nguồn vốn vay")]
        public string SAIMUCDICH { get; set; }

        [Column("IS_BOLD")]
        [Description("Font in đậm")]
        public int IS_BOLD { get; set; }

        [Column("IS_ITALIC")]
        [Description("Font in nghiêng")]
        public int IS_ITALIC { get; set; }
    }
}
