using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BTS.SP.AUTHENTICATION.API;
using BTS.SP.AUTHENTICATION.API.Au.AuNhomQuyen;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Implimentations;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Result;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Types;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;

namespace BTS.SP.AUTHENTICATION.API.Controllers.Au
{
    [RoutePrefix("api/auth/AuNhomQuyen")]
    [Route("{id?}")]
    [Authorize]
    public class AuNhomQuyenController : ApiController
    {
        private readonly IAuNhomQuyenService _service;
        private readonly string PHANHE = "A";

        public AuNhomQuyenController(IAuNhomQuyenService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllForConfigNhomQuyen/{username}")]
        public IHttpActionResult GetAllForConfigNhomQuyen(string username)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest();
            var result = new TransferObj<List<SelectObject>>();
            try
            {
                List<SelectObject> lst = new List<SelectObject>();
                using (
                    OracleConnection connection =
                        new OracleConnection(new AuthContext().Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText =
                            @"SELECT MANHOMQUYEN,TENNHOMQUYEN FROM AU_NHOMQUYEN WHERE TRANGTHAI=1 AND PHANHE='"+PHANHE+"' " +
                            "AND MANHOMQUYEN NOT IN(SELECT MANHOMQUYEN FROM AU_NGUOIDUNG_NHOMQUYEN WHERE AU_NGUOIDUNG_NHOMQUYEN.USERNAME='" + username + "')";
                        using (
                            OracleDataReader oracleDataReader =
                                command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (!oracleDataReader.HasRows)
                            {
                                lst = new List<SelectObject>();
                            }
                            else
                            {
                                while (oracleDataReader.Read())
                                {
                                    lst.Add(new SelectObject()
                                    {
                                        Text = oracleDataReader["TENNHOMQUYEN"].ToString(),
                                        Value = oracleDataReader["MANHOMQUYEN"].ToString(),
                                        Selected = false
                                    });
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

        [Route("Select_Page")]
        [HttpPost]
        public IHttpActionResult Select_Page(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<AuNhomQuyenVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<AU_NHOMQUYEN>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1,
                Filter = new QueryFilterLinQ()
                {
                    Property = ClassHelper.GetProperty(() => new AU_NHOMQUYEN().PHANHE),
                    Value = PHANHE,
                    Method = FilterMethod.EqualTo
                }
            };
            try
            {
                var filterResult = _service.Filter(filtered, query);
                if (!filterResult.WasSuccessful)
                {
                    return NotFound();
                }
                result.Data = filterResult.Value;
                result.Status = true;
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [Route("Add")]
        [HttpPost]
        [ResponseType(typeof(AU_NHOMQUYEN))]
        public IHttpActionResult Add(AU_NHOMQUYEN instance)
        {
            var result = new TransferObj<AU_NHOMQUYEN>();
            try
            {
                instance.PHANHE = PHANHE;
                var item = _service.Insert(instance);
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Message = "Cập nhật thành công.";
                result.Data = item;
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return Ok(result);
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("Update/{id}")]
        public IHttpActionResult Update(string id, AU_NHOMQUYEN instance)
        {
            var result = new TransferObj<AU_NHOMQUYEN>();
            if (id != instance.Id)
            {
                result.Status = false;
                result.Message = "Id không hợp lệ";
            }
            else
            {
                try
                {
                    instance.PHANHE = PHANHE;
                    var item = _service.Update(instance);
                    _service.UnitOfWork.Save();
                    result.Status = true;
                    result.Message = "Cập nhật thành công.";
                    result.Data = item;
                }
                catch (Exception e)
                {
                    result.Status = false;
                    result.Message = e.Message;
                }
            }
            return Ok(result);
        }

        [ResponseType(typeof(AU_NHOMQUYEN))]
        [HttpPut]
        [Route("DeleteItem/{id}")]
        public IHttpActionResult DeleteItem(string Id)
        {
            //int id = int.Parse(Id);
            AU_NHOMQUYEN instance = _service.FindById(Id);
            if (instance == null)
            {
                return NotFound();
            }
            try
            {
                var result = new TransferObj<AU_NHOMQUYEN>();
                _service.Delete(instance.Id);
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Message = "Xóa thành công.";
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
