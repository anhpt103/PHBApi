namespace BTS.SP.API.PHB.Reports.BIEU4A
{
    partial class BIEU4A
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
            Telerik.Reporting.TableGroup tableGroup10 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup11 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule2 = new Telerik.Reporting.Drawing.FormattingRule();
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
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(2D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.table1,
            this.textBox21,
            this.textBox22,
            this.textBox23,
            this.textBox24,
            this.textBox25});
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
            this.sqlDataSource1.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("USER_NAME", System.Data.DbType.String, "= Parameters.USER_NAME.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("LOAIBC", System.Data.DbType.Decimal, "= Parameters.LOAIBC.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("CHITIET", System.Data.DbType.Decimal, "= Parameters.CHITIET.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("NAMBC", System.Data.DbType.Decimal, "= Parameters.NAMBC.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("KYBC", System.Data.DbType.Decimal, "= Parameters.KYBC.Value"),
            new Telerik.Reporting.SqlDataSourceParameter("DSDVQHNS", System.Data.DbType.String, "= Parameters.DSDVQHNS.Value")});
            this.sqlDataSource1.SelectCommand = "BTSTC.PHB_BIEU4A_SUMALLREPORT";
            this.sqlDataSource1.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2676377296447754D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Times New Roman";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox1.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Value = "ĐỐI CHIẾU SỐ LIỆU";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.20007872581481934D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2675986289978027D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Times New Roman";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox2.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox2.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Value = "THU, CHI HOẠT ĐỘNG SỰ NGHIỆP";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.40015736222267151D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2675986289978027D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Times New Roman";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox3.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox3.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox3.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Value = "VÀ HOẠT ĐỘNG SẢN XUẤT KINH DOANH NĂM {Parameters.NAMBC.Value}";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.60023599863052368D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2675986289978027D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Times New Roman";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox4.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox4.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox4.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Value = "ĐƠN VỊ:{Parameters.DSDVQHNS.Value}";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.800314724445343D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2675986289978027D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox5.Style.Font.Italic = true;
            this.textBox5.Style.Font.Name = "Times New Roman";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBox5.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox5.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox5.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Value = "(Kèm theo Thông báo xét duyệt (hoặc thẩm định) quyết toán số …../….. ngày …../….." +
    "/..........)";
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.6313289999961853D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(2.8709125518798828D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.89174431562423706D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.88132768869400024D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.99228453636169434D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox10);
            this.table1.Body.SetCellContent(0, 1, this.textBox11);
            this.table1.Body.SetCellContent(0, 2, this.textBox12);
            this.table1.Body.SetCellContent(0, 3, this.textBox13);
            this.table1.Body.SetCellContent(0, 4, this.textBox20);
            tableGroup2.Name = "group";
            tableGroup2.ReportItem = this.textBox14;
            tableGroup1.ChildGroups.Add(tableGroup2);
            tableGroup1.Name = "mA_CHI_TIEU";
            tableGroup1.ReportItem = this.textBox6;
            tableGroup4.Name = "group1";
            tableGroup4.ReportItem = this.textBox15;
            tableGroup3.ChildGroups.Add(tableGroup4);
            tableGroup3.Name = "tEN_CHI_TIEU";
            tableGroup3.ReportItem = this.textBox7;
            tableGroup6.Name = "group2";
            tableGroup6.ReportItem = this.textBox16;
            tableGroup5.ChildGroups.Add(tableGroup6);
            tableGroup5.Name = "dT_SOBC";
            tableGroup5.ReportItem = this.textBox8;
            tableGroup8.Name = "group3";
            tableGroup8.ReportItem = this.textBox17;
            tableGroup7.ChildGroups.Add(tableGroup8);
            tableGroup7.Name = "tH_SOBC";
            tableGroup7.ReportItem = this.textBox9;
            tableGroup10.Name = "group5";
            tableGroup10.ReportItem = this.textBox19;
            tableGroup9.ChildGroups.Add(tableGroup10);
            tableGroup9.Name = "group4";
            tableGroup9.ReportItem = this.textBox18;
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup3);
            this.table1.ColumnGroups.Add(tableGroup5);
            this.table1.ColumnGroups.Add(tableGroup7);
            this.table1.ColumnGroups.Add(tableGroup9);
            this.table1.DataSource = this.sqlDataSource1;
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox17,
            this.textBox18,
            this.textBox19,
            this.textBox20});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.table1.Name = "table1";
            tableGroup11.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup11.Name = "detail";
            this.table1.RowGroups.Add(tableGroup11);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2675981521606445D), Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791D));
            this.table1.StyleName = "Normal.TableNormal";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.631328284740448D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Times New Roman";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.StyleName = "Normal.TableHeader";
            this.textBox6.Value = "Chỉ tiêu";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8709120750427246D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Times New Roman";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.StyleName = "Normal.TableHeader";
            this.textBox7.Value = "Nội dung";
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89174461364746094D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Times New Roman";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.StyleName = "Normal.TableHeader";
            this.textBox8.Value = "Dự toán";
            // 
            // textBox9
            // 
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.881327748298645D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Times New Roman";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.StyleName = "Normal.TableHeader";
            this.textBox9.Value = "Thực hiện\t\t";
            // 
            // textBox10
            // 
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.631328284740448D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox10.Style.Font.Name = "Times New Roman";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox10.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox10.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox10.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox10.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.StyleName = "Normal.TableBody";
            this.textBox10.Value = "= Fields.MA_CHI_TIEU";
            // 
            // textBox11
            // 
            formattingRule1.Filters.Add(new Telerik.Reporting.Filter("= Fields.IN_DAM", Telerik.Reporting.FilterOperator.Equal, "1"));
            formattingRule1.Style.Font.Bold = true;
            formattingRule2.Filters.Add(new Telerik.Reporting.Filter("= Fields.IN_NGHIENG", Telerik.Reporting.FilterOperator.Equal, "1"));
            formattingRule2.Style.Font.Italic = true;
            formattingRule2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(20D);
            this.textBox11.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1,
            formattingRule2});
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8709120750427246D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox11.Style.Font.Name = "Times New Roman";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox11.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox11.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox11.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.StyleName = "Normal.TableBody";
            this.textBox11.Value = "= Fields.TEN_CHI_TIEU";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:N0}";
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89174461364746094D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox12.Style.Font.Name = "Times New Roman";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox12.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox12.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox12.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.StyleName = "Normal.TableBody";
            this.textBox12.Value = "= Fields.DT_SOBC";
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:N0}";
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.881327748298645D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox13.Style.Font.Name = "Times New Roman";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox13.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox13.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox13.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.StyleName = "Normal.TableBody";
            this.textBox13.Value = "= Fields.TH_SOBC";
            // 
            // textBox14
            // 
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.631328284740448D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Name = "Times New Roman";
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox14.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.StyleName = "";
            this.textBox14.Value = "A";
            // 
            // textBox15
            // 
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8709120750427246D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Times New Roman";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox15.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox15.StyleName = "";
            this.textBox15.Value = "B";
            // 
            // textBox16
            // 
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89174461364746094D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Name = "Times New Roman";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox16.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.StyleName = "";
            this.textBox16.Value = "1";
            // 
            // textBox17
            // 
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.881327748298645D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Times New Roman";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox17.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.StyleName = "";
            this.textBox17.Value = "2";
            // 
            // textBox18
            // 
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99228453636169434D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Times New Roman";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.StyleName = "Normal.TableHeader";
            this.textBox18.Value = "So sánh TH/DT";
            // 
            // textBox19
            // 
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99228453636169434D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox19.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox19.Style.Font.Bold = false;
            this.textBox19.Style.Font.Name = "Times New Roman";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox19.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.StyleName = "";
            this.textBox19.Value = "3=2/1";
            // 
            // textBox20
            // 
            this.textBox20.Format = "{0:N0}";
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99228453636169434D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox20.Style.Font.Name = "Times New Roman";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox20.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox20.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox20.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox20.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(0D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.StyleName = "Normal.TableBody";
            this.textBox20.Value = "= Round(IIf(CDbl(Fields.DT_SOBC) > 0,(CDbl(Fields.TH_SOBC)/ CDbl(Fields.DT_SOBC))" +
    "*100,0))+\'%\'";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.60011833906173706D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2676377296447754D), Telerik.Reporting.Drawing.Unit.Inch(0.19988155364990234D));
            this.textBox21.Style.Font.Name = "Times New Roman";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox21.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox21.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(20D);
            this.textBox21.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox21.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Value = "(*) Nếu chi lớn hơn thu thì ghi số âm dưới hình thức ghi trong ngoặc đơn (…)";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.502241849899292D), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672D));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Times New Roman";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox22.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox22.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(20D);
            this.textBox22.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox22.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.Value = "NGƯỜI LẬP BIỂU";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(1.3000787496566773D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5022811889648438D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox23.Style.Font.Italic = true;
            this.textBox23.Style.Font.Name = "Times New Roman";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBox23.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox23.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(50D);
            this.textBox23.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox23.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.Value = " (Ký, họ và tên)";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3940253257751465D), Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8736124038696289D), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672D));
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Name = "Times New Roman";
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox24.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox24.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox24.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox24.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox24.Value = "THỦ TRƯỞNG ĐƠN VỊ";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3940253257751465D), Telerik.Reporting.Drawing.Unit.Inch(1.3000787496566773D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8736124038696289D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox25.Style.Font.Italic = true;
            this.textBox25.Style.Font.Name = "Times New Roman";
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBox25.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox25.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox25.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox25.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Value = "(Ký, họ tên, đóng dấu)";
            // 
            // BIEU4A
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "BIEU4A";
            this.PageSettings.ContinuousPaper = false;
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "USER_NAME";
            reportParameter1.Text = "USER_NAME";
            reportParameter1.Value = "1004059";
            reportParameter2.AllowNull = true;
            reportParameter2.Name = "LOAIBC";
            reportParameter2.Text = "LOAIBC";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter2.Value = "1";
            reportParameter3.AllowNull = true;
            reportParameter3.Name = "CHITIET";
            reportParameter3.Text = "CHITIET";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter4.AllowNull = true;
            reportParameter4.Name = "NAMBC";
            reportParameter4.Text = "NAMBC";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter4.Value = "2018";
            reportParameter5.AllowNull = true;
            reportParameter5.Name = "KYBC";
            reportParameter5.Text = "KYBC";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter5.Value = "0";
            reportParameter6.AllowNull = true;
            reportParameter6.Name = "DSDVQHNS";
            reportParameter6.Text = "DSDVQHNS";
            reportParameter6.Value = "1004059";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.2676773071289062D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
    }
}