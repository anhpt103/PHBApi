using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.PBDT.B121
{
    public class UploadExcel_ViewModel
    {
        public HttpPostedFile File { get; set; }
        public int Nam { get; set; }
    }
}