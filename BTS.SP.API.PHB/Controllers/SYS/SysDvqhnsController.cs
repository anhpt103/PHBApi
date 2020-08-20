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
using System.Web;

namespace BTS.SP.API.PHB.Controllers.SYS
{
    [RoutePrefix("api/sys/sysDvqhns")]
    [Route("{id?}")]
    public class SysDvqhnsController : ApiController
    {
        private readonly ISysDvqhnsService _service;
        private readonly IDmDVQHNSService _dmDVQHNSService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SysDvqhnsController(
            ISysDvqhnsService service, 
            IUnitOfWorkAsync unitOfWorkAsync,
            ISysDvqhnsService sysDVQHNSService,
            IDmDVQHNSService dmDVQHNSService)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmDVQHNSService = dmDVQHNSService;
            _sysDVQHNSService = sysDVQHNSService;
        }

        [Route("paging")]
        [HttpPost]
        //[CustomAuthorize(Method = "XEM", State = "sysDvqhns")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<SYS_DVQHNS>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<SysDvqhnsVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<SYS_DVQHNS>>();
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
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return InternalServerError();
            }
        }

        [Route("pagingForReport")]
        [HttpPost]
        public async Task<IHttpActionResult> pagingForReport(JObject jsonData)
        {
            var result = new Response<PagedObj<SysDvqhnsVm.ViewModel>>();
            try
            {
                var postData = ((dynamic)jsonData);
                var paged = ((JObject)postData.paged).ToObject<PagedObj<SysDvqhnsVm.ViewModel>>();
                var loaiBc = postData.LOAI_BC;
                var maQhnsCha = postData.MA_QHNS_CHA;
                var DKTK = postData.DKTK;
                var minRn = (paged.currentPage - 1) * paged.itemsPerPage;
                var maxRn = minRn + paged.itemsPerPage;
                var ma_dbhc = postData.MA_DBHC;
               
               
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        //command.CommandText = "SELECT MA_QHNS FROM AU_NGUOIDUNG WHERE USERNAME='" + RequestContext.Principal.Identity.Name + "'";
                        command.CommandText = "SELECT MA_DVQHNS FROM SYS_DVQHNS WHERE DONVI_QL like '" + ma_dbhc + "' and (MA_DVQHNS  like '1%' or MA_DVQHNS like '3%') order by MA_DVQHNS";
                        command.Parameters.Clear();
                        var dr = await command.ExecuteReaderAsync();
                        var lstQhns = new List<string>();
                        string dk = "";
                        while (dr.Read())
                        {
                            string MA_DVQHNS = dr["MA_DVQHNS"].ToString();
                            lstQhns.Add(MA_DVQHNS);
                        }
                        
                       for( int i = 1; i <= lstQhns.Count; i++)
                        {
                            dk += "'" + lstQhns[i - 1] + "',";
                        }
                        string lstMa = dk.Substring(0,dk.Length - 1);


                        if (!string.IsNullOrEmpty(lstMa.ToString()))
                        {
                            var clauseWhere = "";
                            if (loaiBc.ToString().Equals("2"))
                            {
                                clauseWhere += "MA_DVQHNS IN(" + lstMa + ")" ;
                                if (DKTK != null && !string.IsNullOrEmpty(DKTK.ToString()))
                                {
                                    clauseWhere += " AND MA_CHUONG = '" + DKTK + "' or MA_DVQHNS like '" + DKTK + "%'";
                                }
                            }
                            else if (loaiBc.ToString().Equals("3"))
                            {
                                clauseWhere += " MA_DVQHNS_CHA IN(" + lstMa + ")";
                                if (maQhnsCha!=null && !string.IsNullOrEmpty(maQhnsCha.ToString()))
                                {
                                    clauseWhere += " AND MA_DVQHNS_CHA ='" + maQhnsCha + "'";
                                }
                                if (DKTK != null && !string.IsNullOrEmpty(DKTK.ToString()))
                                {
                                    clauseWhere += " AND MA_DVQHNS LIKE '" + DKTK + "%'";
                                }
                            }
                            command.CommandText =
                                "SELECT outer.*FROM (SELECT ROWNUM rn,COUNT(*) OVER () RESULT_COUNT,inner2.* FROM (" +
                                "SELECT MA_DVQHNS,TEN_DVQHNS,MA_DVQHNS_CHA,MA_CHUONG FROM SYS_DVQHNS WHERE "
                                + clauseWhere +
                                " ORDER BY MA_CHUONG" +
                                ") inner2) outer " +
                                "WHERE outer.rn > " + minRn + " AND outer.rn <=" + maxRn;

                            using (var dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                            {
                                if (!dataReader.HasRows)
                                {
                                    result.Error = false;
                                    result.Data = new PagedObj<SysDvqhnsVm.ViewModel>()
                                    {
                                        Data = new List<SysDvqhnsVm.ViewModel>(),
                                        itemsPerPage = paged.itemsPerPage,
                                        totalItems = 0,
                                        currentPage = 1,
                                        takeAll = false
                                    };
                                }
                                else
                                {
                                    int totalItems = 0;
                                    var lst = new List<SysDvqhnsVm.ViewModel>();
                                    while (dataReader.Read())
                                    {
                                        totalItems = int.Parse(dataReader["RESULT_COUNT"].ToString());
                                        lst.Add(new SysDvqhnsVm.ViewModel()
                                        {
                                            TEN_QHNS = dataReader["TEN_DVQHNS"].ToString(),
                                            MA_QHNS = dataReader["MA_DVQHNS"].ToString(),
                                            MA_QHNS_CHA = dataReader["MA_DVQHNS_CHA"]?.ToString(),
                                            MA_CHUONG = dataReader["MA_CHUONG"].ToString(),
                                        });
                                    }
                                    result.Error = false;
                                    result.Data = new PagedObj<SysDvqhnsVm.ViewModel>()
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

        [Route("GetDsDvQhnsByUser/{MaLoai}/{MaDvQhNs}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDsDvQhnsByUser(string MaLoai, string MaDvQhNs)
        {
            var response = new List<SysDvqhnsVm.ViewModel>();    
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.Text;
                        command.CommandText = "SELECT MA_DVQHNS_CHA FROM SYS_DVQHNS WHERE MA_DVQHNS='" + MaDvQhNs + "'";
                        command.Parameters.Clear();
                        var lstQhns_cha = await command.ExecuteScalarAsync();
                        var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                        var currentMA_DBHC = identity.Claims.FirstOrDefault(c => c.Type == "MA_DBHC").Value.ToString();
                        var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
                        var currentMA_CAP = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == "CAPUSER").Value);
                        var currentMA_LOAI = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == "LOAIUSER").Value);
                        if (currentMA_LOAI == 2)
                        {
                            if (!string.IsNullOrEmpty(MaDvQhNs.ToString()))
                            {
                                command.CommandText = "SELECT MA_DVQHNS,TEN_DVQHNS,MA_DVQHNS_CHA,MA_CHUONG FROM SYS_DVQHNS " +
                                    "Where  ";
                                if (currentMA_CAP == 1)
                                {
                                    if (MaLoai == "1")
                                    {
                                        command.CommandText += " MA_CAP = '1' ";
                                    }
                                    else if (MaLoai == "2")
                                    {
                                        command.CommandText += "(ma_dvqhns = '" + MaDvQhNs + "' or ma_dvqhns in (select ma_dvqhns from sys_dvqhns where ma_dvqhns_cha = '" + MaDvQhNs + "')) and MA_CAP = '2'";
                                    }
                                    else if (MaLoai == "3")
                                    {
                                        command.CommandText += "MA_CAP = '3'";
                                    }
                                }
                                if (currentMA_CAP == 2)
                                {
                                    if (MaLoai == "2")
                                    {
                                        command.CommandText += "(ma_dvqhns = '" + MaDvQhNs + "' or ma_dvqhns in (select ma_dvqhns from sys_dvqhns where ma_dvqhns_cha = '" + MaDvQhNs + "')) and MA_CAP = '2'";
                                    }
                                    else if (MaLoai == "3")
                                    {
                                        command.CommandText += "(ma_dvqhns = '" + MaDvQhNs + "' or ma_dvqhns in (select ma_dvqhns from sys_dvqhns where ma_dvqhns_cha = '" + MaDvQhNs + "')) and MA_CAP = '3'";
                                    }
                                }
                                if (currentMA_CAP == 3)
                                {
                                    command.CommandText += " ma_dvqhns = '" + currentMA_QHNS + "' and MA_CAP = '3'";
                                }

                                command.Parameters.Clear();
                                using (var dataReader = command.ExecuteReaderAsync())
                                {
                                    if (dataReader.Result.HasRows)
                                    {
                                        while (dataReader.Result.Read())
                                        {
                                            response.Add(new SysDvqhnsVm.ViewModel()
                                            {
                                                MA_QHNS = dataReader.Result["MA_DVQHNS"].ToString(),
                                                TEN_QHNS = dataReader.Result["TEN_DVQHNS"].ToString(),
                                                MA_QHNS_CHA = dataReader.Result["MA_DVQHNS_CHA"].ToString(),
                                                MA_CHUONG = dataReader.Result["MA_CHUONG"].ToString(),
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(MaDvQhNs.ToString()))
                            {
                                command.CommandText = "SELECT MA_DVQHNS,TEN_DVQHNS,MA_DVQHNS_CHA,MA_CHUONG FROM SYS_DVQHNS " +
                                    "Where  ";
                                    if (MaLoai == "1")
                                    {
                                        command.CommandText += " MA_CAP = '1' and DONVI_QL = '" + currentMA_DBHC + "'";
                                    }
                                    else if (MaLoai == "2")
                                    {
                                        command.CommandText += " MA_CAP = '2' and DONVI_QL = '" + currentMA_DBHC + "'";
                                    }
                                    else if (MaLoai == "3")
                                    {
                                        command.CommandText += " MA_CAP = '3' and DONVI_QL = '" + currentMA_DBHC + "'";
                                    }
                                command.Parameters.Clear();
                                using (var dataReader = command.ExecuteReaderAsync())
                                {
                                    if (dataReader.Result.HasRows)
                                    {
                                        while (dataReader.Result.Read())
                                        {
                                            response.Add(new SysDvqhnsVm.ViewModel()
                                            {
                                                MA_QHNS = dataReader.Result["MA_DVQHNS"].ToString(),
                                                TEN_QHNS = dataReader.Result["TEN_DVQHNS"].ToString(),
                                                MA_QHNS_CHA = dataReader.Result["MA_DVQHNS_CHA"].ToString(),
                                                MA_CHUONG = dataReader.Result["MA_CHUONG"].ToString(),
                                            });
                                        }
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


        [Route("GetDsDvQhnsByMaDBHC/{madbhc}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDsDvQhnsByMaDBHC(string madbhc)
        {
            var response = new List<SysDvqhnsVm.ViewModel>();
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
                            command.CommandText = "select MA_DVQHNS,TEN_DVQHNS,MA_CHUONG from sys_dvqhns  where MA_HUYEN like '" + madbhc + "' or MA_XA like '" + madbhc + "' and MA_DVQHNS not like '7%' order by MA_DVQHNS";
                            command.Parameters.Clear();
                            using (var dataReader = command.ExecuteReaderAsync())
                            {
                                if (dataReader.Result.HasRows)
                                {
                                    while (dataReader.Result.Read())
                                    {
                                        response.Add(new SysDvqhnsVm.ViewModel()
                                        {
                                            MA_QHNS = dataReader.Result["MA_DVQHNS"].ToString(),
                                            TEN_QHNS = dataReader.Result["TEN_DVQHNS"].ToString(),
                                            
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

        [Route("GetSelectData")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A")).Select(x => new ChoiceObj()
                {
                    Value = x.MA_DVQHNS,
                    Text = x.TEN_DVQHNS,
                    Parent = x.MA_DVQHNS_CHA,
                    ExtendValue = x.MA_CHUONG
                }).ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return null;
            }
        }

        [Route("GetByMaDvqhns/{maDvhqns}")]
        [HttpGet]
        [Authorize]
        public async Task<IList<SysDvqhnsVm.ViewModel_Model>> GetByMaDvqhns(string maDvhqns)
        {
            if (string.IsNullOrEmpty(maDvhqns)) return new List<SysDvqhnsVm.ViewModel_Model>();
            var lst = new List<SysDvqhnsVm.ViewModel_Model>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;

                        command.CommandText = "SELECT MA_QHNS FROM AU_NGUOIDUNG WHERE USERNAME='" +
                                              RequestContext.Principal.Identity.Name + "'";
                        command.Parameters.Clear();
                        var lstQhns = await command.ExecuteScalarAsync();
                        if (!string.IsNullOrEmpty(lstQhns.ToString()))
                        {
                            command.CommandText =
                                "SELECT MA_DVQHNS,TEN_DVQHNS,MA_DVQHNS_CHA,MA_CHUONG " +
                                "FROM SYS_DVQHNS WHERE MA_DVQHNS LIKE '" + maDvhqns + "%' OR (MA_DVQHNS IN('" + lstQhns + "') OR MA_DVQHNS_CHA IN('" + lstQhns +
                                "'))";
                            command.Parameters.Clear();
                            using (var dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                            {
                                if (!dataReader.HasRows)
                                {
                                    return null;
                                }
                                while (dataReader.Read())
                                {
                                    lst.Add(new SysDvqhnsVm.ViewModel_Model()
                                    {
                                        TEN_DVQHNS = dataReader["TEN_DVQHNS"].ToString(),
                                        MA_DVQHNS = dataReader["MA_DVQHNS"].ToString(),
                                      
                                        MA_CHUONG = dataReader["MA_CHUONG"].ToString(),
                                    });
                                }
                            }
                        }
                        else
                        {
                            return new List<SysDvqhnsVm.ViewModel_Model>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return new List<SysDvqhnsVm.ViewModel_Model>();
            }
            return lst;
        }

        [Route("GetByTenDvqhns")]
        [HttpPost]
        [Authorize]
        public async Task<IList<SysDvqhnsVm.ViewModel>> GetByTenDvqhns(SYS_DVQHNS searchModel)
        {
            if (string.IsNullOrEmpty(searchModel.TEN_DVQHNS)) return new List<SysDvqhnsVm.ViewModel>();
            searchModel.TEN_DVQHNS = searchModel.TEN_DVQHNS.ToLower();
            var lst = new List<SysDvqhnsVm.ViewModel>();
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;

                        command.CommandText = "SELECT MA_QHNS FROM AU_NGUOIDUNG WHERE USERNAME='" +
                                              RequestContext.Principal.Identity.Name + "'";
                        command.Parameters.Clear();
                        var lstQhns = await command.ExecuteScalarAsync();
                        if (!string.IsNullOrEmpty(lstQhns.ToString()))
                        {
                            command.CommandText =
                                "SELECT MA_DVQHNS,TEN_DVQHNS,MA_DVQHNS_CHA,MA_CHUONG " +
                                "FROM SYS_DVQHNS WHERE LOWER(TEN_DVQHNS) LIKE '%" + searchModel.TEN_DVQHNS + "%' AND (MA_DVQHNS IN(" + lstQhns + ") OR MA_DVQHNS_CHA IN(" + lstQhns +
                                "))";
                            command.Parameters.Clear();
                            using (var dataReader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                            {
                                if (!dataReader.HasRows)
                                {
                                    return null;
                                }
                                while (dataReader.Read())
                                {
                                    lst.Add(new SysDvqhnsVm.ViewModel()
                                    {
                                        TEN_QHNS = dataReader["TEN_DVQHNS"].ToString(),
                                        MA_QHNS = dataReader["MA_DVQHNS"].ToString(),
                                        MA_QHNS_CHA = dataReader["MA_DVQHNS_CHA"]?.ToString(),
                                        MA_CHUONG = dataReader["MA_CHUONG"].ToString(),
                                    });
                                }
                            }
                        }
                        else
                        {
                            return new List<SysDvqhnsVm.ViewModel>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return new List<SysDvqhnsVm.ViewModel>();
            }
            return lst;
        }

        //Lấy List MÃ con Theo Mã Cha
        [Route("GetByMaDvqhnsCha/{maCha}")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetByMaDvqhnsCha(string maCha)
        {
            var response = new Response<List<SysDvqhnsVm.ViewModel>>();
            response.Data = new List<SysDvqhnsVm.ViewModel>();

            if(maCha == null || maCha.Trim() == "")
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            var lstDonVi = new List<SYS_DVQHNS>();

            try
            {
                lstDonVi = await _sysDVQHNSService.Queryable().Where(donvi => donvi.MA_DVQHNS_CHA == maCha).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            foreach(var donvi in lstDonVi)
            {
                var item = new SysDvqhnsVm.ViewModel
                {
                    MA_QHNS = donvi.MA_DVQHNS,
                    TEN_QHNS = donvi.TEN_DVQHNS,
                    MA_CHUONG = donvi.MA_CHUONG,
                    MA_QHNS_CHA = donvi.MA_DVQHNS_CHA
                };
                response.Data.Add(item);
            }
            
            return Ok(response);
        }

        //get all
        [Route("GetAll")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetAll()
        {
            var response = new Response<List<SysDvqhnsVm.ViewModel>>();
            response.Data = new List<SysDvqhnsVm.ViewModel>();
            
            var lstDonVi = new List<PHB_DM_DVQHNS>();

            try
            {
                lstDonVi = await _dmDVQHNSService.Queryable().ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            foreach (var donvi in lstDonVi)
            {
                var item = new SysDvqhnsVm.ViewModel
                {
                    MA_QHNS = donvi.MA_QHNS,
                    TEN_QHNS = donvi.TEN_QHNS,
                    MA_CHUONG = donvi.MA_CHUONG,
                    MA_QHNS_CHA = donvi.MA_CHA
                };
                response.Data.Add(item);
            }

            return Ok(response);
        }

        [Route("GetDetail/{maDvqhns}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetail(string maDvqhns)
        {
            var response = new Response<SYS_DVQHNS>();

            try
            {
                response.Data = _service.Queryable().FirstOrDefault(x => x.MA_DVQHNS == maDvqhns);
            }
            catch(Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
            response.Message = "Tìm Kiếm Thành Công";
            response.Error = false;
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