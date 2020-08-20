using BTS.SP.PHB.SERVICE.BuildQuery.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.ViewModels.SYS.sysLogChucNang
{
    public class SearchViewModel
    {
        public PagedObj<LogSigninViewModel> Page { get; set; }
        public SortBy SortBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Search_UserName { get; set; }
        public string Search_IP { get; set; }
        public string Search_Action { get; set; }
        public string Search_DBHC { get; set; }
    }

    public enum SortBy
    {
        Time = 1,
        UserName = 2,
        IP = 3,
        Action = 4
    }

    public class LogSigninViewModel
    {
        public string Username { get; set; }
        public string DBHC { get; set; }
        public string DiaChiMay { get; set; }
        public DateTime ThoiGianTruyCap { get; set; }
        public string ChucNang { get; set; }
    }
}