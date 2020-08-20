using BTS.SP.AUTHENTICATION.API;
using BTS.SP.AUTHENTICATION.API.Au.AuNhomQuyenChucNang;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BTS.SP.AUTHENTICATION.API.Controllers.Au
{
    [RoutePrefix("api/auth/AuNhomQuyenChucNang")]
    [Route("{id?}")]
    [Authorize]
    public class AuNhomQuyenChucNangController : ApiController
    {
        private readonly IAuNhomQuyenChucNangService _service;
        private readonly string PHANHE="A";
        public AuNhomQuyenChucNangController(IAuNhomQuyenChucNangService service)
        {
            _service = service;
        }

        [Route("GetByMaNhomQuyen/{manhomquyen}")]
        public IHttpActionResult GetByMaNhomQuyen(string manhomquyen)
        {
            if (string.IsNullOrEmpty(manhomquyen)) return BadRequest();
            var result = new TransferObj<List<AuNhomQuyenChucNangVm.ViewModel>>();
            try
            {
                var data = _service.GetByMaNhomQuyen(PHANHE,manhomquyen);
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

        [Route("Config")]
        [HttpPost]
        public IHttpActionResult Config(AuNhomQuyenChucNangVm.ConfigModel model)
        {
            if (string.IsNullOrEmpty(model.MANHOMQUYEN)) return BadRequest();
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
                        AU_NHOMQUYEN_CHUCNANG obj = new AU_NHOMQUYEN_CHUCNANG()
                        {
                            ObjectState = ObjectState.Added,
                            MANHOMQUYEN = item.MANHOMQUYEN,
                            MACHUCNANG = item.MACHUCNANG,
                            PHANHE = PHANHE,
                            XOA = item.XOA,
                            DUYET = item.DUYET,
                            SUA = item.SUA,
                            THEM = item.THEM,
                            TRANGTHAI = 1,
                            XEM = item.XEM
                        };
                        _service.Insert(obj,false);
                    }
                }
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    foreach (var item in model.LstEdit)
                    {
                        AU_NHOMQUYEN_CHUCNANG obj = new AU_NHOMQUYEN_CHUCNANG()
                        {
                            Id = item.Id,
                            MACHUCNANG = item.MACHUCNANG,
                            MANHOMQUYEN = item.MANHOMQUYEN,
                            PHANHE = PHANHE,
                            XEM = item.XEM,
                            THEM = item.THEM,
                            SUA = item.SUA,
                            XOA = item.XOA,
                            DUYET = item.DUYET,
                            TRANGTHAI = 1,
                            ObjectState = ObjectState.Modified
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
