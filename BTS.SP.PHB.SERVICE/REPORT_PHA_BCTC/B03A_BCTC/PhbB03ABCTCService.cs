using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BCTC_TH_TEMPLATE;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03A_BCTC
{


    public interface IPhbB03ABCTCService : IBaseService<PHB_B03A_BCTC>
    {
        void UploadXml(XDocument document, string nguoiTao, string maDonVi, string capDuToan, int nam, DateTime ngayTao);

        void ExportXml(XmlWriter writer, string maDonVi, int nam);
    }

    public class PhbB03ABCTCService : BaseService<PHB_B03A_BCTC>, IPhbB03ABCTCService
    {
        private readonly IRepositoryAsync<PHB_B03A_BCTC> _reportRepo;
        private readonly IRepositoryAsync<PHB_B03A_BCTC_TEMPLATE> _templateRepo;
        private readonly IRepositoryAsync<PHA_BCTC_B03_BCTC_TH_TEMPLATE> _bctcTemplateRepo;
        private readonly IRepositoryAsync<PHB_B03A_BCTC_DETAIL> _detailRepo;

        private const string REPORT_ELEMENT_NAME = "LCTTTT";
        private const string PREFIX_CONTENT_ELEMENT_NAME = "CT";

        private const string COL_THUYET_MINH = "D";
        private const string COL_NAM_NAY = "1";
        private const string COL_NAM_TRUOC = "2";

        public PhbB03ABCTCService(
            IRepositoryAsync<PHB_B03A_BCTC> repository,
            IRepositoryAsync<PHB_B03A_BCTC_TEMPLATE> templateRepo,
            IRepositoryAsync<PHA_BCTC_B03_BCTC_TH_TEMPLATE> bctcTemplateRepo,
            IRepositoryAsync<PHB_B03A_BCTC_DETAIL> detailRepo) : base(repository)
        {
            _reportRepo = repository;
            _templateRepo = templateRepo;
            _bctcTemplateRepo = bctcTemplateRepo;
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
            var report = new PHB_B03A_BCTC
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
                throw new Exception($"Đã tồn tại báo cáo B03A_BCTC năm {report.NAM}");
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
            var lstTemplate = new List<PHB_B03A_BCTC_TEMPLATE>();
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
                var detail = new PHB_B03A_BCTC_DETAIL
                {
                    PHB_B03A_BCTC_REFID = report.REFID,
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
                    var elementName_ThuyetMinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_THUYET_MINH;
                    var elementName_NamNay = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_NAY;
                    var elementName_NamTruoc = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_TRUOC;

                    //query element
                    var thuyetMinh = reportXml.Descendants(elementName_ThuyetMinh).FirstOrDefault();
                    var namNay = reportXml.Descendants(elementName_NamNay).FirstOrDefault();
                    var namTruoc = reportXml.Descendants(elementName_NamTruoc).FirstOrDefault();

                    string thuyetMinhVal;
                    decimal? namNayVal = null;
                    decimal? namTruocVal = null;

                    try
                    {
                        thuyetMinhVal = thuyetMinh == null ? null : thuyetMinh.Value;
                        namNayVal = namNay == null ? (decimal?)null : decimal.Parse(namNay.Value);
                        namTruocVal = namTruoc == null ? (decimal?)null : decimal.Parse(namTruoc.Value);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException();
                    }

                    detail.THUYET_MINH = thuyetMinhVal;
                    detail.SO_CUOI_NAM= namNayVal;
                    detail.SO_DAU_NAM = namTruocVal;
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
            var report = new PHB_B03A_BCTC();
            var lstTemplate = new List<PHA_BCTC_B03_BCTC_TH_TEMPLATE>();
            var lstDetail = new List<PHB_B03A_BCTC_DETAIL>();
            try
            {
                report = _reportRepo.Queryable()
                    .Where(rpt => rpt.MA_DONVI == maDonVi && rpt.NAM == nam)
                    .FirstOrDefault();
                lstTemplate = _bctcTemplateRepo.Queryable()
                    .OrderBy(template => template.STT_SAPXEP)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

            if (report == null)
            {
                writer.WriteStartElement("LCTTTH");
                writer.WriteEndElement();
                return;
            }

            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "BTSTC.PHA_BCTC_B03_BCTC_TH";
                    command.Parameters.Clear();
                    command.Parameters.Add("ma_don_vi", OracleDbType.NVarchar2, 100).Value = maDonVi;
                    command.Parameters.Add("nam", OracleDbType.NVarchar2, 100).Value = nam;
                    command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    OracleDataReader reader = ((OracleRefCursor)command.Parameters["cur"].Value).GetDataReader();
                    while (reader.Read())
                    {
                        var thuyet_minh = reader["THUYET_MINH"] == null ? "" : reader["THUYET_MINH"].ToString();
                        var ma_so = reader["MA_SO"].ToString();
                        var so_cuoi_nam = reader["NAM_NAY"].ToString() == "" ? "0" : reader["NAM_NAY"].ToString();
                        var so_dau_nam = reader["NAM_TRUOC"].ToString() == "" ? "0" : reader["NAM_TRUOC"].ToString();

                        var detail = new PHB_B03A_BCTC_DETAIL
                        {
                            THUYET_MINH = thuyet_minh,
                            MA_SO = ma_so,
                            SO_CUOI_NAM = decimal.Parse(so_cuoi_nam),
                            SO_DAU_NAM = decimal.Parse(so_dau_nam)
                        };

                        lstDetail.Add(detail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            //get list of parent
            var lstParent = lstTemplate
                .Where(tpl => tpl.XML_PARENT_FIELD_NAME != null)
                .Select(tpl => tpl.XML_PARENT_FIELD_NAME)
                .Distinct()
                .ToList();

            //write <THTC>
            writer.WriteStartElement("LCTTTH");

            //write element
            foreach (var parent in lstParent)
            {
                //write parent element such as <TaiSan>
                writer.WriteStartElement(parent);

                //select list maso of current parent
                var lstMaSo = lstTemplate
                    .Where(tpl => tpl.XML_PARENT_FIELD_NAME != null && tpl.XML_PARENT_FIELD_NAME.Trim() == parent)
                    .Select(tpl => tpl.MA_SO)
                    .ToList();

                //loop and write each element
                foreach (var maSo in lstMaSo)
                {
                    if (maSo != null && maSo.Trim() != "")
                    {
                        var elementName_ThuyetMinh = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_THUYET_MINH;
                        var elementName_NamNay = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_NAY;
                        var elementName_NamTruoc = PREFIX_CONTENT_ELEMENT_NAME + maSo.Trim() + COL_NAM_TRUOC;

                        var detail = lstDetail.Where(dt => dt.MA_SO != null && dt.MA_SO.Trim() == maSo).FirstOrDefault();

                        var thuyetMinhVal = detail == null ? "" : detail.THUYET_MINH;
                        var soNamNayVal = detail == null ? 0 : detail.SO_CUOI_NAM;
                        var soNamTruocVal = detail == null ? 0 : detail.SO_DAU_NAM;

                        writer.WriteElementString(elementName_ThuyetMinh, thuyetMinhVal ?? "");
                        writer.WriteElementString(elementName_NamNay, soNamNayVal.ToString());
                        writer.WriteElementString(elementName_NamTruoc, soNamTruocVal.ToString());
                    }
                }

                writer.WriteEndElement();
            }

            //write </THTC>
            writer.WriteEndElement();

        }
    }
}
