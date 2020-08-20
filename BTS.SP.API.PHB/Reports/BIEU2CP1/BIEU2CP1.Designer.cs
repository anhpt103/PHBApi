namespace BTS.SP.API.PHB.Reports.BIEU2CP1
{
    partial class BIEU2CP1
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.FormattingRule formattingRule2 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup6 = new Telerik.Reporting.TableGroup();
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
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector3 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox99 = new Telerik.Reporting.TextBox();
            this.textBox98 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.crosstab1 = new Telerik.Reporting.Crosstab();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.95625036954879761D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.StyleName = "Normal.TableGroup";
            this.textBox2.Value = "Khoản {Fields.MA_KHOAN}";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9562504291534424D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.StyleName = "Normal.TableGroup";
            this.textBox1.Value = "Loại {Fields.MA_LOAI}";
            // 
            // textBox4
            // 
            formattingRule1.Filters.Add(new Telerik.Reporting.Filter("= Fields.IS_BOLD", Telerik.Reporting.FilterOperator.Equal, "1"));
            formattingRule1.Style.Font.Bold = true;
            formattingRule2.Filters.Add(new Telerik.Reporting.Filter("= Fields.IS_ITALIC", Telerik.Reporting.FilterOperator.Equal, "1"));
            formattingRule2.Style.Font.Italic = true;
            this.textBox4.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1,
            formattingRule2});
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2062497138977051D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox4.StyleName = "Normal.TableGroup";
            this.textBox4.Value = "= Fields.TEN_CHI_TIEU";
            // 
            // textBox3
            // 
            this.textBox3.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1,
            formattingRule2});
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60208326578140259D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.StyleName = "Normal.TableGroup";
            this.textBox3.Value = "= Fields.STT_CHI_TIEU";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox99,
            this.textBox98,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox99
            // 
            this.textBox99.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9802360534667969D), Telerik.Reporting.Drawing.Unit.Inch(3.9378803194267675E-05D));
            this.textBox99.Name = "textBox99";
            this.textBox99.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.19996058940887451D));
            this.textBox99.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox99.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox99.Style.Font.Bold = true;
            this.textBox99.Style.Font.Name = "Times New Roman";
            this.textBox99.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBox99.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox99.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox99.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox99.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox99.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox99.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox99.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox99.Value = "Mẫu biểu 2c";
            // 
            // textBox98
            // 
            this.textBox98.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.20007865130901337D));
            this.textBox98.Name = "textBox98";
            this.textBox98.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.4803147315979D), Telerik.Reporting.Drawing.Unit.Inch(0.49996078014373779D));
            this.textBox98.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox98.Style.Font.Bold = true;
            this.textBox98.Style.Font.Name = "Times New Roman";
            this.textBox98.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox98.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox98.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox98.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox98.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox98.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox98.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox98.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox98.Value = "SỐ LIỆU XÉT DUYỆT (HOẶC THẨM ĐỊNH)\r\nQUYẾT TOÁN CHI NGÂN SÁCH NĂM ….";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.699999988079071D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.4803147315979D), Telerik.Reporting.Drawing.Unit.Inch(0.29988196492195129D));
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Times New Roman";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(14D);
            this.textBox8.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox8.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox8.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox8.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "ĐƠN VỊ:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.999960720539093D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.4802360534667969D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox9.Style.Font.Italic = true;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Value = "(Kèm theo Thông báo xét duyệt (hoặc thẩm định) quyết toán số …../….. ngày …/…/….)" +
    "";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.2802367210388184D), Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999994516372681D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox10.Value = "Đơn vị: Đồng";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60212260484695435D), Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2062499523162842D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Value = "Phần I- TỔNG HỢP TÌNH HÌNH KINH PHÍ:";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(2D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.crosstab1});
            this.detail.Name = "detail";
            // 
            // crosstab1
            // 
            this.crosstab1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0000001192092896D)));
            this.crosstab1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0000001192092896D)));
            this.crosstab1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.956250786781311D)));
            this.crosstab1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D)));
            this.crosstab1.Body.SetCellContent(0, 2, this.textBox7);
            this.crosstab1.Body.SetCellContent(0, 1, this.textBox14);
            this.crosstab1.Body.SetCellContent(0, 0, this.textBox15);
            tableGroup1.Name = "group1";
            tableGroup1.ReportItem = this.textBox13;
            tableGroup3.Name = "group";
            tableGroup3.ReportItem = this.textBox12;
            tableGroup4.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.MA_KHOAN"));
            tableGroup4.Name = "mA_KHOAN";
            tableGroup4.ReportItem = this.textBox2;
            tableGroup4.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.MA_KHOAN", Telerik.Reporting.SortDirection.Asc));
            tableGroup2.ChildGroups.Add(tableGroup3);
            tableGroup2.ChildGroups.Add(tableGroup4);
            tableGroup2.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.MA_LOAI"));
            tableGroup2.Name = "mA_LOAI";
            tableGroup2.ReportItem = this.textBox1;
            tableGroup2.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.MA_LOAI", Telerik.Reporting.SortDirection.Asc));
            this.crosstab1.ColumnGroups.Add(tableGroup1);
            this.crosstab1.ColumnGroups.Add(tableGroup2);
            this.crosstab1.Corner.SetCellContent(0, 0, this.textBox5, 2, 1);
            this.crosstab1.Corner.SetCellContent(0, 1, this.textBox6, 2, 1);
            this.crosstab1.DataSource = this.sqlDataSource1;
            this.crosstab1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox12,
            this.textBox14,
            this.textBox13,
            this.textBox15});
            this.crosstab1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.crosstab1.Name = "crosstab1";
            tableGroup6.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.TEN_CHI_TIEU"));
            tableGroup6.Name = "tEN_CHI_TIEU";
            tableGroup6.ReportItem = this.textBox4;
            tableGroup6.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.TEN_CHI_TIEU", Telerik.Reporting.SortDirection.Asc));
            tableGroup5.ChildGroups.Add(tableGroup6);
            tableGroup5.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.MA_CHI_TIEU"));
            tableGroup5.Name = "mA_CHI_TIEU";
            tableGroup5.ReportItem = this.textBox3;
            tableGroup5.Sortings.Add(new Telerik.Reporting.Sorting("= CInt(Fields.SAP_XEP)", Telerik.Reporting.SortDirection.Asc));
            this.crosstab1.RowGroups.Add(tableGroup5);
            this.crosstab1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.7645840644836426D), Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791D));
            this.crosstab1.StyleName = "Normal.TableNormal";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:N0}";
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.95625036954879761D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.StyleName = "Normal.TableBody";
            this.textBox7.Value = "= Sum(Fields.GIATRI)";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60208326578140259D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.StyleName = "Normal.TableHeader";
            this.textBox5.Value = "Chỉ tiêu";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2062497138977051D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.StyleName = "Normal.TableHeader";
            this.textBox6.Value = "Nội dung\t";
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
            this.sqlDataSource1.SelectCommand = "BTSTC.PHB_BIEU2CP1_SUMREPORT";
            this.sqlDataSource1.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox12
            // 
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.StyleName = "Normal.TableGroup";
            this.textBox12.Value = "Tổng Loại {Fields.MA_LOAI}";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N0}";
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.StyleName = "Normal.TableBody";
            this.textBox14.Value = "= Sum(Fields.GIATRI)";
            // 
            // textBox13
            // 
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.StyleName = "Normal.TableGroup";
            this.textBox13.Value = "Tổng số";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:N0}";
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.StyleName = "Normal.TableBody";
            this.textBox15.Value = "= Sum(Fields.GIATRI)";
            // 
            // BIEU2CP1
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "BIEU2CP1";
            this.PageSettings.ContinuousPaper = false;
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "USER_NAME";
            reportParameter1.Text = "USER_NAME";
            reportParameter1.Value = "quantri27";
            reportParameter2.AllowNull = true;
            reportParameter2.Name = "LOAIBC";
            reportParameter2.Text = "LOAIBC";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter2.Value = "1";
            reportParameter3.AllowNull = true;
            reportParameter3.Name = "CHITIET";
            reportParameter3.Text = "CHITIET";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter3.Value = "1";
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
            descendantSelector3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableGroup")});
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector3});
            styleRule5.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule5.Style.Font.Name = "Tahoma";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.4803147315979D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.Crosstab crosstab1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox99;
        private Telerik.Reporting.TextBox textBox98;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
    }
}