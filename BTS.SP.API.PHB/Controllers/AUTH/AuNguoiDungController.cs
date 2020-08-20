using System;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using System.Web.Http.Description;
using BTS.SP.PHB.SERVICE.Helper;
using System.Linq;

namespace BTS.SP.API.PHB.Controllers.AUTH
{
    [RoutePrefix("api/auth/AuNguoiDung")]
    [Route("{id?}")]
    [Authorize]
    public class AuNguoiDungController : ApiController
    {
        private readonly IAuNguoiDungService _auNguoiDungService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AuNguoiDungController(IAuNguoiDungService auNguoiDungService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _auNguoiDungService = auNguoiDungService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("paging")]
        [CustomAuthorize(Method = "XEM", State = "AuNguoiDung")]
        [HttpPost]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<AU_NGUOIDUNG>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<AuNguoiDungVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<AU_NGUOIDUNG>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1
            };
            try
            {
                var filterResult = await _auNguoiDungService.FilterAsync(filtered, query);
                if (!filterResult.WasSuccessful)
                {
                    return NotFound();
                }
                result.Data = filterResult.Value;
                result.Error = false;
                return Ok(result);
            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
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
                    instance.PASSWORD = SP.PHB.ENTITY.Helper.MD5Encrypt.MD5Hash(instance.PASSWORD);
                    var check =
                        _auNguoiDungService.Queryable().FirstOrDefault(
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
                instance.PASSWORD = SP.PHB.SERVICE.Helper.MD5Encrypt.MD5Hash(instance.PASSWORD);
                _auNguoiDungService.Insert(instance);
                _unitOfWorkAsync.SaveChanges();
                result.Status = true;
                result.Message = "Cập nhật thành công.";
                result.Data = instance;
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
        public IHttpActionResult Update(int id, AU_NGUOIDUNG instance)
        {
            var result = new TransferObj<AU_NGUOIDUNG>();
            if (id != instance.ID)
            {
                return BadRequest();
            }
            try
            {
                var entity = _auNguoiDungService.FindById(id);
                if (entity == null) return BadRequest();

                entity.PASSWORD = instance.PASSWORD;
                entity.CHUCVU = instance.CHUCVU;
                entity.EMAIL = instance.EMAIL;
                entity.FULLNAME = instance.FULLNAME;
                entity.GHICHU = instance.GHICHU;
                entity.LOAI = instance.LOAI;
                entity.MA_DBHC = instance.MA_DBHC;
                entity.MA_DBHC_CHA = instance.MA_DBHC_CHA;
                entity.MA_DONVI = instance.MA_DONVI;
                entity.MA_PHONGBAN = instance.MA_PHONGBAN;
                entity.MA_QHNS = instance.MA_QHNS;
                entity.PHONE = instance.PHONE;
                _auNguoiDungService.Update(entity);
                
                _unitOfWorkAsync.SaveChanges();

                result.Status = true;
                result.Message = "Cập nhật thành công.";
                result.Data = instance;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("UpdatePassWord/{id}")]
        public IHttpActionResult UpdatePassWord(int id, AU_NGUOIDUNG instance)
        {
            var result = new TransferObj<AU_NGUOIDUNG>();
            if (id != instance.ID)
            {
                return BadRequest();
            }
            try
            {
                var entity = _auNguoiDungService.FindById(id);
                if (entity == null) return BadRequest();
                instance.PASSWORD = SP.PHB.SERVICE.Helper.MD5Encrypt.MD5Hash(instance.PASSWORD);
                _auNguoiDungService.Update(instance);
                _unitOfWorkAsync.SaveChanges();
                result.Status = true;
                result.Message = "Đổi mật khẩu thành công";
                result.Data = instance;
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
                _auNguoiDungService.Delete(Int32.Parse(id));
                _unitOfWorkAsync.SaveChanges();
                result.Status = true;
                result.Message = "Xóa thành công.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
