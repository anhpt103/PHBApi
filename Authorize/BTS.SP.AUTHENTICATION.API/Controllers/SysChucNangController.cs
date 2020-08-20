using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BTS.SP.AUTHENTICATION.API.BuildQuery;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Implimentations;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Result;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Types;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.ServiceFunc.SysChucNang;
using Newtonsoft.Json.Linq;

namespace BTS.SP.AUTHENTICATION.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/sys/SysChucNang")]
    [System.Web.Http.Route("{id?}")]
    [System.Web.Http.Authorize]
    public class SysChucNangController : ApiController
    {
        private readonly ISysChucNangService _service;
        //private readonly IPHA_HACHTOAN_THUService _serviceTHU;
        //private readonly IPHA_HACHTOAN_CHIService _serviceCHI;
        private readonly string PHANHE = "A1";
        public SysChucNangController(ISysChucNangService service)
        {
            _service = service;
            //_serviceTHU = serviceThu;
            //_serviceCHI = serviceChi;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllForConfigNhomQuyen/{manhomquyen}")]
        public IHttpActionResult GetAllForConfig(string manhomquyen)
        {
            if (string.IsNullOrEmpty(manhomquyen)) return BadRequest();
            var result=new TransferObj<List<SYS_CHUCNANG>>();
            try
            {
                var data = _service.GetAllForConfigNhomQuyen(manhomquyen, PHANHE);
                result.Status = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        public class LastUpdateObj
        {
            public DateTime DATE_THU { get; set; }
            public DateTime DATE_CHI { get; set; }
        }

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("GetLastUpdate")]
        //[ResponseType(typeof(LastUpdateObj))]
        //public IHttpActionResult GetLastUpdate()
        //{
        //    var result = new TransferObj<LastUpdateObj>();
        //    try
        //    {
        //        var thu = _serviceTHU.Repository.DbSet.Select(x => x.NGAY_KET_SO).ToList();
        //        var lastDateTHU = thu.Max();
        //        lastDateTHU = new DateTime(lastDateTHU.Value.Year, lastDateTHU.Value.Month, lastDateTHU.Value.Day);
        //        var chi = _serviceCHI.Repository.DbSet.Select(x => x.NGAY_KET_SO).ToList();
        //        var lastDateCHI = chi.Max();
        //        lastDateCHI = new DateTime(lastDateCHI.Value.Year, lastDateCHI.Value.Month, lastDateCHI.Value.Day);

        //        var tmp = new LastUpdateObj();
        //        if (lastDateTHU != null) tmp.DATE_THU = lastDateTHU.Value;
        //        if (lastDateCHI != null) tmp.DATE_CHI = lastDateCHI.Value;
        //        result.Data = tmp;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = false;
        //        result.Message = ex.Message;
        //    }
        //    return Ok(result);
        //}

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllForConfigQuyen/{username}")]
        public IHttpActionResult GetAllForConfigQuyen(string username)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest();
            var result = new TransferObj<List<SYS_CHUCNANG>>();
            try
            {
                var data = _service.GetAllForConfigQuyen(username, PHANHE);
                result.Status = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetByPhanHe/{phanhe}")]
        public IList<ChoiceObj> GetByPhanHe(string phanhe)
        {
            if(string.IsNullOrEmpty(phanhe)) return new List<ChoiceObj>();
            try
            {
                var data = _service.Repository.DbSet.Where(x => x.PHANHE.Equals(phanhe) && x.TRANGTHAI == 1)
                    .OrderBy(x => x.SOTHUTU).Select(x=>new ChoiceObj()
                    {
                        Id = x.Id,
                        Value = x.MACHUCNANG,
                        Text = x.TENCHUCNANG,
                        Parent = x.MACHA
                    }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return new List<ChoiceObj>();
            }
        }

        [System.Web.Http.Route("Select_Page")]
        public IHttpActionResult Select_Page(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<SysChucNangVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<SYS_CHUCNANG>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1,
                Filter = new QueryFilterLinQ()
                {
                    Property = ClassHelper.GetProperty(() => new SYS_CHUCNANG().PHANHE),
                    Value = "A",
                    Method = FilterMethod.EqualTo
                },
                Orders = new List<IQueryOrder>()
                {
                    new QueryOrder()
                    {
                        Field = "SOTHUTU",
                        Method = OrderMethod.ASC
                    }
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

        [System.Web.Http.Route("AddNew")]
        [ResponseType(typeof(SYS_CHUCNANG))]
        public IHttpActionResult AddNew(SYS_CHUCNANG instance)
        {
            var result = new TransferObj<SYS_CHUCNANG>();
            try
            {
                instance.PHANHE = PHANHE;
                var item = _service.Insert(instance);
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Data = item;
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
            }
            return Ok(result);
        }

        //[ResponseType(typeof(void))]
        //[System.Web.Http.HttpPut]
        //[System.Web.Http.Route("Update/{id}")]
        //public async Task<IHttpActionResult> Update(int id, SYS_CHUCNANG instance)
        //{
        //    var result = new TransferObj<SYS_CHUCNANG>();
        //    if (id != instance.Id)
        //    {
        //        result.Status = false;
        //        result.Message = "Id không hợp lệ";
        //        return Ok(result);
        //    }
        //    try
        //    {
        //        instance.PHANHE = PHANHE;
        //        var item = _service.Update(instance);
        //        await _service.UnitOfWork.SaveAsync();
        //        result.Status = true;
        //        result.Data = item;
        //        result.Message = "Cập nhật thành công.";
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        result.Status = false;
        //        result.Message = e.Message;
        //        return Ok(result);
        //    }
        //}

        //[ResponseType(typeof(SYS_CHUCNANG))]
        //[System.Web.Http.Route("DeleteItem/{Id}")]
        //[System.Web.Http.HttpPut]
        //public async Task<IHttpActionResult> DeleteItem(string Id)
        //{
        //    var result = new TransferObj<SYS_CHUCNANG>();
        //    int id = int.Parse(Id);
        //    SYS_CHUCNANG instance = _service.FindById(id);
        //    if (instance == null)
        //    {
        //        return NotFound();
        //    }
        //    try
        //    {
        //        _service.Delete(instance.ID);
        //        await _service.UnitOfWork.SaveAsync();
        //        result.Status = true;
        //        result.Message = "Cập nhật thành công.";
        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        return InternalServerError();
        //    }
        //}
    }
}
