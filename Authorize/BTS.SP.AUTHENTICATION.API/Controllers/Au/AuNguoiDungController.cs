using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using BTS.SP.AUTHENTICATION.API.Au.AuNguoiDung;
using BTS.SP.AUTHENTICATION.API.BuildQuery;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Implimentations;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Result;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Types;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.ServiceFunc.DmDBHC;
using Newtonsoft.Json.Linq;

namespace BTS.SP.AUTHENTICATION.API.Controllers.Au
{
    [RoutePrefix("api/auth/AuNguoiDung")]
    [Route("{id?}")]
    [Authorize]
    public class AuNguoiDungController : ApiController
    {
        private readonly IAuNguoiDungService _service;
        public readonly IDM_DBHCService _serviceDBHC;

        public AuNguoiDungController(IAuNguoiDungService service, IDM_DBHCService serviceDBHC)
        {
            _service = service;
            _serviceDBHC = serviceDBHC;
        }

        [Route("Select_Page")]
        [HttpPost]
        public async Task<IHttpActionResult> Select_Page(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<AuNguoiDungVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<AU_NGUOIDUNG>>();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var currentMA_DBHC = identity.Claims.FirstOrDefault(c => c.Type == "MA_DBHC").Value.ToString();
            var crrDBHC = _serviceDBHC.Repository.DbSet.FirstOrDefault(x => x.MA_DBHC == currentMA_DBHC);

            if (crrDBHC != null)
            {
                var lisDBHC = _serviceDBHC.GetListDbhc(crrDBHC.MA_T);
                var query = new QueryBuilder
                {
                    Take = paged.itemsPerPage,
                    Skip = paged.fromItem - 1,
                    Filter = new QueryFilterLinQ()
                    {
                        SubFilters = new List<IQueryFilter>()
                        {
                            new QueryFilterLinQ
                            {
                                Property = ClassHelper.GetProperty(() => new AU_NGUOIDUNG().MA_DBHC),
                                Method = FilterMethod.In,
                                Value = lisDBHC,
                            },
                        },
                        Method = FilterMethod.And
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
                catch (Exception ex)
                {
                    return InternalServerError();
                }
            }
            else
            {
                return InternalServerError();

            }

        }

        [Route("CheckPass")]
        [HttpPost]
        [ResponseType(typeof(AU_NGUOIDUNG))]
        public IHttpActionResult CheckPass(AU_NGUOIDUNG instance)
        {
            var result = new TransferObj<bool>();
            result.Data = false;
            try
            {
                if (!string.IsNullOrEmpty(instance.PASSWORD))
                {
                    instance.PASSWORD = MD5Encrypt.MD5Hash(instance.PASSWORD);
                    var check =
                        _service.Repository.DbSet.FirstOrDefault(
                            x => x.USERNAME == instance.USERNAME && x.PASSWORD == instance.PASSWORD);
                    if (check != null) result.Data = true;
                }
                result.Status = result.Data;
                result.Data = result.Data;
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return Ok(result);
        }

        [Route("Add")]
        [HttpPost]
        [ResponseType(typeof(AU_NGUOIDUNG))]
        public IHttpActionResult Add(AU_NGUOIDUNG instance)
        {
            var result = new TransferObj<AU_NGUOIDUNG>();
            try
            {
                Guid id = Guid.NewGuid();
                instance.Id = id.ToString();
                instance.PASSWORD = MD5Encrypt.MD5Hash(instance.PASSWORD);
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
        public IHttpActionResult Update(string id, AU_NGUOIDUNG instance)
        {
            var result = new TransferObj<AU_NGUOIDUNG>();
            if (id != instance.Id)
            {
                return BadRequest();
            }
            try
            {
                var entity = _service.FindById(id, false);
                if (entity == null) return BadRequest();
                instance.PASSWORD = entity.PASSWORD;
                var item = _service.Update(instance);
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Message = "Cập nhật thành công.";
                result.Data = item;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("UpdatePassWord/{id}")]
        public IHttpActionResult UpdatePassWord(string id, AU_NGUOIDUNG instance)
        {
            var result = new TransferObj<AU_NGUOIDUNG>();
            if (id != instance.Id)
            {
                return BadRequest();
            }
            try
            {
                var entity = _service.FindById(id, false);
                if (entity == null) return BadRequest();
                instance.PASSWORD = MD5Encrypt.MD5Hash(instance.PASSWORD);
                var item = _service.Update(instance);
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Message = "Đổi mật khẩu thành công";
                result.Data = item;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("DeleteItem/{id}")]
        public IHttpActionResult DeleteItem(string id)
        {
            try
            {
                var result = new TransferObj();
                //_service.Delete(Int32.Parse(id));
                _service.Delete(id);
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Message = "Xóa thành công.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
