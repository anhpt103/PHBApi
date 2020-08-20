using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Services;
using Oracle.ManagedDataAccess.Client;

namespace BTS.SP.AUTHENTICATION.API.Au.AuNhomQuyenChucNang
{
    public interface IAuNhomQuyenChucNangService : IDataInfoService<AU_NHOMQUYEN_CHUCNANG>
    {
        List<AuNhomQuyenChucNangVm.ViewModel> GetByMaNhomQuyen(string phanhe,string manhomquyen);
    }
    public class AuNhomQuyenChucNangService: DataInfoServiceBase<AU_NHOMQUYEN_CHUCNANG>, IAuNhomQuyenChucNangService
    {
        public AuNhomQuyenChucNangService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<AuNhomQuyenChucNangVm.ViewModel> GetByMaNhomQuyen(string phanhe, string manhomquyen)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(new AuthContext().Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"SELECT VC.ID,VC.MANHOMQUYEN,VC.MACHUCNANG,CN.TENCHUCNANG,CN.""STATE"",CN.SOTHUTU,VC.XEM,VC.THEM,VC.SUA,VC.XOA,VC.DUYET 
                            FROM AU_NHOMQUYEN_CHUCNANG VC RIGHT JOIN SYS_CHUCNANG CN ON VC.MACHUCNANG = CN.MACHUCNANG WHERE VC.PHANHE='"+phanhe+ "' AND CN.PHANHE='" + phanhe + "' AND VC.MANHOMQUYEN = '" + manhomquyen + "' ORDER BY CN.SOTHUTU";
                        using (OracleDataReader oracleDataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (!oracleDataReader.HasRows) return new List<AuNhomQuyenChucNangVm.ViewModel>();
                            List<AuNhomQuyenChucNangVm.ViewModel> lst = new List<AuNhomQuyenChucNangVm.ViewModel>();
                            while (oracleDataReader.Read())
                            {
                                AuNhomQuyenChucNangVm.ViewModel item = new AuNhomQuyenChucNangVm.ViewModel();
                                item.Id= oracleDataReader["ID"].ToString();
                                item.MACHUCNANG = oracleDataReader["MACHUCNANG"].ToString();
                                item.MANHOMQUYEN = oracleDataReader["MANHOMQUYEN"]?.ToString() ?? string.Empty;
                                item.SOTHUTU = oracleDataReader["SOTHUTU"].ToString();
                                item.STATE = oracleDataReader["STATE"]?.ToString() ?? string.Empty;
                                item.TENCHUCNANG = oracleDataReader["TENCHUCNANG"].ToString();
                                if (oracleDataReader["XEM"] != null)
                                {
                                    item.XEM = oracleDataReader["XEM"].ToString().Equals("1");
                                }
                                if (oracleDataReader["THEM"] != null)
                                {
                                    item.THEM = oracleDataReader["THEM"].ToString().Equals("1");
                                }
                                if (oracleDataReader["SUA"] != null)
                                {
                                    item.SUA = oracleDataReader["SUA"].ToString().Equals("1");
                                }
                                if (oracleDataReader["XOA"] != null)
                                {
                                    item.XOA = oracleDataReader["XOA"].ToString().Equals("1");
                                }
                                if (oracleDataReader["DUYET"] != null)
                                {
                                    item.DUYET = oracleDataReader["DUYET"].ToString().Equals("1");
                                }
                                lst.Add(item);
                            }
                            return lst;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
