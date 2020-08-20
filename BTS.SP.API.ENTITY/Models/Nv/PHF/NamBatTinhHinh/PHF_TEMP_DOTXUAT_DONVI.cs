using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Nv.PHF.NamBatTinhHinh
{
    [Table("PHF_TEMP_DOTXUAT_DONVI")]
    public class PHF_TEMP_DOTXUAT_DONVI : DataInfoEntityPHF
    {
        [Column("MA_DVQHNS")]
        [StringLength(50)]
        [Description("Mã đơn vị quan hệ ngân sách")]
        public string MA_DVQHNS { get; set; }

        [Column("TEN_DVQHNS")]
        [StringLength(2000)]
        [Description("Tên đơn vị quan hệ ngân sách")]
        public string TEN_DVQHNS { get; set; }

        [Column("MA_DIABAN")]
        [StringLength(50)]
        [Description("Mã địa bàn")]
        public string MA_DIABAN { get; set; }

        [Column("TEN_DIABAN")]
        [StringLength(2000)]
        [Description("Tên địa bàn")]
        public string TEN_DIABAN { get; set; }

        [Column("MA_CHUONG")]
        [StringLength(50)]
        [Description("Mã chương")]
        public string MA_CHUONG { get; set; }

        [Column("TEN_CHUONG")]
        [StringLength(2000)]
        [Description("Tên chương")]
        public string TEN_CHUONG { get; set; }

        [Column("MA_LOAI")]
        [StringLength(50)]
        [Description("Mã loại")]
        public string MA_LOAI { get; set; }

        [Column("TEN_LOAI")]
        [StringLength(2000)]
        [Description("Tên loại")]
        public string TEN_LOAI { get; set; }

        [Column("MA_KHOAN")]
        [StringLength(50)]
        [Description("Mã khoản")]
        public string MA_KHOAN { get; set; }

        [Column("TEN_KHOAN")]
        [StringLength(2000)]
        [Description("Tên khoản")]
        public string TEN_KHOAN { get; set; }

        [Column("MA_MUC")]
        [StringLength(50)]
        [Description("Mã mục")]
        public string MA_MUC { get; set; }

        [Column("TEN_MUC")]
        [StringLength(2000)]
        [Description("Tên mục")]
        public string TEN_MUC { get; set; }

        [Column("MA_TIEUMUC")]
        [StringLength(50)]
        [Description("Mã tiểu mục")]
        public string MA_TIEUMUC { get; set; }

        [Column("TEN_TIEUMUC")]
        [StringLength(2000)]
        [Description("Tên tiểu mục")]
        public string TEN_TIEUMUC { get; set; }

        [Column("MA_CAP")]
        [StringLength(50)]
        [Description("Mã cấp ngân sách")]
        public string MA_CAP { get; set; }

        [Column("TEN_CAP")]
        [StringLength(2000)]
        [Description("Tên cấp ngân sách")]
        public string TEN_CAP { get; set; }

        [Column("SOTIEN")]
        [Description("Số tiền")]
        public int SOTIEN { get; set; }

        [Column("NGAY_KET_SO")]
        [Description("Ngày Post sổ")]
        public Nullable<DateTime> NGAY_KET_SO { get; set; }

        [Column("NGAY_HIEU_LUC")]
        [Description("Ngày hạch toán")]
        public Nullable<DateTime> NGAY_HIEU_LUC { get; set; }
    }
}
