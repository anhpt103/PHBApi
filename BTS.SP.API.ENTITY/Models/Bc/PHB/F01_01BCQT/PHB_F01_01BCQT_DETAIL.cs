﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.F01_01BCQT
{
    [Table("PHB_F01_01BCQT_DETAIL")]
    public class PHB_F01_01BCQT_DETAIL:DataInfoEntity
    {
        [Description("Guid ID trong  PHB_F01_01BCQT")]
        [StringLength(50)]
        [Required]

        public string PHB_F01_01BCQT_REFID { get; set; }

        [Description("NOI_DUNG_CHI")]
        [StringLength(255)]
        [Required]
        public string NOI_DUNG_CHI { get; set; }

        [Description("Mã loại")]
        [StringLength(3)]
        public string MA_LOAI { get; set; }

        [Description("Mã khoản")]
        [StringLength(3)]
        public string MA_KHOAN { get; set; }

        [Description("Mã mục")]
        [StringLength(4)]
        public string MA_MUC { get; set; }

        [Description("Mã tiểu mục")]
        [StringLength(4)]
        public string MA_TIEU_MUC { get; set; }

        [Description("Tổng số")]
        public decimal TONG_SO { get; set; }

        [Description("Ngân sách nhà nước trong nước")]
        public decimal NSNN_TRONGNUOC { get; set; }

        [Description("Viện trợ")]
        public decimal VIEN_TRO { get; set; }

        [Description("Vay nợ nước ngoài")]
        public decimal VAYNO_NUOCNGOAI { get; set; }

        [Description("Nguồn phí được khấu trừ để lại")]
        public decimal NP_DELAI { get; set; }

        [Description("Nguồn hoạt động khác để lại")]
        public decimal NHD_DELAI { get; set; }

        [Description("Loại nội dung chi")]
        public int LOAI { get; set; }
    }
}