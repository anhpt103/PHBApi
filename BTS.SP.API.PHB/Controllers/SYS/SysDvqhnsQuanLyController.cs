using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;

namespace BTS.SP.API.PHB.Controllers.SYS
{
    [RoutePrefix("api/sys/sysDvqhnsQuanLy")]
    [Route("{id?}")]
    public class SysDvqhnsQuanLyController : ApiController
    {
        private readonly ISysDvqhnsService _service;
        private readonly IDmDVQHNSService _dmDVQHNSService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SysDvqhnsQuanLyController(
            ISysDvqhnsService service, 
            IUnitOfWorkAsync unitOfWorkAsync,
            ISysDvqhns_QuanLyService sysDVQHNSQLService,
            IDmDVQHNSService dmDVQHNSService)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmDVQHNSService = dmDVQHNSService;
            _sysDVQHNSQLService = sysDVQHNSQLService;
        }

        #region GetDsDvQhnsByMaDBHC
        [Route("GetDsDvQhnsByMaDBHC/{madbhc}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDsDvQhnsByMaDBHC(string madbhc)
        {
            var response = new List<SysDvqhns_QuanLyVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        if (!string.IsNullOrEmpty(madbhc))
                        {
                            command.CommandText = "select MA_DVQHNS,TEN_DVQHNS,MA_CHUONG, MA_DBHC from sys_dvqhns_quanly  where MA_DBHC like '" + madbhc + "' order by MA_DBHC";
                            command.Parameters.Clear();
                            using (var dataReader = command.ExecuteReaderAsync())
                            {
                                if (dataReader.Result.HasRows)
                                {
                                    while (dataReader.Result.Read())
                                    {
                                        response.Add(new SysDvqhns_QuanLyVm.ViewModel()
                                        {
                                            MA_DVQHNS = dataReader.Result["MA_DVQHNS"].ToString(),
                                            TEN_DVQHNS = dataReader.Result["TEN_DVQHNS"].ToString(),
                                            MA_CHUONG = dataReader.Result["MA_CHUONG"].ToString(),
                                            MA_DBHC = dataReader.Result["MA_DBHC"].ToString(),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
            }
            return Ok(response);
        }
        #endregion

        [Route("GetDsDvQhnsByMaDvQhNS/{madvqhns}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDsDvQhnsByMaDvQhNS(string madvqhns)
        {
            var response = new List<SysDvqhns_QuanLyVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        if (!string.IsNullOrEmpty(madvqhns))
                        {
                            command.CommandText = "select MA_DVQHNS,TEN_DVQHNS,MA_CHUONG, MA_DBHC from sys_dvqhns_quanly  where MA_DVQHNS_CHA like '" + madvqhns + "' or MA_DVQHNS like'" + madvqhns + "' order by MA_DVQHNS";
                            command.Parameters.Clear();
                            using (var dataReader = command.ExecuteReaderAsync())
                            {
                                if (dataReader.Result.HasRows)
                                {
                                    while (dataReader.Result.Read())
                                    {
                                        response.Add(new SysDvqhns_QuanLyVm.ViewModel()
                                        {
                                            MA_DVQHNS = dataReader.Result["MA_DVQHNS"].ToString(),
                                            TEN_DVQHNS = dataReader.Result["TEN_DVQHNS"].ToString(),
                                            MA_CHUONG = dataReader.Result["MA_CHUONG"].ToString(),
                                            MA_DBHC = dataReader.Result["MA_DBHC"].ToString(),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLogs.LogError(ex);
            }
            return Ok(response);
        }


        [Route("pagingForReport")]
        [HttpPost]
        public async Task<IHttpActionResult> PagingForReport(JObject jsonData)
        {
            var result = new Response<PagedObj<SysDvqhns_QuanLyVm.ViewModel>>();
            try
            {
                var postData = ((dynamic)jsonData);
                var paged = ((JObject)postData.paged).ToObject<PagedObj<SysDvqhns_QuanLyVm.ViewModel>>();
                var loaiBc = postData.LOAI_BC;
                var maQhnsCha = postData.MA_QHNS_CHA;
                var DKTK = postData.DKTK;
                var minRn = (paged.currentPage - 1) * paged.itemsPerPage;
                var maxRn = minRn + paged.itemsPerPage;
                var ma_dbhc = postData.MA_DBHC;
                var ma_dvqhns_ql = postData.MA_QHNS;
                var sql = "";
                if (postData.LOAI_DK == 1)
                {
                     sql = "SELECT MA_DVQHNS FROM SYS_DVQHNS_QUANLY WHERE MA_DBHC like '" + ma_dbhc + "' and (MA_DVQHNS  like '1%' or MA_DVQHNS like '3%') order by MA_DVQHNS";
                }

                if(postData.LOAI_DK == 2)
                {
                     sql = "SELECT MA_DVQHNS FROM SYS_DVQHNS_QUANLY WHERE MA_DVQHNS_CHA = '" + ma_dvqhns_ql + "' and (MA_DVQHNS  like '1%' or MA_DVQHNS like '3%') order by MA_DVQHNS";
                }

                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        command.Parameters.Clear();
                        var dr = await command.ExecuteReaderAsync();
                        var lstQhns = new List<string>();
                        string dk = "";
                        while (dr.Read())
                        {
                            string MA_DVQHNS = dr["MA_DVQHNS"].ToString();
                            lstQhns.Add(MA_DVQHNS);
                        }

                        for (int i = 1; i <= lstQhns.Count; i++)
                        {
                            dk += "'" + lstQhns[i - 1] + "',";
                        }
                        string lstMa = dk.Substring(0, dk.Length - 1);


                        if (!string.IsNullOrEmpty(lstMa.ToString()))
                        {
                            var clauseWhere = "";
                            if (loaiBc.ToString().Equals("2"))
                            {
                                clauseWhere += "MA_DVQHNS IN(" + lstMa + ")";
                                if (DKTK != null && !string.IsNullOrEmpty(DKTK.ToString()))
                                {
                                    clauseWhere += " AND MA_CHUONG = '" + DKTK + "' or MA_DVQHNS like '" + DKTK + "%'";
                                }
                            }
                            else if (loaiBc.ToString().Equals("3"))
                            {                               
                                if (maQhnsCha != null && !string.IsNullOrEmpty(maQhnsCha.ToString()))
                                {
                                    clauseWhere += " MA_DVQHNS_CHA ='" + maQhnsCha + "'";
                                }
                                else
                                {
                                    clauseWhere += " MA_DVQHNS_CHA = '" + ma_dvqhns_ql + "'";
                                }
                                if (DKTK != null && !string.IsNullOrEmpty(DKTK.ToString()))
                                {
                                    clauseWhere += " AND MA_DVQHNS LIKE '" + DKTK + "%'";
                                }
                            }
                            command.CommandText =
                                "SELECT outer.*FROM (SELECT ROWNUM rn,COUNT(*) OVER () RESULT_COUNT,inner2.* FROM (" +
                                "SELECT MA_DVQHNS,TEN_DVQHNS,MA_DVQHNS_CHA,MA_CHUONG FROM SYS_DVQHNS_QUANLY WHERE "
                                + clauseWhere +
                                " ORDER BY MA_CHUONG" +
                                ") inner2) outer " +
                                "WHERE outer.rn > " + minRn + " AND outer.rn <=" + maxRn;

                            using (var dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                            {
                                if (!dataReader.HasRows)
                                {
                                    result.Error = false;
                                    result.Data = new PagedObj<SysDvqhns_QuanLyVm.ViewModel>()
                                    {
                                        Data = new List<SysDvqhns_QuanLyVm.ViewModel>(),
                                        itemsPerPage = paged.itemsPerPage,
                                        totalItems = 0,
                                        currentPage = 1,
                                        takeAll = false
                                    };
                                }
                                else
                                {
                                    int totalItems = 0;
                                    var lst = new List<SysDvqhns_QuanLyVm.ViewModel>();
                                    while (dataReader.Read())
                                    {
                                        totalItems = int.Parse(dataReader["RESULT_COUNT"].ToString());
                                        lst.Add(new SysDvqhns_QuanLyVm.ViewModel()
                                        {
                                            TEN_DVQHNS = dataReader["TEN_DVQHNS"].ToString(),
                                            MA_DVQHNS = dataReader["MA_DVQHNS"].ToString(),
                                            MA_DVQHNS_CHA = dataReader["MA_DVQHNS_CHA"]?.ToString(),
                                            MA_CHUONG = dataReader["MA_CHUONG"].ToString(),
                                        });
                                    }
                                    result.Error = false;
                                    result.Data = new PagedObj<SysDvqhns_QuanLyVm.ViewModel>()
                                    {
                                        Data = lst,
                                        itemsPerPage = paged.itemsPerPage,
                                        totalItems = totalItems,
                                        currentPage = paged.currentPage,
                                        takeAll = false
                                    };
                                }
                            }
                        }
                        else
                        {
                            return InternalServerError();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }

        [Route("GetDsDvQhnsByUser/{MaDvQhNs}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDsDvQhnsByUser(string MaDvQhNs)
        {
            var response = new List<SysDvqhns_QuanLyVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;                       
                        command.Parameters.Clear();
                        
                        if (!string.IsNullOrEmpty(MaDvQhNs.ToString()))
                        {
                            command.CommandText = "SELECT MA_DVQHNS,TEN_DVQHNS,MA_DVQHNS_CHA,MA_CHUONG FROM SYS_DVQHNS_QUANLY Where MA_DVQHNS_CHA ='" + MaDvQhNs + "'";
                            command.Parameters.Clear();
                            using (var dataReader = command.ExecuteReaderAsync())
                            {
                                if (dataReader.Result.HasRows)
                                {
                                    while (dataReader.Result.Read())
                                    {
                                        response.Add(new SysDvqhns_QuanLyVm.ViewModel()
                                        {
                                            MA_DVQHNS = dataReader.Result["MA_DVQHNS"].ToString(),
                                            TEN_DVQHNS = dataReader.Result["TEN_DVQHNS"].ToString(),
                                            MA_DVQHNS_CHA = dataReader.Result["MA_DVQHNS_CHA"].ToString(),
                                            MA_CHUONG = dataReader.Result["MA_CHUONG"].ToString(),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
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