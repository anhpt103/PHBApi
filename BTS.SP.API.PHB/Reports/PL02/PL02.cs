namespace BTS.SP.API.PHB.Reports.PL02
{
    using Controllers.REPORT;
    using SP.PHB.ENTITY.Helper;
    using SP.PHB.ENTITY.Rp;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for PL02.
    /// </summary>
    public partial class PL02 : Telerik.Reporting.Report
    {
        public PL02()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
           // DtoPL01 data = new DtoPL01();
           // ReportParameterAvailableValues a = new ReportParameterAvailableValues();
           // var abc = this.DataSource;
           // //foreach(var item in this.ReportParameters)
           // //{
           // //    data.DONG11 = {
           // //        yield.};
           // //}
            

           // data.DONG11 = double.Parse(this.ReportParameters[0].Value == null ? "0" :  this.ReportParameters[0].Value .ToString());
           // data.DONG12 = double.Parse(this.ReportParameters[1].Value == null ? "0" :  this.ReportParameters[1].Value .ToString());
           // data.DONG13 = double.Parse(this.ReportParameters[2].Value == null ? "0" :  this.ReportParameters[2].Value .ToString());
           // data.DONG21 = double.Parse(this.ReportParameters[3].Value == null ? "0" :  this.ReportParameters[3].Value .ToString());
           // data.DONG22 = double.Parse(this.ReportParameters[4].Value == null ? "0" :  this.ReportParameters[4].Value .ToString());
           // data.DONG23 = double.Parse(this.ReportParameters[5].Value == null ? "0" :  this.ReportParameters[5].Value .ToString());
           // data.DONG24 = double.Parse(this.ReportParameters[6].Value == null ? "0" :  this.ReportParameters[6].Value .ToString());
           // data.DONG31 = double.Parse(this.ReportParameters[7].Value == null ? "0" :  this.ReportParameters[7].Value .ToString());
           // data.DONG32 = double.Parse(this.ReportParameters[8].Value == null ? "0" :  this.ReportParameters[8].Value .ToString());
           // data.DONG41 = double.Parse(this.ReportParameters[9].Value == null ? "0" :  this.ReportParameters[9].Value .ToString());
           // data.DONG42 = double.Parse(this.ReportParameters[10].Value == null ? "0" : this.ReportParameters[10].Value.ToString());
           //data.DONG421 = double.Parse(this.ReportParameters[11].Value == null ? "0" : this.ReportParameters[11].Value.ToString());
           //data.DONG422 = double.Parse(this.ReportParameters[12].Value == null ? "0" : this.ReportParameters[12].Value.ToString());
           // data.DONG43 = double.Parse(this.ReportParameters[13].Value == null ? "0" : this.ReportParameters[13].Value.ToString());
           // data.DONG44 = double.Parse(this.ReportParameters[14].Value == null ? "0" : this.ReportParameters[14].Value.ToString());
           // data.DONG45 = double.Parse(this.ReportParameters[15].Value == null ? "0" : this.ReportParameters[15].Value.ToString());
           // data.DONG46 = double.Parse(this.ReportParameters[16].Value == null ? "0" : this.ReportParameters[16].Value.ToString());
           //data.DONG461 = double.Parse(this.ReportParameters[17].Value == null ? "0" : this.ReportParameters[17].Value.ToString());
           //data.DONG462 = double.Parse(this.ReportParameters[18].Value == null ? "0" : this.ReportParameters[18].Value.ToString());
           // data.DONG51 = double.Parse(this.ReportParameters[19].Value == null ? "0" : this.ReportParameters[19].Value.ToString());
           // data.DONG52 = double.Parse(this.ReportParameters[20].Value == null ? "0" : this.ReportParameters[20].Value.ToString());
           // data.DONG53 = double.Parse(this.ReportParameters[21].Value == null ? "0" : this.ReportParameters[21].Value.ToString());
           // data.DONG20 = double.Parse(this.ReportParameters[22].Value == null ? "0" : this.ReportParameters[21].Value.ToString());
            DateTime localDate = DateTime.Now;
            textBox6.Value = "Ngày " + localDate.Day + " Tháng "+localDate.Month+" Năm "+localDate.Year;
            //textBox8.Value = "Xét duyệt/Thẩm định quyết toán ngân sách năm " + this.ReportParameters[23].Value;
            //textBox9.Value = "Đơn vị được xét duyệt/thẩm định: "+this.ReportParameters[24].Value;
            //textBox10.Value = "Mã chương: " + this.ReportParameters[25].Value;
            //textBox18.Value = "- Tổng số thu trong năm: "+data.DONG11+" đồng";
            //textBox19.Value = "- Số phải nộp ngân sách nhà nước: " + data.DONG12 + " đồng";
            //textBox20.Value = "- Số phí được khấu trừ để lại: " + data.DONG13 + " đồng";
            //textBox22.Value = "2. Đối chiếu số liệu kết quả chênh lệch thu lớn hơn chi trong năm: "+data.DONG20+" đồng, trong đó:";

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }


    }
}