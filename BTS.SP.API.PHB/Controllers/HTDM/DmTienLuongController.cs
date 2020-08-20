﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmTienLuong;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using System.Web;
using System.Security.Claims;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using System.IO;
using System.Xml.Linq;
using BTS.SP.PHB.ENTITY;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Globalization;
using BTS.SP.API.PHB.Services;
using BTS.SP.API.PHB.ViewModels;
using Repository.Pattern.Infrastructure;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmTienLuong")]
    [Route("{id?}")]
    public class DmTienLuongController : ApiController
    {
        private readonly IDmTienLuongService _service;
        private readonly IDmDVQHNSService _serviceDMDVQHNS;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public DmTienLuongController(IDmTienLuongService service, IUnitOfWorkAsync unitOfWorkAsync,  IDmDVQHNSService serviceDMDVQHNS)
        {
            _service = service;
            _serviceDMDVQHNS = serviceDMDVQHNS;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmTienLuong")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_TIENLUONG>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmTienLuongVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_TIENLUONG>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1
            };
            try
            {
                var filterResult = await _service.FilterAsync(filtered, query);
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
                return InternalServerError();
            }
        }

        [HttpPost]
        [CustomAuthorize(Method = "THEM", State = "phb_dmTienLuong")]
        public async Task<IHttpActionResult> Post(PHB_DM_TIENLUONG model)
        {
            Response<PHB_DM_TIENLUONG> response = new Response<PHB_DM_TIENLUONG>();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ObjectState = ObjectState.Added;
                    _service.Insert(model);
                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        response.Error = false;
                        response.Message = "Cập nhật thành công.";
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Lỗi cập nhật dữ liệu.";
                    }
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ex.Message;
                }

                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [CustomAuthorize(Method = "SUA", State = "phb_dmTienLuong")]
        public async Task<IHttpActionResult> Put(string id, PHB_DM_TIENLUONG model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty(id) || !id.Equals(model.ID.ToString())) return BadRequest();
            model.ObjectState = ObjectState.Modified;
            _service.Update(model);
            Response<string> response = new Response<string>();
            try
            {
                if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                {
                    response.Error = false;
                    response.Message = "Cập nhật thành công.";
                }
                else
                {
                    response.Error = true;
                    response.Message = "Lỗi cập nhật dữ liệu.";
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpDelete]
        [CustomAuthorize(Method = "XOA", State = "phb_dmTienLuong")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            try
            {
                Response<string> response = new Response<string>();
                PHB_DM_TIENLUONG item = await _service.FindByIdAsync(long.Parse(id));
                if (item == null) return NotFound();
                item.ObjectState = ObjectState.Deleted;
                _service.Delete(item);
                if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                {
                    response.Error = false;
                    response.Message = "Xóa thành công.";
                }
                else
                {
                    response.Error = true;
                    response.Message = "Lỗi cập nhật dữ liệu.";
                }
                return Ok(response);
            }
            catch (FormatException ex)
            {
                WriteLogs.LogError(ex);
                return BadRequest();
            }
        }

        [Route("GetSelectData")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).OrderBy(x => x.MA_TIEN_LUONG)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_TIEN_LUONG,
                        //Text = x.,
                        //ExtendValue = x.TEMPLATE.ToString(),
                        ReportValue = x.MUC_LUONG_TT,
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}