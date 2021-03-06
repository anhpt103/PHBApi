﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.SP.API.ENTITY.Models.Au.PHF
{
    [Table("PHF_AU_NGUOIDUNG_QUYEN")]
    public class PHF_AU_NGUOIDUNG_QUYEN : DataInfoEntityPHF
    {
        [Column("USERNAME")]
        [StringLength(50)]
        [Required]
        public string USERNAME { get; set; }
        [Column("MACHUCNANG")]
        [StringLength(50)]
        [Required]
        public string MACHUCNANG { get; set; }
        [Column("XEM")]
        [Required]
        public bool XEM { get; set; }
        [Column("THEM")]
        [Required]
        public bool THEM { get; set; }
        [Column("SUA")]
        [Required]
        public bool SUA { get; set; }
        [Column("XOA")]
        [Required]
        public bool XOA { get; set; }
        [Column("DUYET")]
        [Required]
        public bool DUYET { get; set; }

        [Column("TRANG_THAI")]
        [Description("Trạng thái sử dụng (1: Đang sử dụng; 0: Không sử dụng) ")]
        public int TRANG_THAI { get; set; }
    }
}
