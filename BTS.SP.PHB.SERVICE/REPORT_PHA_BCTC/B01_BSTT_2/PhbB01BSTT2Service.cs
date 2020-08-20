using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_B01_BSTT_2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_2
{


    public interface IPhbB01BSTT2Service : IBaseService<PHB_B01_BSTT_2>
    {
        void UploadXml(XDocument document, string nguoiTao, string maDonVi, string capDuToan, int nam, DateTime ngayTao);
        void ExportXml(XmlWriter writer, string maDonVi, int nam);

    }

    public class PhbB01BSTT2Service : BaseService<PHB_B01_BSTT_2>, IPhbB01BSTT2Service
    {
        private readonly IRepositoryAsync<PHB_B01_BSTT_2> _reportRepo;
        private readonly IRepositoryAsync<PHB_B01_BSTT_2_TEMPLATE> _templateRepo;
        private readonly IRepositoryAsync<PHB_B01_BSTT_2_DETAIL> _detailRepo;

        private const string REPORT_ELEMENT_NAME = "TMTaiChinh";
        private const string PREFIX_CONTENT_ELEMENT_NAME = "CT";

        private const string COL_NAM_NAY = "NN";

        public PhbB01BSTT2Service(
            IRepositoryAsync<PHB_B01_BSTT_2> repository,
            IRepositoryAsync<PHB_B01_BSTT_2_TEMPLATE> templateRepo,
            IRepositoryAsync<PHB_B01_BSTT_2_DETAIL> detailRepo) : base(repository)
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
            var report = new PHB_B01_BSTT_2
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
                throw new Exception($"Đã tồn tại báo cáo B01_BSTT_2 năm {report.NAM}");
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
            var lstTemplate = new List<PHB_B01_BSTT_2_TEMPLATE>();
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
                var maSo = GenMaSo_From_STT(template.STT_SAPXEP);

                //detail
                var detail = new PHB_B01_BSTT_2_DETAIL
                {
                    PHB_B01_BSTT_2_REFID = report.REFID,
                    STT_SAPXEP = template.STT_SAPXEP,
                    STT = template.STT,
                    CHI_TIEU = template.CHI_TIEU,
                    IS_BOLD = template.IS_BOLD,
                    IS_ITALIC = template.IS_ITALIC
                };

                //get data from xml 
                if (maSo != null && maSo.Trim() != "")
                {
                    //join string to get element name
                    var elementName_Namnay = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_NAY;

                    //query element
                    var namNay = reportXml.Descendants(elementName_Namnay).FirstOrDefault();
                    
                    decimal? namNayVal = null;

                    try
                    {
                        namNayVal = namNay == null ? (decimal?)null : decimal.Parse(namNay.Value);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException();
                    }

                    detail.NAM_NAY = namNayVal;
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
            var report = new PHB_B01_BSTT_2();
            var lstTemplate = new List<PHB_B01_BSTT_2_TEMPLATE>();
            var lstDetail = new List<PHB_B01_BSTT_2_DETAIL>();
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
                    .Where(detail => detail.PHB_B01_BSTT_2_REFID == report.REFID)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            //write <THTC>
            writer.WriteStartElement(REPORT_ELEMENT_NAME);

            var lstSTT = lstTemplate
                    .Select(tpl => tpl.STT_SAPXEP)
                    .ToList();

            //loop and write each element
            foreach (var stt in lstSTT)
            {
                var elementName_Namnay = PREFIX_CONTENT_ELEMENT_NAME + GenMaSo_From_STT(stt) + COL_NAM_NAY;

                var detail = lstDetail.Where(dt => dt.STT_SAPXEP == stt).FirstOrDefault();

                var namNayVal = detail == null ? 0 : detail.NAM_NAY;

                writer.WriteElementString(elementName_Namnay, namNayVal.ToString());
            }

            //write </THTC>
            writer.WriteEndElement();

        }

        private string GenMaSo_From_STT(int stt)
        {
            return (72 + stt).ToString();
        }
    }
}
