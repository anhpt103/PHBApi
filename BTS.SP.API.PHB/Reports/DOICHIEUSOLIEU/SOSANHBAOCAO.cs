using System.Linq;
using BTS.SP.API.ENTITY;
namespace BTS.SP.API.PHB.Reports.DOICHIEUSOLIEU
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    /// <summary>
    /// Summary description for SOSANHBAOCAO.
    /// </summary>
    public partial class SOSANHBAOCAO : Telerik.Reporting.Report
    {
        public static string TenDVQHNS(string maDVQHNS)
        {
            string result = "";
            if (!string.IsNullOrEmpty(maDVQHNS))
            {
                using (var context = new STCContext())
                {
                    var data = context.SYS_DVQHNS.FirstOrDefault(x => x.MA_DVQHNS == maDVQHNS);
                    if (data != null)
                    {
                        result = data.TEN_DVQHNS;
                    }
                }
            }
            return result;
        }
        public SOSANHBAOCAO()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}