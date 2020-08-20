namespace BTS.SP.API.PHB.Reports.PHB_C_B02A_X
{
    partial class PHB_C_B02A_X
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
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup6 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup7 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup8 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup9 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.714583694934845D);
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.table1});
            this.detailSection1.Name = "detailSection1";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "STCConnection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("MADBHC", System.Data.DbType.String, "= Parameters.MADBHC.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("MACHUONG", System.Data.DbType.String, "= Parameters.MACHUONG.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("NAMBC", System.Data.DbType.Decimal, "= Parameters.NAMBC.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("KYBC", System.Data.DbType.Decimal, "= Parameters.KYBC.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("LOAIBC", System.Data.DbType.Decimal, "= Parameters.LOAIBC.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("DSDVQHNS", System.Data.DbType.String, "= Parameters.DSDVQHNS.Value")});
            this.sqlDataSource1.SelectCommand = "BTSTC.PHB_C_B02A_X_SUMREPORT";
            this.sqlDataSource1.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.48750001192092896D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(2.6020870208740234D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.68541687726974487D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.5187520980834961D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.5395828485488892D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.3937509059906006D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.4374990463256836D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.31458333134651184D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox7);
            this.table1.Body.SetCellContent(0, 1, this.textBox8);
            this.table1.Body.SetCellContent(0, 2, this.textBox9);
            this.table1.Body.SetCellContent(0, 3, this.textBox10);
            this.table1.Body.SetCellContent(0, 4, this.textBox11);
            this.table1.Body.SetCellContent(0, 5, this.textBox12);
            this.table1.Body.SetCellContent(0, 6, this.textBox15);
            tableGroup1.Name = "sTT";
            tableGroup1.ReportItem = this.textBox16;
            tableGroup2.Name = "nOI_DUNG";
            tableGroup2.ReportItem = this.textBox17;
            tableGroup3.Name = "mA_SO";
            tableGroup3.ReportItem = this.textBox18;
            tableGroup4.Name = "dU_TOAN";
            tableGroup4.ReportItem = this.textBox19;
            tableGroup6.Name = "group5";
            tableGroup6.ReportItem = this.textBox5;
            tableGroup7.Name = "group6";
            tableGroup7.ReportItem = this.textBox6;
            tableGroup5.ChildGroups.Add(tableGroup6);
            tableGroup5.ChildGroups.Add(tableGroup7);
            tableGroup5.Name = "tH_TRONG_THANG";
            tableGroup5.ReportItem = this.textBox20;
            tableGroup8.Name = "group";
            tableGroup8.ReportItem = this.textBox22;
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.ColumnGroups.Add(tableGroup3);
            this.table1.ColumnGroups.Add(tableGroup4);
            this.table1.ColumnGroups.Add(tableGroup5);
            this.table1.ColumnGroups.Add(tableGroup8);
            this.table1.DataSource = this.sqlDataSource1;
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox5,
            this.textBox6,
            this.textBox15,
            this.textBox16,
            this.textBox17,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox22});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.table1.Name = "table1";
            tableGroup9.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup9.Name = "detail";
            this.table1.RowGroups.Add(tableGroup9);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(9.6645889282226562D), Telerik.Reporting.Drawing.Unit.Inch(0.714583694934845D));
            this.table1.StyleName = "Normal.TableNormal";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5395830869674683D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Times New Roman";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.StyleName = "Normal.TableHeader";
            this.textBox5.Value = "Trong tháng";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3937512636184692D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Times New Roman";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.StyleName = "Normal.TableHeader";
            this.textBox6.Value = "Luỹ kế từ đầu năm ";
            // 
            // textBox7
            // 
            formattingRule1.Filters.Add(new Telerik.Reporting.Filter("= Fields.ISBOLD", Telerik.Reporting.FilterOperator.Equal, "1"));
            formattingRule1.Style.Font.Bold = true;
            formattingRule1.Style.Font.Name = "Times New Roman";
            formattingRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            formattingRule1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.48750001192092896D), Telerik.Reporting.Drawing.Unit.Inch(0.31458333134651184D));
            this.textBox7.Style.Font.Name = "Times New Roman";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.StyleName = "Normal.TableBody";
            this.textBox7.Value = "= Fields.STT";
            // 
            // textBox8
            // 
            this.textBox8.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6020870208740234D), Telerik.Reporting.Drawing.Unit.Inch(0.31458333134651184D));
            this.textBox8.Style.Font.Name = "Times New Roman";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.StyleName = "Normal.TableBody";
            this.textBox8.Value = "= Fields.NOI_DUNG";
            // 
            // textBox9
            // 
            this.textBox9.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox9.Format = "{0:N0}";
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.68541687726974487D), Telerik.Reporting.Drawing.Unit.Inch(0.31458333134651184D));
            this.textBox9.Style.Font.Name = "Times New Roman";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.StyleName = "Normal.TableBody";
            this.textBox9.Value = "= Fields.MA_SO";
            // 
            // textBox10
            // 
            this.textBox10.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox10.Format = "{0:N0}";
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5187520980834961D), Telerik.Reporting.Drawing.Unit.Inch(0.31458333134651184D));
            this.textBox10.Style.Font.Name = "Times New Roman";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.StyleName = "Normal.TableBody";
            this.textBox10.Value = "= Fields.DU_TOAN";
            // 
            // textBox11
            // 
            this.textBox11.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox11.Format = "{0:N0}";
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5395828485488892D), Telerik.Reporting.Drawing.Unit.Inch(0.31458333134651184D));
            this.textBox11.Style.Font.Name = "Times New Roman";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.StyleName = "Normal.TableBody";
            this.textBox11.Value = "= Fields.TH_TRONG_THANG";
            // 
            // textBox12
            // 
            this.textBox12.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox12.Format = "{0:N0}";
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3937509059906006D), Telerik.Reporting.Drawing.Unit.Inch(0.31458333134651184D));
            this.textBox12.Style.Font.Name = "Times New Roman";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.StyleName = "Normal.TableBody";
            this.textBox12.Value = "= Fields.TH_LUYKE_DN";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1999999284744263D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1854162216186523D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox14,
            this.textBox13,
            this.textBox4,
            this.textBox39,
            this.textBox40,
            this.textBox3});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox15
            // 
            this.textBox15.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox15.Format = "{0:N0}";
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4374990463256836D), Telerik.Reporting.Drawing.Unit.Inch(0.31458333134651184D));
            this.textBox15.Style.Font.Name = "Times New Roman";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.StyleName = "Normal.TableBody";
            this.textBox15.Value = "=IIf(Fields.DU_TOAN <> 0 And Fields.TH_TRONG_THANG <> 0 And Fields.TH_LUYKE_DN <>" +
    " 0,((Fields.TH_TRONG_THANG+ Fields.TH_LUYKE_DN)/ Fields.DU_TOAN)*100,0)  ";
            // 
            // textBox16
            // 
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.48749998211860657D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Name = "Times New Roman";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.StyleName = "Normal.TableHeader";
            this.textBox16.Value = "STT";
            // 
            // textBox17
            // 
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.6020865440368652D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.Font.Name = "Times New Roman";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.StyleName = "Normal.TableHeader";
            this.textBox17.Value = "Nội dung";
            // 
            // textBox18
            // 
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.68541693687438965D), Telerik.Reporting.Drawing.Unit.Inch(0.40000006556510925D));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Times New Roman";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.StyleName = "Normal.TableHeader";
            this.textBox18.Value = "Mã số";
            // 
            // textBox19
            // 
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5187513828277588D), Telerik.Reporting.Drawing.Unit.Inch(0.40000003576278687D));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Times New Roman";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.StyleName = "Normal.TableHeader";
            this.textBox19.Value = "Dự toán";
            // 
            // textBox20
            // 
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.9333341121673584D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Times New Roman";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.StyleName = "Normal.TableHeader";
            this.textBox20.Value = "Thực hiện";
            // 
            // textBox22
            // 
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4374998807907105D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Times New Roman";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.StyleName = "Normal.TableHeader";
            this.textBox22.Value = "So sánh \r\nthực hiện từ đầu năm với dự toán năm (%)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.03878021240234375D), Telerik.Reporting.Drawing.Unit.Inch(0.29992136359214783D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(9.6541337966918945D), Telerik.Reporting.Drawing.Unit.Inch(0.29999995231628418D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Times New Roman";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "BÁO CÁO TỔNG HỢP THU NGÂN SÁCH XÃ THEO NỘI DUNG KINH TẾ";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(9.6541337966918945D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Times New Roman";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Tháng {Parameters.KYBC.Value} năm {Parameters.NAMBC.Value}";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.18541653454303742D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Italic = false;
            this.textBox3.Style.Font.Name = "Times New Roman";
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox3.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "(1) Chỉ áp dụng đối với cấp ngân sách xã, thị trấn";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.6000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.88333338499069214D));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox40.Style.Font.Bold = false;
            this.textBox40.Style.Font.Italic = true;
            this.textBox40.Style.Font.Name = "Times New Roman";
            this.textBox40.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox40.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox40.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox40.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox40.Value = "(Ký, họ tên, đóng dấu)";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.5625D), Telerik.Reporting.Drawing.Unit.Inch(0.88333338499069214D));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox39.Style.Font.Bold = false;
            this.textBox39.Style.Font.Italic = true;
            this.textBox39.Style.Font.Name = "Times New Roman";
            this.textBox39.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox39.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox39.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox39.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox39.Value = "(Ký, họ tên)";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.7041668891906738D), Telerik.Reporting.Drawing.Unit.Inch(0.66458338499069214D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421D), Telerik.Reporting.Drawing.Unit.Inch(0.19992128014564514D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Times New Roman";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox4.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Chủ tịch UBND xã";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.4000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.46450471878051758D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(0.19999979436397553D));
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Italic = true;
            this.textBox13.Style.Font.Name = "Times New Roman";
            this.textBox13.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox13.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox13.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "...., Ngày .... Tháng .... Năm ....";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.46875D), Telerik.Reporting.Drawing.Unit.Inch(0.581250011920929D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5000003576278687D), Telerik.Reporting.Drawing.Unit.Inch(0.299999862909317D));
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Name = "Times New Roman";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox14.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox14.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "Kế toán trưởng";
            // 
            // PHB_C_B02A_X
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detailSection1,
            this.pageHeaderSection1,
            this.pageFooterSection1});
            this.Name = "PHB_C_B02A_X";
            this.PageSettings.ContinuousPaper = false;
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "MADBHC";
            reportParameter1.Text = "MADBHC";
            reportParameter1.Value = "256";
            reportParameter2.AllowNull = true;
            reportParameter2.Name = "MACHUONG";
            reportParameter2.Text = "MACHUONG";
            reportParameter2.Value = "421";
            reportParameter3.AllowNull = true;
            reportParameter3.Name = "NAMBC";
            reportParameter3.Text = "NAMBC";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Value = "2017";
            reportParameter4.AllowNull = true;
            reportParameter4.Name = "KYBC";
            reportParameter4.Text = "KYBC";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter4.Value = "0";
            reportParameter5.AllowNull = true;
            reportParameter5.Name = "LOAIBC";
            reportParameter5.Text = "LOAIBC";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter6.AllowNull = true;
            reportParameter6.Name = "DSDVQHNS";
            reportParameter6.Text = "DSDVQHNS";
            reportParameter6.Value = "1019099";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.Table), "Normal.TableNormal")});
            styleRule2.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableBody")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableHeader")});
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule4.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(9.6928749084472656D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detailSection1;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox3;
    }
}