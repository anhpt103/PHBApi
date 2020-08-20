using System;
using System.Data;
using BTS.SP.AUTHENTICATION.API;
using Oracle.ManagedDataAccess.Client;

namespace BTS.SP.AUTHENTICATION.API.Au.Shared
{
    public interface ISharedService
    {
        RoleState GetRoleStateByMaChucNang(string phanhe, string username,string machucnang);
    }
    public class SharedService: ISharedService
    {
        public RoleState GetRoleStateByMaChucNang(string phanhe,string username, string machucnang)
        {
            RoleState roleState = new RoleState();
            //var cacheData = MemoryCacheHelper.GetValue(phanhe+"|"+machucnang + "|" + username);
            //if (cacheData==null)
            //{

            //}
            //else
            //{
            //    roleState = (RoleState)cacheData;

            //}
            using (var connection = new OracleConnection(new AuthContext().Database.Connection.ConnectionString))
            {
                connection.Open();
                using (OracleCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        @"SELECT XEM,THEM,SUA,XOA,DUYET FROM AU_NHOMQUYEN_CHUCNANG WHERE PHANHE='" + phanhe + "' AND MACHUCNANG='" + machucnang +
                        "' AND MANHOMQUYEN IN (SELECT MANHOMQUYEN FROM AU_NGUOIDUNG_NHOMQUYEN WHERE PHANHE='" + phanhe + "' AND USERNAME='" +
                        username + "') UNION SELECT AU_NGUOIDUNG_QUYEN.XEM,AU_NGUOIDUNG_QUYEN.THEM,AU_NGUOIDUNG_QUYEN.SUA,AU_NGUOIDUNG_QUYEN.XOA,AU_NGUOIDUNG_QUYEN.DUYET " +
                        "FROM AU_NGUOIDUNG_QUYEN WHERE AU_NGUOIDUNG_QUYEN.PHANHE='" + phanhe + "' AND AU_NGUOIDUNG_QUYEN.MACHUCNANG='" + machucnang + "' AND AU_NGUOIDUNG_QUYEN.USERNAME='" + username + "'";
                    using (OracleDataReader oracleDataReader = command.ExecuteReader())
                    {
                        if (!oracleDataReader.HasRows)
                        {
                            roleState = new RoleState()
                            {
                                STATE = string.Empty,
                                View = false,
                                Approve = false,
                                Delete = false,
                                Add = false,
                                Edit = false
                            };
                        }
                        else
                        {
                            roleState.STATE = machucnang;
                            while (oracleDataReader.Read())
                            {
                                int objXem = Int32.Parse(oracleDataReader["XEM"].ToString());
                                if (objXem == 1)
                                {
                                    roleState.View = true;
                                }
                                int objThem = Int32.Parse(oracleDataReader["THEM"].ToString());
                                if (objThem == 1)
                                {
                                    roleState.Add = true;
                                }
                                int objSua = Int32.Parse(oracleDataReader["SUA"].ToString());
                                if (objSua == 1)
                                {
                                    roleState.Edit = true;
                                }
                                int objXoa = Int32.Parse(oracleDataReader["XOA"].ToString());
                                if (objXoa == 1)
                                {
                                    roleState.Delete = true;
                                }
                                int objDuyet = Int32.Parse(oracleDataReader["DUYET"].ToString());
                                if (objDuyet == 1)
                                {
                                    roleState.Approve = true;
                                }
                            }
                            MemoryCacheHelper.Add(phanhe + "|" + machucnang + "|" + username, roleState,
                                DateTimeOffset.Now.AddHours(6));
                        }
                    }
                }
            }
            return roleState;
        }
    }
}
