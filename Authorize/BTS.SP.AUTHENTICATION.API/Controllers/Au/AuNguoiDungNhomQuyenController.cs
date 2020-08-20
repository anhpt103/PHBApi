using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using BTS.SP.AUTHENTICATION.API;
using BTS.SP.AUTHENTICATION.API.Au.AuNguoiDungNhomQuyen;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using Oracle.ManagedDataAccess.Client;

namespace BTS.SP.AUTHENTICATION.API.Controllers.Au
{
    [RoutePrefix("api/auth/AuNguoiDungNhomQuyen")]
    [Route("{id?}")]
    [Authorize]
    public class AuNguoiDungNhomQuyenController : ApiController
    {
        private readonly IAuNguoiDungNhomQuyenService _service;
        private readonly string PHANHE = "TH";
        public AuNguoiDungNhomQuyenController(IAuNguoiDungNhomQuyenService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetByUsername/{username}")]
        public IHttpActionResult GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest();
            var result = new TransferObj<List<AuNguoiDungNhomQuyenVm.ViewModel>>();
            try
            {
                List<AuNguoiDungNhomQuyenVm.ViewModel> lst =new List<AuNguoiDungNhomQuyenVm.ViewModel>();
                using (var connection = new OracleConnection(new AuthContext().Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText =
                            @"SELECT NN.ID,NN.USERNAME,NN.MANHOMQUYEN,NQ.TENNHOMQUYEN FROM AU_NGUOIDUNG_NHOMQUYEN NN INNER JOIN AU_NHOMQUYEN NQ ON NN.MANHOMQUYEN=NQ.MANHOMQUYEN 
                            WHERE NN.PHANHE='" + PHANHE+"' AND NQ.PHANHE='"+PHANHE+"' AND NN.USERNAME='"+username+"'";
                        using (OracleDataReader oracleDataReader =
                            command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (!oracleDataReader.HasRows)
                            {
                                lst=new List<AuNguoiDungNhomQuyenVm.ViewModel>();
                            }
                            else
                            {
                                while (oracleDataReader.Read())
                                {
                                    AuNguoiDungNhomQuyenVm.ViewModel item = new AuNguoiDungNhomQuyenVm.ViewModel()
                                    {
                                        //Id = Int32.Parse(oracleDataReader["ID"].ToString()),
                                        Id = oracleDataReader["ID"].ToString(),
                                        USERNAME = oracleDataReader["USERNAME"].ToString(),
                                        MANHOMQUYEN = oracleDataReader["MANHOMQUYEN"].ToString(),
                                        TENNHOMQUYEN = oracleDataReader["TENNHOMQUYEN"].ToString()
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
        public IHttpActionResult Config(AuNguoiDungNhomQuyenVm.ConfigModel model)
        {
            if (string.IsNullOrEmpty(model.USERNAME)) return BadRequest();
            var result=new TransferObj();
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
                        AU_NGUOIDUNG_NHOMQUYEN obj = new AU_NGUOIDUNG_NHOMQUYEN()
                        {
                            ObjectState = ObjectState.Added,
                            MANHOMQUYEN = item.MANHOMQUYEN,
                            PHANHE = PHANHE,
                            USERNAME = item.USERNAME,
                        };
                        _service.Insert(obj, false);
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
