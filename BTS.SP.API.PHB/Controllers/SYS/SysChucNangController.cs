﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SYS.SysChucNang;
using BTS.SP.PHB.SERVICE.UTILS;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.SYS
{
    [RoutePrefix("api/sys/SysChucNang")]
    [Route("{id?}")]
    [Authorize]
    public class SysChucNangController : ApiController
    {
        private readonly string PHANHE = "B";

        private readonly ISysChucNangService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SysChucNangController(ISysChucNangService service, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "sysChucNang")]
        public IHttpActionResult Paging(PagingParam<string> para)
        {
            Response<Paging<List<SYS_CHUCNANG>>> response = new Response<Paging<List<SYS_CHUCNANG>>>();
            try
            {
                var query = _service.Queryable().Where(x => x.PHANHE.Equals(PHANHE));
                if (!string.IsNullOrEmpty(para.searchModel))
                {
                    query = query.Where(x => x.MACHUCNANG.Contains(para.searchModel) || x.TENCHUCNANG.Contains(para.searchModel));
                }
                query = query.OrderBy(x => x.SOTHUTU);
                Paging<List<SYS_CHUCNANG>> paging = new Paging<List<SYS_CHUCNANG>>();
                paging.totalItems = query.Count();
                paging.data = query.Skip((para.currentPage - 1) * (para.itemsPerPage)).Take(para.itemsPerPage).ToList();
                response.Data = paging;
                response.Error = false;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetSelectData")]
        public IList<ChoiceObj> GetSelectData()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANGTHAI == 1 && x.PHANHE.Equals(PHANHE))
                    .OrderBy(x => x.SOTHUTU)
                    .Select(x => new ChoiceObj()
                    {
                        Id = x.ID,
                        Value = x.MACHUCNANG,
                        Text = x.TENCHUCNANG,
                        Parent = x.MACHA
                    }).ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetAllForConfigNhomQuyen/{manhomquyen}")]
        public IHttpActionResult GetAllForConfig(string manhomquyen)
        {
            if (string.IsNullOrEmpty(manhomquyen)) return BadRequest();
            Response<List<SYS_CHUCNANG>> response = new Response<List<SYS_CHUCNANG>>();
            try
            {
                response.Error = false;
                response.Data= _service.GetAllForConfigNhomQuyen(PHANHE,manhomquyen);
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllForConfigQuyen/{username}")]
        public IHttpActionResult GetAllForConfigQuyen(string username)
        {
            if (string.IsNullOrEmpty(username)) return BadRequest();
            Response<List<SYS_CHUCNANG>> response = new Response<List<SYS_CHUCNANG>>();
            try
            {
                response.Error = false;
                response.Data = _service.GetAllForConfigQuyen(PHANHE,username);
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
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