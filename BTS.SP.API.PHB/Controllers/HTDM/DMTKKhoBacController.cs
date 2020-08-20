using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmTKKhoBac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmTKKhoBac")]
    [Route("{id?}")]
    public class DMTKKhoBacController : ApiController
    {
        private IDmTKKhoBacService _service;

        public DMTKKhoBacController(IDmTKKhoBacService service)
        {
            _service = service;
        }

        [Route("GetKhoBacByMaDiaBan/{maDiaBan}")]
        [HttpGet]
        public IList<ChoiceObj> GetKhoBacByMaDiaBan(string maDiaBan)
        {
            try
            {
                var a = _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && x.MA_DBHC == maDiaBan)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_DBHC,
                        Text = x.MA,                       
                        Id = x.ID
                    }).ToList();
               
                return a;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return null;
            }
        }

    }
}