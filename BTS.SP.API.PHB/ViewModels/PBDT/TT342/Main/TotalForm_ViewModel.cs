using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.Main
{
    public class TotalForm_ViewModel
    {
        public List<FormsOfDonVi_ViewModel> FormsOfDonVi { get; set; }
    }

    public class FormsOfDonVi_ViewModel
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
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