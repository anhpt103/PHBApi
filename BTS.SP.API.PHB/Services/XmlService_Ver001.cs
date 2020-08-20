using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.DmBaoCao;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_1;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_2;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03B_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B04_BCTC;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace BTS.SP.API.PHB.Services
{
    public interface IXmlService_Ver001
    {
        string WriteXml(ExportXmlViewModel model, string maDonVi, string tenDonVi);
        Task ReadXml(XDocument document, string nguoiTao, string maDonVi, string capDuToan, int nam, DateTime ngayTao);
    }

    public class XmlService_Ver001 : IXmlService_Ver001
    {
        private readonly IPhaB01BCTCService _b01BCTCService;
        private readonly IPhaB02BCTCService _b02BCTCService;
        private readonly IPhbB03ABCTCService _b03ABCTCService;
        private readonly IPhaB03BBCTCService _b03BBCTCService;
        private readonly IPhaB04BCTCService _b04BCTCService;
        private readonly IPhaB01BSTT1Service _b01BSTT1Service;
        private readonly IPhbB01BSTT2Service _b01BSTT2Service;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public XmlService_Ver001(
            IPhaB01BCTCService b01BCTCService,
            IPhaB02BCTCService b02BCTCService,
            IPhbB03ABCTCService b03ABCTCService,
            IPhaB03BBCTCService b03BBCTCService,
            IPhaB04BCTCService b04BCTCService,
            IPhaB01BSTT1Service b01BSTT1Service,
            IPhbB01BSTT2Service b01BSTT2Service,

            IUnitOfWorkAsync unitOfWorkAsync)
        {
            _b01BCTCService = b01BCTCService;
            _b02BCTCService = b02BCTCService;
            _b03ABCTCService = b03ABCTCService;
            _b03BBCTCService = b03BBCTCService;
            _b04BCTCService = b04BCTCService;
            _b01BSTT1Service = b01BSTT1Service;
            _b01BSTT2Service = b01BSTT2Service;

            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public string WriteXml(ExportXmlViewModel model, string maDonVi, string tenDonVi)
        {
            var tempFilePath = Path.GetTempFileName();
            var content = "";

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };

            using (XmlWriter writer = XmlWriter.Create(tempFilePath, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("BCCCTT");
                WriteXml_TTChung(writer, maDonVi, tenDonVi, model.Nam, model.PhienBan, model.TenBC, model.NguoiLapBC, model.NgayKy.ToLocalTime(), model.NguoiKy, model.NguoiKiemSoat);
                writer.WriteStartElement("TTBaoCao");

                try
                {
                    _b01BCTCService.ExportXml(writer, maDonVi, model.Nam);
                    _b02BCTCService.ExportXml(writer, maDonVi, model.Nam);
                    _b03BBCTCService.ExportXml(writer, maDonVi, model.Nam);

                    _b04BCTCService.ExportXml(writer, maDonVi, model.Nam);

                    writer.WriteStartElement("BSTT");
                    _b01BSTT1Service.ExportXml(writer, maDonVi, model.Nam);
                    _b01BSTT2Service.ExportXml(writer, maDonVi, model.Nam);
                    writer.WriteEndElement();
                }
                catch (ArgumentNullException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw;
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();

            }

            content = File.ReadAllText(tempFilePath);
            File.Delete(tempFilePath);

            return content;
        }

        public async Task ReadXml(XDocument document, string nguoiTao, string maDonVi, string capDuToan, int nam, DateTime ngayTao)
        {
            //upload 5 reports
            try
            {
                _b01BCTCService.UploadXml(document, nguoiTao, maDonVi, capDuToan, nam, ngayTao);
                _b02BCTCService.UploadXml(document, nguoiTao, maDonVi, capDuToan, nam, ngayTao);
                _b03ABCTCService.UploadXml(document, nguoiTao, maDonVi, capDuToan, nam, ngayTao);
                _b03BBCTCService.UploadXml(document, nguoiTao, maDonVi, capDuToan, nam, ngayTao);
                _b04BCTCService.UploadXml(document, nguoiTao, maDonVi, capDuToan, nam, ngayTao);
                _b01BSTT1Service.UploadXml(document, nguoiTao, maDonVi, capDuToan, nam, ngayTao);
                _b01BSTT2Service.UploadXml(document, nguoiTao, maDonVi, capDuToan, nam, ngayTao);

                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void WriteXml_TTChung(XmlWriter writer, string maDonVi, string tenDonVi, int nam, PhienBan phienBan, string tenBC, string nguoiLapBC, DateTime ngayKy, string nguoiKy, string nguoiKiemSoat)
        {
            var phienBanStr = "";
            switch (phienBan)
            {
                case PhienBan.Ver_001:
                    phienBanStr = "001";
                    break;
                default:
                    break;
            }

            writer.WriteStartElement("TTChung");

            writer.WriteElementString("phienBan", phienBanStr);
            writer.WriteElementString("mauBC", "BCTC_99");
            writer.WriteElementString("tenBC", tenBC);
            writer.WriteElementString("kyBC", nam.ToString());
            writer.WriteElementString("maDonvi", maDonVi);
            writer.WriteElementString("tenDonVi", tenDonVi);
            writer.WriteElementString("ngayLapBC", DateTime.Now.ToString("dd/MM/yyyy"));
            writer.WriteElementString("nguoiLapBC", nguoiLapBC);
            writer.WriteElementString("ngayKy", ngayKy.ToString("dd/MM/yyyy"));
            writer.WriteElementString("nguoiKy", nguoiKy);
            writer.WriteElementString("nguoiKsoat", nguoiKiemSoat);

            writer.WriteEndElement();
        }
        
    }
}