using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BSTT_1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_1
{
    public interface IPhaB01BSTT1Service : IBaseService<PHA_B01_BSTT_1>
    {
        void UploadXml(XDocument document, string nguoiTao, string maDonVi, string capDuToan, int nam, DateTime ngayTao);

        void ExportXml(XmlWriter writer, string maDonVi, int nam);
    }

    public class PhaB01BSTT1Service : BaseService<PHA_B01_BSTT_1>, IPhaB01BSTT1Service
    {
        private readonly IRepositoryAsync<PHA_B01_BSTT_1> _reportRepo;
        private readonly IRepositoryAsync<PHA_B01_BSTT_1_TEMPLATE> _templateRepo;
        private readonly IRepositoryAsync<PHA_B01_BSTT_1_DETAIL> _detailRepo;

        private const string REPORT_ELEMENT_NAME = "TaiChinhTongHop";
        private const string PREFIX_CONTENT_ELEMENT_NAME = "CT";

        private const string COL_TONG_SO = "1";
        private const string COL_TRONG_DVKTTG_2 = "2";
        private const string COL_TRONG_DVKTTG_1 = "3";
        private const string COL_TRONG_DVDT_CAP1 = "4";
        private const string COL_NGOAI_DVDT_CAP1_CUNGTINH = "5";
        private const string COL_NGOAI_DVDT_CAP1_KHACTINH = "6";
        private const string COL_NGOAI_NHA_NUOC = "7";

        public PhaB01BSTT1Service(
            IRepositoryAsync<PHA_B01_BSTT_1> repository,
            IRepositoryAsync<PHA_B01_BSTT_1_TEMPLATE> templateRepo,
            IRepositoryAsync<PHA_B01_BSTT_1_DETAIL> detailRepo) : base(repository)
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
            var report = new PHA_B01_BSTT_1
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
                throw new Exception($"Đã tồn tại báo cáo B01_BSTT_1 năm {report.NAM}");
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
            var lstTemplate = new List<PHA_B01_BSTT_1_TEMPLATE>();
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
                var maSo = template.MA_SO;

                //detail
                var detail = new PHA_B01_BSTT_1_DETAIL
                {
                    PHA_B01_BSTT_1_REFID = report.REFID,
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
                    var elementName_TongSo = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TONG_SO;
                    var elementName_TrongDVKTTG2 = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TRONG_DVKTTG_2;
                    var elementName_TrongDVKTTG1 = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TRONG_DVKTTG_1;
                    var elementName_TrongDVDT = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TRONG_DVDT_CAP1;
                    var elementName_NgoaiDVDT_CungTinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NGOAI_DVDT_CAP1_CUNGTINH;
                    var elementName_NgoaiDVDT_KhacTinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NGOAI_DVDT_CAP1_KHACTINH;
                    var elementName_NgoaiNN = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NGOAI_NHA_NUOC;

                    //query element
                    var tongSo = reportXml.Descendants(elementName_TongSo).FirstOrDefault();
                    var trongDVKTTG2 = reportXml.Descendants(elementName_TrongDVKTTG2).FirstOrDefault();
                    var trongDVKTTG1 = reportXml.Descendants(elementName_TrongDVKTTG1).FirstOrDefault();
                    var trongDVDT = reportXml.Descendants(elementName_TrongDVDT).FirstOrDefault();
                    var ngoaiDVDT_CungTinh = reportXml.Descendants(elementName_NgoaiDVDT_CungTinh).FirstOrDefault();
                    var ngoaiDVDT_KhacTinh = reportXml.Descendants(elementName_NgoaiDVDT_KhacTinh).FirstOrDefault();
                    var ngoaiNN = reportXml.Descendants(elementName_NgoaiNN).FirstOrDefault();
                    
                    decimal? tongSoVal = null;
                    decimal? trongDVKTTG2Val = null;
                    decimal? trongDVKTTG1Val = null;
                    decimal? trongDVDTVal = null;
                    decimal? ngoaiDVDT_CungTinhVal = null;
                    decimal? ngoaiDVDT_KhacTinhVal = null;
                    decimal? ngoaiNNVal = null;

                    try
                    {
                        tongSoVal = tongSo == null ? (decimal?)null : decimal.Parse(tongSo.Value);
                        trongDVKTTG2Val = trongDVKTTG2 == null ? (decimal?)null : decimal.Parse(trongDVKTTG2.Value);
                        trongDVKTTG1Val = trongDVKTTG1 == null ? (decimal?)null : decimal.Parse(trongDVKTTG1.Value);
                        trongDVDTVal = trongDVDT == null ? (decimal?)null : decimal.Parse(trongDVDT.Value);
                        ngoaiDVDT_CungTinhVal = ngoaiDVDT_CungTinh == null ? (decimal?)null : decimal.Parse(ngoaiDVDT_CungTinh.Value);
                        ngoaiDVDT_KhacTinhVal = ngoaiDVDT_KhacTinh == null ? (decimal?)null : decimal.Parse(ngoaiDVDT_KhacTinh.Value);
                        ngoaiNNVal = ngoaiNN == null ? (decimal?)null : decimal.Parse(ngoaiNN.Value);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException();
                    }

                    detail.TONG_SO = tongSoVal;
                    detail.TRONG_DVKTTG_2= trongDVKTTG2Val;
                    detail.TRONG_DVKTTG_1 = trongDVKTTG1Val;
                    detail.TRONG_DVDT_CAP1 = trongDVDTVal;
                    detail.NGOAI_DVDT_CAP1_CUNGTINH = ngoaiDVDT_CungTinhVal;
                    detail.NGOAI_DVDT_CAP1_KHACTINH = ngoaiDVDT_KhacTinhVal;
                    detail.NGOAI_NHA_NUOC = ngoaiNNVal;
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
            var report = new PHA_B01_BSTT_1();
            var lstTemplate = new List<PHA_B01_BSTT_1_TEMPLATE>();
            var lstDetail = new List<PHA_B01_BSTT_1_DETAIL>();
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
                    .Where(detail => detail.PHA_B01_BSTT_1_REFID == report.REFID)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            if (report == null)
            {
                return;
            }

            //write <THTC>
            writer.WriteStartElement(REPORT_ELEMENT_NAME);

            //write element
            var lstMaSo = lstTemplate
                    .Select(tpl => tpl.MA_SO)
                    .ToList();

            //loop and write each element
            foreach (var maSo in lstMaSo)
            {
                if (maSo != null && maSo.Trim() != "")
                {
                    var elementName_TongSo = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_TONG_SO;
                    var elementName_NgoaiDVDT_CungTinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NGOAI_DVDT_CAP1_CUNGTINH;
                    var elementName_NgoaiDVDT_KhacTinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NGOAI_DVDT_CAP1_KHACTINH;
                    var elementName_NgoaiNN = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NGOAI_NHA_NUOC;

                    var detail = lstDetail.Where(dt => dt.MA_SO != null && dt.MA_SO.Trim() == maSo).FirstOrDefault();

                    var tongSoVal = detail == null ? 0 : detail.TONG_SO;
                    var ngoaiDVDT_CungTinhVal = detail == null ? 0 : detail.NGOAI_DVDT_CAP1_CUNGTINH;
                    var ngoaiDVDT_KhacTinhVal = detail == null ? 0 : detail.NGOAI_DVDT_CAP1_KHACTINH;
                    var ngoaiNNVal = detail == null ? 0 : detail.NGOAI_NHA_NUOC;

                    writer.WriteElementString(elementName_TongSo, tongSoVal.ToString());
                    writer.WriteElementString(elementName_NgoaiDVDT_CungTinh, ngoaiDVDT_CungTinhVal.ToString());
                    writer.WriteElementString(elementName_NgoaiDVDT_KhacTinh, ngoaiDVDT_KhacTinhVal.ToString());
                    writer.WriteElementString(elementName_NgoaiNN, ngoaiNNVal.ToString());
                }
            }

            //write </THTC>
            writer.WriteEndElement();

        }
    }
}
