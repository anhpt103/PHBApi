using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.TT344.Main
{
    public class TotalForm_ViewModel
    {
        public List<FormsOfDBHC_ViewModel> FormsOfDBHC { get; set; }
    }

    public class FormsOfDBHC_ViewModel
    {
        public string MaDBHC { get; set; }
        public string TenDBHC { get; set; }
        public List<FormViewModel> Forms { get; set; }
    }

    public class FormViewModel
    {
        public int Id { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }
        public int Nam { get; set; }
        public int TrangThai { get; set; }
    }
}