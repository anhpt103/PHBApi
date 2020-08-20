using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTS.SP.API.PHB.Controllers
{
    public class test
    {
    //    public void _BindingDataToExcel1C(ExcelWorksheet ws, List<PHA_HACHTOAN_CHI> result, List<PHA_HACHTOAN_CHI> result1, List<DM_CHITIEU_BAOCAO> items, ExportParams para)
    //    {
    //        var starRow = 13;
    //        var starCol = 4;
    //        var STT = 1;
    //        var nextSTT = 0;
    //        var khoanCount = 0;
    //        //Ghi STT và Tên Chỉ Tiêu
    //        for (int i = 0; i < items.Count; i++)
    //        {
    //            ws.Cells[starRow + i, 1].Value = items[i].SAPXEP;
    //            ws.Cells[starRow + i, 2].Value = items[i].TEN_CHITIEU;
    //            ws.Cells[starRow + i, 3].Value = items[i].MA_DONG;
    //            ws.Cells[starRow + i, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //            if (items[i].INDAM == 1)
    //            {
    //                ws.Cells[starRow + i, 1].Style.Font.Bold = true;
    //                ws.Cells[starRow + i, 2].Style.Font.Bold = true;
    //            }
    //            if (items[i].INNGHIENG == 1)
    //            {
    //                ws.Cells[starRow + i, 1].Style.Font.Italic = true;
    //                ws.Cells[starRow + i, 1].Style.Font.Italic = true;
    //            }

    //        }
    //        //Thêm Năm Nay
    //        //Thêm Động loại
    //        var loaitmp = result.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
    //        var loaiS = loaitmp.Select(x => x.MA_LOAI).ToList();



    //        for (int j = 0; j < loaiS.Count; j++)
    //        {
    //            var khoanS = result.Where(x => x.MA_LOAI == loaiS[j]).Select(y => y.MA_NGANHKT).Distinct().ToList();
    //            // Thêm mã dòng loại

    //            ws.Cells[10, starCol, 10, starCol + khoanS.Count].Merge = true;
    //            ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.Font.Bold = true;
    //            ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.WrapText = true;
    //            ws.Cells[10, starCol, 10, starCol + khoanS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //            ws.Cells[10, starCol].Value = "LOẠI " + loaiS[j];
    //            ws.Cells[11, starCol].Merge = true;
    //            ws.Cells[11, starCol].Style.Font.Bold = true;
    //            ws.Cells[11, starCol].Style.WrapText = true;
    //            ws.Cells[11, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //            ws.Cells[11, starCol].Value = "TỔNG SỐ ";
    //            ws.Cells[12, starCol].Value = STT;
    //            ws.Cells[12, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

    //            //Thêm Tiền Tổng Loại
    //            for (int i = 0; i < items.Count; i++)
    //            {
    //                if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
    //                {
    //                    ws.Cells[starRow + i, starCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
    //                    ws.Cells[starRow + i, starCol].Style.Numberformat.Format = "###,###,###,###,###";
    //                }
    //            }

    //            //Thêm Khoản
    //            for (int k = 0; k < khoanS.Count; k++)
    //            {
    //                var nextCol = starCol + k + 1;
    //                ws.Cells[11, nextCol].Value = "KHOẢN " + khoanS[k];
    //                ws.Cells[11, nextCol].Style.Font.Bold = true;
    //                ws.Cells[11, nextCol].Style.WrapText = true;
    //                ws.Cells[11, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //                ws.Cells[12, nextCol].Value = STT + khoanS.Count;
    //                for (int i = 0; i < items.Count; i++)
    //                {
    //                    if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
    //                    {
    //                        ws.Cells[starRow + i, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_NGANHKT == khoanS[k] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
    //                        ws.Cells[starRow + i, starCol].Style.Numberformat.Format = "###,###,###,###,###";
    //                    }
    //                }
    //            }
    //            //Gán số cột bằng số cột hiện tại
    //            starCol += khoanS.Count + 1;
    //            STT += khoanS.Count + 1;
    //            nextSTT = STT;
    //            khoanCount += khoanS.Count;
    //        }

    //        ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Merge = true;
    //        ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Value = "Năm Nay";
    //        ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.Font.Bold = true;
    //        ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.WrapText = true;
    //        ws.Cells[9, 4, 9, 4 + loaiS.Count + khoanCount - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

    //        // END Năm Nay

    //        var dk = "Điều kiện lọc:";
    //        if (!string.IsNullOrEmpty(para.MA_CAP))
    //        {
    //            dk += "Cấp" + para.MA_CAP + ";";
    //        }
    //        if (!string.IsNullOrEmpty(para.MA_CHUONG))
    //        {
    //            dk += "Chương" + para.MA_CHUONG + ";";
    //        }
    //        if (!string.IsNullOrEmpty(para.MA_LOAI))
    //        {
    //            dk += "Loại" + para.MA_LOAI + ";";
    //        }
    //        if (!string.IsNullOrEmpty(para.MA_NGANHKT))
    //        {
    //            dk = "Khoản" + para.MA_NGANHKT + ";";
    //        }
    //        if (!string.IsNullOrEmpty(para.MA_MUC))
    //        {
    //            dk = "Mục" + para.MA_MUC + ";";
    //        }
    //        if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
    //        {
    //            dk = "Tiểu Mục" + para.MA_TIEUMUC + ";";
    //        }

    //        ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Merge = true;
    //        ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Value = "Luỹ kế từ khi khởi đầu";
    //        ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.Font.Bold = true;
    //        ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.WrapText = true;
    //        ws.Cells[9, starCol, 9, starCol + loaiS.Count + khoanCount].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //        //Thêm Luỹ kế
    //        var loaitmp1 = result1.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
    //        var loaiS1 = loaitmp.Select(x => x.MA_LOAI).ToList();

    //        for (int j = 0; j < loaiS1.Count; j++)
    //        {

    //            var khoanS1 = result1.Where(x => x.MA_LOAI == loaiS1[j]).Select(y => y.MA_NGANHKT).Distinct().ToList();
    //            // Thêm mã dòng loại

    //            ws.Cells[10, starCol, 10, starCol + khoanS1.Count].Merge = true;
    //            ws.Cells[10, starCol, 10, starCol + khoanS1.Count].Style.Font.Bold = true;
    //            ws.Cells[10, starCol, 10, starCol + khoanS1.Count].Style.WrapText = true;
    //            ws.Cells[10, starCol, 10, starCol + khoanS1.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //            ws.Cells[10, starCol].Value = "Loại" + loaiS[j];
    //            ws.Cells[11, starCol].Merge = true;
    //            ws.Cells[11, starCol].Style.Font.Bold = true;
    //            ws.Cells[11, starCol].Style.WrapText = true;
    //            ws.Cells[11, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //            ws.Cells[11, starCol].Value = "Tổng Số";
    //            ws.Cells[12, starCol].Value = STT;
    //            ws.Cells[12, starCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

    //            //Thêm Tiền Tổng Loại
    //            for (int i = 0; i < items.Count; i++)
    //            {
    //                if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
    //                {
    //                    ws.Cells[starRow + i, starCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
    //                    ws.Cells[starRow + i, starCol].Style.Numberformat.Format = "###,###,###,###,###";
    //                }
    //            }

    //            //Thêm Khoản
    //            for (int k = 0; k < khoanS1.Count; k++)
    //            {
    //                var nextCol = starCol + k + 1;
    //                ws.Cells[11, nextCol].Value = "Khoản" + khoanS1[k];
    //                ws.Cells[11, nextCol].Style.Font.Bold = true;
    //                ws.Cells[11, nextCol].Style.WrapText = true;
    //                ws.Cells[11, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    //                ws.Cells[12, nextCol].Value = STT + khoanS1.Count;
    //                for (int i = 0; i < items.Count; i++)
    //                {
    //                    if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
    //                    {
    //                        ws.Cells[starRow + i, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_NGANHKT == khoanS1[k] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
    //                        ws.Cells[starRow + i, starCol].Style.Numberformat.Format = "###,###,###,###,###";
    //                    }
    //                }
    //            }
    //            //Gán số cột bằng số cột hiện tại
    //            starCol += khoanS1.Count + 1;
    //            STT += khoanS1.Count + 1;
    //            nextSTT = STT;
    //            khoanCount += khoanS1.Count;
    //        }


    //        //End Luỹ kế

    //        var DVBAOCAO = _dmChuong.Queryable().FirstOrDefault(x => x.MA_CHUONG == para.MA_CHUONG).TEN_CHUONG;
    //        ws.Cells[1, 1].Value = dk;
    //        ws.Cells[2, 1].Value = "Đơn vị báo cáo :" + DVBAOCAO;
    //        ws.Cells[3, 1].Value = "Từ ngày hiệu lực :" + para.TUNGAY_HIEULUC.ToString("d") + "đến ngày hiệu lực" + para.DENNGAY_HIEULUC.ToString("d");
    //        ws.Cells[4, 1].Value = "Từ ngày kết sổ :" + para.TUNGAY_KETSO.ToString("d") + "đến ngày kết sổ:" + para.DENNGAY_KETSO.ToString("d");

    //        ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
    //        ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
    //        ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
    //        ws.SelectedRange[9, 4, 12, starCol - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

    //        ws.SelectedRange[10, 1, starRow + items.Count - 1, starCol - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
    //        ws.SelectedRange[10, 1, starRow + items.Count - 1, starCol - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
    //        ws.SelectedRange[10, 1, starRow + items.Count - 1, starCol - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
    //        ws.SelectedRange[10, 1, starRow + items.Count - 1, starCol - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
    //    }
    }
}