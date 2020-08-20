using System;
using System.IO;
using BTS.SP.PHB.ENTITY.Rp.DOICHIEUSOLIEU;
using BTS.SP.PHB.SERVICE.Models.DOICHIEUSOLIEU;
using BTS.SP.PHB.SERVICE.SERVICES;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.DOICHIEUSOLIEU
{
    public interface IPhbDoiChieuSoLieuService : IBaseService<PHB_DOICHIEUSOLIEU>
    {
        string CreateTemplate(string fileName, string mapPath, DOICHIEUSOLIEUVm.TemplateExcel dataTemplate);
    }
    public class PhbDoiChieuSoLieuService : BaseService<PHB_DOICHIEUSOLIEU>, IPhbDoiChieuSoLieuService
    {
        private readonly IRepositoryAsync<PHB_DOICHIEUSOLIEU> _repository;
        public PhbDoiChieuSoLieuService(IRepositoryAsync<PHB_DOICHIEUSOLIEU> repository) : base(repository)
        {
            _repository = repository;
        }

        public string CreateTemplate(string fileName, string mapPath, DOICHIEUSOLIEUVm.TemplateExcel dataTemplate)
        {
            var temp = new DOICHIEUSOLIEUVm.TemplateExcel();
            string filename = "";
            new FileInfo(mapPath).Directory.Create();
            filename = mapPath + "TEMPLATE_DOICHIEU_" + fileName + "_(" + DateTime.Now.ToString("dd-MM-yyyy") + ")" + DateTime.Now.Ticks + ".xlsx";
            FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "SystemAccount";
                excelPackage.Workbook.Properties.Title = "TEMPLATE_DOICHIEU";
                excelPackage.Workbook.Worksheets.Add(DateTime.Now.ToString("dd-MM-yyyy"));
                var workSheet = excelPackage.Workbook.Worksheets[1];
                BindingDataBienLaiThuToExcel(workSheet, temp);
                excelPackage.SaveAs(file);
                file.Close();
            }
            return filename;
        }

        public void BindingDataBienLaiThuToExcel(ExcelWorksheet ws, DOICHIEUSOLIEUVm.TemplateExcel dataTemplate)
        {
            ws.Cells[1, 1, 1, 8].Merge = true;
            ws.Cells[1, 1, 1, 8].Value = "Loại dữ liệu(*): ";
            ws.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells[2, 1, 2, 8].Merge = true;
            ws.Cells[2, 1, 2, 8].Value = "Năm(*): ";
            ws.Cells[2, 1, 2, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells[3, 1, 3, 8].Merge = true;
            ws.Cells[3, 1, 3, 8].Value = "Cấp dự toán(*): ";
            ws.Cells[3, 1, 3, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells[4, 1, 4, 8].Merge = true;
            ws.Cells[4, 1, 4, 8].Value = "MẪU ĐỐI CHIẾU SỐ LIỆU NGÂN SÁCH";
            ws.Cells[4, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[4, 1, 4, 8].Style.Font.Bold = true;
            ws.Cells[5, 1, 5, 8].Merge = true;
            ws.Cells[5, 1, 5, 8].Style.Font.Size = 14;
            ws.Cells[5, 1, 5, 8].Style.Font.Bold = true;
            ws.Cells[5, 1, 5, 8].Value = string.Format("Ngày " + DateTime.Now.ToString("dd/MM/yyyy"));
            ws.Cells[5, 1, 5, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[6, 1, 6, 8].Merge = true;
            var NumOfRow = (9 + 1).ToString();
            string modelRange = "A1:D" + NumOfRow;
            var modelTable = ws.Cells[modelRange];
            // Assign borders
            modelTable.Style.Font.Name = "Time New Roman";
            ws.Cells[7, 1, 7, 2].Merge = true;
            ws.Cells[7, 1, 7, 2].Value = "Mã đơn vị(*)";
            ws.Cells[7, 1, 7, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[7, 1, 7, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            ws.Cells[7, 3, 7, 4].Merge = true;
            ws.Cells[7, 3, 7, 4].Value = "Số đề nghị (đơn vị)(*)";
            ws.Cells[7, 3, 7, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[7, 3, 7, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            ws.Cells[7, 5, 7, 6].Merge = true;
            ws.Cells[7, 5, 7, 6].Value = "Số thực thi (TABMIS)";
            ws.Cells[7, 5, 7, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[7, 5, 7, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            ws.Cells[7, 7, 7, 8].Merge = true;
            ws.Cells[7, 7, 7, 8].Value = "Chênh lệch";
            ws.Cells[7, 7, 7, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[7, 7, 7, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            ws.Cells[7, 1, 7, 8].Style.Font.Bold = true;
            int startColumn = 1;
            int currentRow = 8;
            for (int i = 0; i < 10; i++)
            {
                ws.Cells[currentRow, startColumn, currentRow, startColumn + 1].Merge = true;
                ws.Cells[currentRow, startColumn, currentRow, startColumn + 1].Value = "";
                ws.Cells[currentRow, startColumn + 2, currentRow, startColumn + 3].Merge = true;
                ws.Cells[currentRow, startColumn + 2, currentRow, startColumn + 3].Value = "";
                ws.Cells[currentRow, startColumn + 4, currentRow, startColumn + 5].Merge = true;
                ws.Cells[currentRow, startColumn + 4, currentRow, startColumn + 5].Value = "";
                ws.Cells[currentRow, startColumn + 4, currentRow, startColumn + 5].Style.Numberformat.Format =
                    "#,##0.00";
                ws.Cells[currentRow, startColumn + 6, currentRow, startColumn + 7].Merge = true;
                ws.Cells[currentRow, startColumn + 6, currentRow, startColumn + 7].Value = "";
                ws.Cells[currentRow, startColumn + 6, currentRow, startColumn + 7].Style.Numberformat.Format =
                    "#,##0.00";
                ws.Cells[currentRow, 1, currentRow, startColumn + 7].Style.Border.BorderAround(ExcelBorderStyle.Dotted);
                currentRow++;
            }
            ws.Column(1).AutoFit();
            ws.Column(2).AutoFit();
            ws.Column(3).AutoFit();
            ws.Column(4).AutoFit();
            ws.Column(5).AutoFit();
            ws.Column(6).AutoFit();
            ws.Column(7).AutoFit();
            ws.Column(8).AutoFit();
        }
    }
}
