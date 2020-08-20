using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.API.ENTITY.Models.Bc.PHB.PBDT.B1303
{
    [Table("PHB_PBDT_B1303_DETAIL")]
    public class PHB_PBDT_B1303_DETAIL : DataInfoEntity
    {
        [Required]
        [StringLength(50)]
        public string PHB_PBDT_B1303_REFID { get; set; }

        public int STT_SAPXEP { get; set; }
        [StringLength(5)]
        public string STT { get; set; }

        [StringLength(5)]
        public string MA_SO { get; set; }

        [StringLength(5)]
        public string MA_CHA { get; set; }

        [Required]
        [StringLength(1000)]
        public string CHI_TIEU { get; set; }

        
        [StringLength(250)]
        public string CQ_CHUTRI { get; set; }

        [StringLength(250)]
        public string THOIGIAN_THUCHIEN { get; set; }

        [StringLength(250)]
        public string QUYETDINH_PHEDUYET { get; set; }


        //Kinh phí được phê duyệt		
        public decimal? KPPD_TONGSO { get; set; }

        public decimal? KPPD_NGUON_NSNN { get; set; }

        public decimal? KPPD_NGUON_KHAC { get; set; }


        //Năm … (năm hiện hành)			
        public decimal? NAMHH_TONGSO { get; set; }

        public decimal? NAMHH_DT { get; set; }

        public decimal? NAMHH_UOC_THUCHIEN { get; set; }

        public decimal? NAMHH_KINHPHI_THUCHIEN { get; set; }


        //Lũy kế số kinh phí đã bố trí đến hết năm… (năm hiện hành)		
        public decimal? LUYKE_TONGSO { get; set; }

        public decimal? LUYKE_NGUON_NSNN { get; set; }

        public decimal? LUYKE_NGUON_KHAC { get; set; }


        //Dự toán bố trí năm… (năm kế hoạch)		
        public decimal? NAMKH_TONGSO { get; set; }

        public decimal? NAMKH_NGUON_NSNN { get; set; }

        public decimal? NAMKH_NGUON_KHAC { get; set; }


        public int IS_BOLD { get; set; }
        public int IS_ITALIC { get; set; }
        public int IS_OPTIONAL { get; set; }
    }
}
