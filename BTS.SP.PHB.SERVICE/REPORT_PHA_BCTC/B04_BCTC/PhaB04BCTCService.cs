using BTS.SP.PHB.SERVICE.SERVICES;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B04_BCTC;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B04_BCTC
{
    public interface IPhaB04BCTCService : IBaseService<PHA_B04_BCTC>
    {
        void UploadXml(XDocument document, string nguoiTao, string maDonVi, string capDuToan, int nam, DateTime ngayTao);

        void ExportXml(XmlWriter writer, string maDonVi, int nam);
    }

    public class PhaB04BCTCService : BaseService<PHA_B04_BCTC>, IPhaB04BCTCService
    {
        private readonly IRepositoryAsync<PHA_B04_BCTC> _reportRepo;
        private readonly IRepositoryAsync<PHA_B04_BCTC_TEMPLATE> _templateRepo;
        private readonly IRepositoryAsync<PHA_B04_BCTC_DETAIL> _detailRepo;

        private const string REPORT_ELEMENT_NAME = "TMBCTC";
        private const string PREFIX_CONTENT_ELEMENT_NAME = "CT";
        
        private const string COL_SO_CUOI_NAM = "CN";
        private const string COL_SO_DAU_NAM = "DN";
        private const string COL_TONG_CONG = "TC";
        private const string COL_TSCD_HUU_HINH = "HH";
        private const string COL_TSCD_VO_HINH = "VH";
        private const string COL_NGUON_VON_KD = "KD";
        private const string COL_CHENH_LECH_TY_GIA = "CL";
        private const string COL_THANG_DU_LUY_KE = "TD";
        private const string COL_CAC_QUY = "QU";
        private const string COL_CAI_CACH_TIEN_LUONG = "TL";
        private const string COL_KHAC = "KC";
        private const string COL_CONG = "CG";
        private const string COL_NAM_NAY = "NN";
        private const string COL_NAM_TRUOC = "NT";

        public PhaB04BCTCService(
            IRepositoryAsync<PHA_B04_BCTC> repository, 
            IRepositoryAsync<PHA_B04_BCTC_TEMPLATE> templateRepo,
            IRepositoryAsync<PHA_B04_BCTC_DETAIL> detailRepo) : base(repository)
        {
            _reportRepo = repository;
            _templateRepo = templateRepo;
            _detailRepo = detailRepo;
        }

        /// <summary>
        /// add report, detail, but have not save to database yet.
        /// use unitOfWork.SaveChange() to save
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="nguoiTao"></param>
        /// <param name="maDonVi"></param>
        /// <param name="capDuToan"></param>
        /// <param name="nam"></param>
        public void UploadXml(XDocument document, string nguoiTao, string maDonVi, string capDuToan, int nam, DateTime ngayTao)
        {
            //get xml report element
            var reportXml = document.Descendants(REPORT_ELEMENT_NAME).FirstOrDefault();
            if (reportXml == null || reportXml.Value == null || reportXml.Value.Trim() == "")
            {
                //if does not have report or content of report empy
                //return
                return;
            }

            //add new report
            var report = new PHA_B04_BCTC
            {
                NGAY_TAO = ngayTao,
                NGUOI_TAO = nguoiTao,
                MA_DONVI = maDonVi,
                CAP_DU_TOAN = capDuToan,
                NAM = nam,

                REFID = Guid.NewGuid().ToString()
            };

            //check đã có báo cáo chưa
            var reportCount = _reportRepo.Queryable()
                .Where(rpt => rpt.MA_DONVI == report.MA_DONVI && rpt.NAM == report.NAM)
                .Count();

            if (reportCount > 0)
            {
                throw new Exception($"Đã tồn tại báo cáo B04_BCTC năm {report.NAM}");
            }

            try
            {
                _reportRepo.Insert(report);
            }
            catch (Exception)
            {
                throw;
            }

            //get template
            var lstTemplate = new List<PHA_B04_BCTC_TEMPLATE>();
            try
            {
                lstTemplate = _templateRepo.Queryable().ToList();
            }
            catch (Exception)
            {
                throw;
            }

            //add detail
            foreach (var template in lstTemplate)
            {
                //get ma_so
                var maSo = template.MA_SO_IMPORT_XML_BCTC_107_GT;

                //detail
                var detail = new PHA_B04_BCTC_DETAIL
                {
                    PHA_B04_BCTC_REFID = report.REFID,
                    STT_SAPXEP = template.STT_SAPXEP,
                    STT = template.STT,
                    CHI_TIEU = template.CHI_TIEU,
                    MA_SO = template.MA_SO,
                    MA_SO_CHA = template.MA_SO_CHA,
                    IS_BOLD = template.IS_BOLD,
                    IS_ITALIC = template.IS_ITALIC
                };

                //get data from xml 
                if (maSo != null && maSo.Trim() != "")
                {
                    //join string to get element name
                    var elementName_SoCuoiNam = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_SO_CUOI_NAM;
                    var elementName_SoDauNam = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_SO_DAU_NAM;
                    var elementName_TongCong = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TONG_CONG;
                    var elementName_HuuHinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TSCD_HUU_HINH;
                    var elementName_VoHinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TSCD_VO_HINH;
                    var elementName_KinhDoanh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NGUON_VON_KD;
                    var elementName_TyGia = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_CHENH_LECH_TY_GIA;
                    var elementName_LuyKe = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_THANG_DU_LUY_KE;
                    var elementName_Quy= PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_CAC_QUY;
                    var elementName_TienLuong= PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_CAI_CACH_TIEN_LUONG;
                    var elementName_Khac = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_KHAC;
                    var elementName_Cong = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_CONG;
                    var elementName_NamNay = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_NAY;
                    var elementName_NamTruoc = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_TRUOC;

                    //query element
                    var soCuoiNam = reportXml.Descendants(elementName_SoCuoiNam).FirstOrDefault();
                    var soDauNam = reportXml.Descendants(elementName_SoDauNam).FirstOrDefault();
                    var tongCong = reportXml.Descendants(elementName_TongCong).FirstOrDefault();
                    var huuHinh = reportXml.Descendants(elementName_HuuHinh).FirstOrDefault();
                    var voHinh = reportXml.Descendants(elementName_VoHinh).FirstOrDefault();
                    var kinhDoanh = reportXml.Descendants(elementName_KinhDoanh).FirstOrDefault();
                    var tyGia = reportXml.Descendants(elementName_TyGia).FirstOrDefault();
                    var luyKe = reportXml.Descendants(elementName_LuyKe).FirstOrDefault();
                    var quy = reportXml.Descendants(elementName_Quy).FirstOrDefault();
                    var tienLuong = reportXml.Descendants(elementName_TienLuong).FirstOrDefault();
                    var khac = reportXml.Descendants(elementName_Khac).FirstOrDefault();
                    var cong = reportXml.Descendants(elementName_Cong).FirstOrDefault();
                    var namNay = reportXml.Descendants(elementName_NamNay).FirstOrDefault();
                    var namTruoc = reportXml.Descendants(elementName_NamTruoc).FirstOrDefault();

                    decimal? soCuoiNamVal = null;
                    decimal? soDauNamVal = null;
                    decimal? tongCongVal = null;
                    decimal? huuHinhVal = null;
                    decimal? voHinhVal = null;
                    decimal? kinhDoanhVal = null;
                    decimal? tyGiaVal = null;
                    decimal? luyKeVal = null;
                    decimal? quyVal = null;
                    decimal? tienLuongVal = null;
                    decimal? khacVal = null;
                    decimal? congVal = null;
                    decimal? namNayVal = null;
                    decimal? namTruocVal = null;

                    try
                    {
                        soCuoiNamVal = soCuoiNam == null ? (decimal?)null : decimal.Parse(soCuoiNam.Value);
                        soDauNamVal = soDauNam == null ? (decimal?)null : decimal.Parse(soDauNam.Value);
                        tongCongVal = tongCong == null ? (decimal?)null : decimal.Parse(tongCong.Value);
                        huuHinhVal = huuHinh == null ? (decimal?)null : decimal.Parse(huuHinh.Value);
                        voHinhVal = voHinh == null ? (decimal?)null : decimal.Parse(voHinh.Value);
                        kinhDoanhVal = kinhDoanh == null ? (decimal?)null : decimal.Parse(kinhDoanh.Value);
                        tyGiaVal = tyGia == null ? (decimal?)null : decimal.Parse(tyGia.Value);
                        luyKeVal = luyKe == null ? (decimal?)null : decimal.Parse(luyKe.Value);
                        quyVal = quy == null ? (decimal?)null : decimal.Parse(quy.Value);
                        tienLuongVal = tienLuong == null ? (decimal?)null : decimal.Parse(tienLuong.Value);
                        khacVal = khac == null ? (decimal?)null : decimal.Parse(khac.Value);
                        congVal = cong == null ? (decimal?)null : decimal.Parse(cong.Value);
                        namNayVal = namNay == null ? (decimal?)null : decimal.Parse(namNay.Value);
                        namTruocVal = namTruoc == null ? (decimal?)null : decimal.Parse(namTruoc.Value);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException();
                    }
                    
                    detail.SO_CUOI_NAM = soCuoiNamVal;
                    detail.SO_DAU_NAM = soDauNamVal;
                    detail.TONG_CONG = tongCongVal;
                    detail.TSCD_HUU_HINH = huuHinhVal;
                    detail.TSCD_VO_HINH = voHinhVal;
                    detail.NGUON_VON_KD = kinhDoanhVal;
                    detail.CHENH_LECH_TY_GIA = tyGiaVal;
                    detail.THANG_DU_LUY_KE = luyKeVal;
                    detail.CAC_QUY = quyVal;
                    detail.CAI_CACH_TIEN_LUON = tienLuongVal;
                    detail.KHAC = khacVal;
                    detail.CONG = congVal;
                    detail.NAM_NAY = namNayVal;
                    detail.NAM_TRUOC = namTruocVal;
                }

                //insert detail
                try
                {
                    _detailRepo.Insert(detail);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void ExportXml(XmlWriter writer, string maDonVi, int nam)
        {
            if (maDonVi == null || maDonVi.Trim() == "")
            {
                throw new ArgumentNullException();
            }

            //get report by maDonVi and nam
            var report = new PHA_B04_BCTC();
            var lstTemplate = new List<PHA_B04_BCTC_TEMPLATE>();
            var lstDetail = new List<PHA_B04_BCTC_DETAIL>();
            try
            {
                report = _reportRepo.Queryable()
                    .Where(rpt => rpt.MA_DONVI == maDonVi && rpt.NAM == nam)
                    .FirstOrDefault();
                lstTemplate = _templateRepo.Queryable()
                    .OrderBy(template => template.STT_SAPXEP)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            if (report == null)
            {
                writer.WriteStartElement(REPORT_ELEMENT_NAME);
                writer.WriteEndElement();
                return;
            }

            try
            {
                lstDetail = _detailRepo.Queryable()
                    .Where(detail => detail.PHA_B04_BCTC_REFID == report.REFID)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            //get list of parent 2
            //parent 2 is parent of parent 1
            var lstParent2 = lstTemplate
                .Where(tpl => tpl.XML_PARENT_FIELD_NAME_2 != null)
                .Select(tpl => tpl.XML_PARENT_FIELD_NAME_2)
                .Distinct()
                .ToList();

            //write <THTC>
            writer.WriteStartElement(REPORT_ELEMENT_NAME);

            //write element
            foreach (var parent2 in lstParent2)
            {

                //get list of parent 1
                var lstParent1 = lstTemplate
                    .Where(tpl => tpl.XML_PARENT_FIELD_NAME_2 != null && tpl.XML_PARENT_FIELD_NAME_2.Trim() == parent2)
                    .Where(tpl => tpl.XML_PARENT_FIELD_NAME_1 != null)
                    .Select(tpl => tpl.XML_PARENT_FIELD_NAME_1)
                    .Distinct()
                    .ToList();

                //write parent element such as <TMLCTT>
                writer.WriteStartElement(parent2);

                foreach(var parent1 in lstParent1)
                {
                    if(parent1.Trim() != "")
                    {
                        writer.WriteStartElement(parent1);
                    }

                    //select list template2 of current template1
                    var lstTpl = lstTemplate
                        .Where(tpl => tpl.XML_PARENT_FIELD_NAME_1 != null && tpl.XML_PARENT_FIELD_NAME_1 == parent1
                            && tpl.XML_PARENT_FIELD_NAME_2 != null && tpl.XML_PARENT_FIELD_NAME_2.Trim() == parent2)
                        .ToList();

                    //loop and write each element
                    foreach (var tpl in lstTpl)
                    {
                        var maSo = tpl.MA_SO;
                        if (maSo != null && maSo.Trim() != "")
                        {
                            var elementName_SoCuoiNam = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_SO_CUOI_NAM;
                            var elementName_SoDauNam = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_SO_DAU_NAM;
                            var elementName_TongCong = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TONG_CONG;
                            var elementName_HuuHinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TSCD_HUU_HINH;
                            var elementName_VoHinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TSCD_VO_HINH;
                            var elementName_KinhDoanh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NGUON_VON_KD;
                            var elementName_TyGia = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_CHENH_LECH_TY_GIA;
                            var elementName_LuyKe = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_THANG_DU_LUY_KE;
                            var elementName_Quy = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_CAC_QUY;
                            var elementName_TienLuong = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_CAI_CACH_TIEN_LUONG;
                            var elementName_Khac = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_KHAC;
                            var elementName_Cong = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_CONG;
                            var elementName_NamNay = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_NAY;
                            var elementName_NamTruoc = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_TRUOC;

                            var detail = lstDetail.Where(dt => dt.MA_SO != null && dt.MA_SO.Trim() == maSo).FirstOrDefault();
                            
                            var soCuoiNamVal = detail == null ? 0 : detail.SO_CUOI_NAM;
                            var soCuoiDauVal = detail == null ? 0 : detail.SO_DAU_NAM;
                            var elementName_TongCongVal = detail == null ? 0 : detail.TONG_CONG;
                            var elementName_HuuHinhVal = detail == null ? 0 : detail.TSCD_HUU_HINH;
                            var elementName_VoHinhVal = detail == null ? 0 : detail.TSCD_VO_HINH;
                            var elementName_KinhDoanhVal = detail == null ? 0 : detail.NGUON_VON_KD;
                            var elementName_TyGiaVal = detail == null ? 0 : detail.CHENH_LECH_TY_GIA;
                            var elementName_LuyKeVal = detail == null ? 0 : detail.THANG_DU_LUY_KE;
                            var elementName_QuyVal = detail == null ? 0 : detail.CAC_QUY;
                            var elementName_TienLuongVal = detail == null ? 0 : detail.CAI_CACH_TIEN_LUON;
                            var elementName_KhacVal = detail == null ? 0 : detail.KHAC;
                            var elementName_CongVal = detail == null ? 0 : detail.CONG;
                            var elementName_NamNayVal = detail == null ? 0 : detail.NAM_NAY;
                            var elementName_NamTruocVal = detail == null ? 0 : detail.NAM_TRUOC;

                            if(tpl.IS_INCLUDED_SO_CUOI_NAM == 1)
                            {
                                writer.WriteElementString(elementName_SoCuoiNam, soCuoiNamVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_SO_DAU_NAM == 1)
                            {
                                writer.WriteElementString(elementName_SoDauNam, soCuoiDauVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_TONG_CONG == 1)
                            {
                                writer.WriteElementString(elementName_TongCong, elementName_TongCongVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_TSCD_HUU_HINH == 1)
                            {
                                writer.WriteElementString(elementName_HuuHinh, elementName_HuuHinhVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_TSCD_VO_HINH == 1)
                            {
                                writer.WriteElementString(elementName_VoHinh, elementName_VoHinhVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_NGUON_VON_KD == 1)
                            {
                                writer.WriteElementString(elementName_KinhDoanh, elementName_KinhDoanhVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_CHENH_LECH_TY_GIA == 1)
                            {
                                writer.WriteElementString(elementName_TyGia, elementName_TyGiaVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_THANG_DU_LUY_KE == 1)
                            {
                                writer.WriteElementString(elementName_LuyKe, elementName_LuyKeVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_CAC_QUY == 1)
                            {
                                writer.WriteElementString(elementName_Quy, elementName_QuyVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_CAI_CACH_TIEN_LUON == 1)
                            {
                                writer.WriteElementString(elementName_TienLuong, elementName_TienLuongVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_KHAC == 1)
                            {
                                writer.WriteElementString(elementName_Khac, elementName_KhacVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_CONG == 1)
                            {
                                writer.WriteElementString(elementName_Cong, elementName_CongVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_NAM_NAY == 1)
                            {
                                writer.WriteElementString(elementName_NamNay, elementName_NamNayVal.ToString());
                            }
                            if (tpl.IS_INCLUDED_NAM_TRUOC == 1)
                            {
                                writer.WriteElementString(elementName_NamTruoc, elementName_NamTruocVal.ToString());
                            }
                        }
                    }

                    if (parent1.Trim() != "")
                    {
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
            }

            //write </THTC>
            writer.WriteEndElement();

        }
    }

}
