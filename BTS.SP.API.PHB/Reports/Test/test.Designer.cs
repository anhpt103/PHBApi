namespace BTS.SP.API.PHB.Reports.Test
{
    partial class test
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.crosstab1 = new Telerik.Reporting.Crosstab();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(2D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.crosstab1});
            this.detail.Name = "detail";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "STCConnection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.Parameters.Add(new Telerik.Reporting.SqlDataSourceParameter("MA_DVQHNS", System.Data.DbType.String, "= Parameters.DSDVQHNS.Value"));
            this.sqlDataSource1.Parameters.Add(new Telerik.Reporting.SqlDataSourceParameter("DONVI_TIEN", System.Data.DbType.Decimal, "= Parameters.DONVI_TIEN.Value"));
            this.sqlDataSource1.Parameters.Add(new Telerik.Reporting.SqlDataSourceParameter("NAM_BC", System.Data.DbType.String, "= Parameters.NAM_BC.Value"));
            this.sqlDataSource1.Parameters.Add(new Telerik.Reporting.SqlDataSourceParameter("KY_BC", System.Data.DbType.String, "= Parameters.KY_BC.Value"));
            this.sqlDataSource1.SelectCommand = "BTSTC.PHB_F01_02P1_SUMREPORT";
            this.sqlDataSource1.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // crosstab1
            // 
            this.crosstab1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.177D)));
            this.crosstab1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.24D)));
            this.crosstab1.Body.SetCellContent(0, 0, this.textBox5);
            tableGroup3.Name = "group";
            tableGroup2.ChildGroups.Add(tableGroup3);
            tableGroup2.Name = "group2";
            tableGroup2.ReportItem = this.textBox6;
            tableGroup1.ChildGroups.Add(tableGroup2);
            tableGroup1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.MA_LOAI"));
            tableGroup1.Name = "columnGroup";
            tableGroup1.ReportItem = this.textBox2;
            this.crosstab1.ColumnGroups.Add(tableGroup1);
            this.crosstab1.Corner.SetCellContent(0, 0, this.textBox8, 2, 1);
            this.crosstab1.DataSource = this.sqlDataSource1;
            this.crosstab1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.textBox5,
            this.textBox1,
            this.textBox2,
            this.textBox6});
            this.crosstab1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.crosstab1.Name = "crosstab1";
            tableGroup4.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.CHI_TIEU"));
            tableGroup4.Name = "rowGroup";
            tableGroup4.ReportItem = this.textBox1;
            this.crosstab1.RowGroups.Add(tableGroup4);
            this.crosstab1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.354D), Telerik.Reporting.Drawing.Unit.Inch(0.68D));
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.177D), Telerik.Reporting.Drawing.Unit.Inch(0.44D));
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox8.Value = "test";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.177D), Telerik.Reporting.Drawing.Unit.Inch(0.24D));
            this.textBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox1.Value = "= Fields.CHI_TIEU";
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.177D), Telerik.Reporting.Drawing.Unit.Inch(0.24D));
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox2.Value = "= Fields.MA_LOAI";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.177D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox6.StyleName = "";
            this.textBox6.Value = "= Fields.MA_KHOAN";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.177D), Telerik.Reporting.Drawing.Unit.Inch(0.24D));
            this.textBox5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox5.StyleName = "";
            this.textBox5.Value = "=Fields.GIA_TRI_LK";
            // 
            // test
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "test";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "DSDVQHNS";
            reportParameter1.Text = "DSDVQHNS";
            reportParameter1.Value = "1001886";
            reportParameter2.AllowNull = true;
            reportParameter2.Name = "DONVI_TIEN";
            reportParameter2.Text = "DONVI_TIEN";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.Value = "1";
            reportParameter3.AllowNull = true;
            reportParameter3.Name = "NAM_BC";
            reportParameter3.Text = "NAM_BC";
            reportParameter3.Value = "2020";
            reportParameter4.AllowNull = true;
            reportParameter4.Name = "KY_BC";
            reportParameter4.Text = "KY_BC";
            reportParameter4.Value = "0";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.Crosstab crosstab1;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
    }
}