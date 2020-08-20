using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Services;
using Oracle.ManagedDataAccess.Client;

namespace BTS.SP.AUTHENTICATION.API.ServiceFunc.SysChucNang
{
    public interface ISysChucNangService : IDataInfoService<SYS_CHUCNANG>
    {
        List<SYS_CHUCNANG> GetAllForConfigNhomQuyen(string manhomquyen, string phanhe);
        List<SYS_CHUCNANG> GetAllForConfigQuyen(string username, string phanhe);
    }
    public class SysChucNangService: DataInfoServiceBase<SYS_CHUCNANG>, ISysChucNangService
    {
        public SysChucNangService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected override Expression<Func<SYS_CHUCNANG, bool>> GetKeyFilter(SYS_CHUCNANG instance)
        {
            return x => x.MACHUCNANG == instance.MACHUCNANG;
        }

        public List<SYS_CHUCNANG> GetAllForConfigNhomQuyen(string manhomquyen,string phanhe)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(new AuthContext().Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"SELECT B.MACHUCNANG,B.TENCHUCNANG,B.""STATE"",B.SOTHUTU FROM SYS_CHUCNANG B WHERE B.MACHUCNANG 
                                                NOT IN (SELECT A.MACHUCNANG FROM AU_NHOMQUYEN_CHUCNANG A WHERE A.PHANHE='"+ phanhe + "' AND A.MANHOMQUYEN='" + manhomquyen + "') AND B.PHANHE='" + phanhe + "' AND B.STATE IS NOT NULL ORDER BY B.SOTHUTU";
                        using (OracleDataReader oracleDataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (!oracleDataReader.HasRows) return null;
                            List<SYS_CHUCNANG> lst = new List<SYS_CHUCNANG>();
                            while (oracleDataReader.Read())
                            {
                                SYS_CHUCNANG item = new SYS_CHUCNANG();
                                item.MACHUCNANG = oracleDataReader["MACHUCNANG"].ToString();
                                item.SOTHUTU = oracleDataReader["SOTHUTU"].ToString();
                                item.STATE = oracleDataReader["STATE"].ToString();
                                item.TENCHUCNANG = oracleDataReader["TENCHUCNANG"].ToString();
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

        public List<SYS_CHUCNANG> GetAllForConfigQuyen(string username, string phanhe)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(new AuthContext().Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"SELECT SYSCHUCNANG.MACHUCNANG,SYSCHUCNANG.TENCHUCNANG,SYSCHUCNANG.SOTHUTU,SYSCHUCNANG.STATE FROM SYS_CHUCNANG SYSCHUCNANG 
                                                WHERE TRANGTHAI =1 AND PHANHE='"+ phanhe + "' AND STATE IS NOT NULL AND SYSCHUCNANG.MACHUCNANG NOT IN(SELECT MACHUCNANG FROM AU_NGUOIDUNG_QUYEN WHERE AU_NGUOIDUNG_QUYEN.PHANHE='"+phanhe+"' AND AU_NGUOIDUNG_QUYEN.USERNAME='" + username + "') ORDER BY SYSCHUCNANG.TENCHUCNANG";

                        using (OracleDataReader oracleDataReader =
                            command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (!oracleDataReader.HasRows) return null;
                            List<SYS_CHUCNANG> lst = new List<SYS_CHUCNANG>();
                            while (oracleDataReader.Read())
                            {
                                SYS_CHUCNANG item = new SYS_CHUCNANG();
                                item.MACHUCNANG = oracleDataReader["MACHUCNANG"].ToString();
                                item.SOTHUTU = oracleDataReader["SOTHUTU"].ToString();
                                item.STATE = oracleDataReader["STATE"].ToString();
                                item.TENCHUCNANG = oracleDataReader["TENCHUCNANG"].ToString();
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
