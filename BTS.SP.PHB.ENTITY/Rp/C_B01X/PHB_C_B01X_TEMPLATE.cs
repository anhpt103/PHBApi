using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.C_B01X
{
    [Table("PHB_C_B01X_TEMPLATE")]
    public class PHB_C_B01X_TEMPLATE : BaseEntity
    {
        [Description("Loại tài khoản: 1 TRONG BẢNG , 2 NGOÀI BẢNG")]
        public int LOAI { get; set; }

        [Description("Mã tài khoản")]
        [Required]
        [StringLength(20)]
        public string MA_TAIKHOAN { get; set; }

        [Description("Tên tài khoản")]
        [Required]
        [StringLength(250)]
        public string TEN_TAIKHOAN { get; set; }
        
    }
}
