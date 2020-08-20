using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BTS.SP.AUTHENTICATION.API;
using BTS.SP.AUTHENTICATION.API.Au.AuNguoiDungQuyen;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using Oracle.ManagedDataAccess.Client;

namespace BTS.SP.AUTHENTICATION.API.Controllers.Au
{
    [RoutePrefix("api/auth/AuNguoiDungQuyen")]
    [Route("{id?}")]
    [Authorize]
    public class AuNguoiDungQuyenController : ApiController
    {
        private readonly IAuNguoiDungQuyenService _service;
        private readonly string PHANHE = "A";
        public AuNguoiDungQuyenController(IAuNguoiDungQuyenService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetByUsername/{username}")]
        public IHttpActionResult GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest();
            var result = new TransferObj<List<AuNguoiDungQuyenVm.ViewModel>>();
            try
            {
                List<AuNguoiDungQuyenVm.ViewModel> lst = new List<AuNguoiDungQuyenVm.ViewModel>();
                using (var connection = new OracleConnection(new AuthContext().Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText =
                            @"SELECT NQ.ID,NQ.USERNAME,NQ.MACHUCNANG,SYSC.TENCHUCNANG,SYSC.SOTHUTU,NQ.XEM,NQ.THEM,NQ.SUA,NQ.XOA,NQ.DUYET 
                            FROM AU_NGUOIDUNG_QUYEN NQ INNER JOIN SYS_CHUCNANG SYSC ON NQ.MACHUCNANG=SYSC.MACHUCNANG WHERE NQ.PHANHE='" + PHANHE + "' " +
                            "AND SYSC.PHANHE='" + PHANHE + "' AND NQ.USERNAME='" + username + "'";
                        using (OracleDataReader oracleDataReader =
                            command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (!oracleDataReader.HasRows)
                            {
                                lst = new List<AuNguoiDungQuyenVm.ViewModel>();
                            }
                            else
                            {
                                while (oracleDataReader.Read())
                                {
                                    AuNguoiDungQuyenVm.ViewModel item = new AuNguoiDungQuyenVm.ViewModel()
                                    {
                                        //ID = Int32.Parse(oracleDataReader["ID"].ToString()),
                                        USERNAME = oracleDataReader["USERNAME"].ToString(),
                                        MACHUCNANG = oracleDataReader["MACHUCNANG"].ToString(),
                                        TENCHUCNANG = oracleDataReader["TENCHUCNANG"].ToString(),
                                        SOTHUTU = oracleDataReader["SOTHUTU"].ToString(),
                                        XEM = oracleDataReader["XEM"].ToString().Equals("1"),
                                        THEM = oracleDataReader["THEM"].ToString().Equals("1"),
                                        SUA = oracleDataReader["SUA"].ToString().Equals("1"),
                                        XOA = oracleDataReader["XOA"].ToString().Equals("1"),
                                        DUYET = oracleDataReader["DUYET"].ToString().Equals("1"),
                                        PHANHE = PHANHE
                                    };
                                    lst.Add(item);
                                }
                            }
                        }
                    }
                }
                result.Status = true;
                result.Data = lst;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Config")]
        public IHttpActionResult Config(AuNguoiDungQuyenVm.ConfigModel model)
        {
            if (string.IsNullOrEmpty(model.USERNAME)) return BadRequest();
            var result = new TransferObj();
            try
            {
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    foreach (var item in model.LstDelete)
                    {
                        _service.Delete(item.Id);
                    }
                }
                if (model.LstAdd != null && model.LstAdd.Count > 0)
                {
                    foreach (var item in model.LstAdd)
                    {
                        AU_NGUOIDUNG_QUYEN obj = new AU_NGUOIDUNG_QUYEN()
                        {
                            ObjectState = ObjectState.Added,
                            USERNAME = item.USERNAME,
                            MACHUCNANG = item.MACHUCNANG,
                            XOA = item.XOA,
                            DUYET = item.DUYET,
                            SUA = item.SUA,
                            THEM = item.THEM,
                            TRANGTHAI = 1,
                            XEM = item.XEM,
                            PHANHE = PHANHE
                        };
                        _service.Insert(obj, false);
                    }
                }
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    foreach (var item in model.LstEdit)
                    {
                        AU_NGUOIDUNG_QUYEN obj = new AU_NGUOIDUNG_QUYEN()
                        {
                            Id = item.Id,
                            PHANHE = PHANHE,
                            USERNAME = item.USERNAME,
                            MACHUCNANG = item.MACHUCNANG,
                            ObjectState = ObjectState.Modified,
                            TRANGTHAI = 1,
                            XEM = item.XEM,
                            THEM = item.THEM,
                            SUA = item.SUA,
                            XOA = item.XOA,
                            DUYET = item.DUYET
                        };
                        _service.Update(obj);
                    }
                }
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Message = "Cập nhật thành công.";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return Ok(result);
}
    }
}
