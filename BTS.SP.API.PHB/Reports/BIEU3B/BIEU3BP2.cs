namespace BTS.SP.API.PHB.Reports.BIEU3B
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for BIEU3BP2.
    /// </summary>
    public partial class BIEU3BP2 : Telerik.Reporting.Report
    {
        public static Color ColorFromName(string colorName)
        {
            if (!string.IsNullOrEmpty(colorName))
            {
                return Color.FromName(colorName);
            }
            return Color.Transparent;
        }
        public BIEU3BP2()
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