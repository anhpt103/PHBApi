using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.HuongDanSd.PHA
{
    [Table("PHA_TAILIEU_HD")]
    public class TAILIEU_HD : DataInfoEntity
    {
        [Column("MA_TAILIEU")]
        [StringLength(200)]
        [Description("Mã tài liệu")]
        public string MA_TAILIEU { get; set; }

        [Column("STT")]
        [Description("Số thứ tự")]
        public int STT { get; set; }

        [Column("TEN_TAILIEU")]
        [StringLength(500)]
        [Description("Tên tài liệu")]
        public string TEN_TAILIEU { get; set; }

        [Column("DINHKEMFILE")]
        [StringLength(500)]
        [Description("File đính kèm")]
        public string DINHKEMFILE { get; set; }

        [Column("URL")]
        [StringLength(500)]
        [Description("Đường dẫn để lưu tệp upload")]
        public string URL { get; set; }

        [Column("THOIGIAN")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public virtual DateTime? THOIGIAN { get; set; }
    }
}
