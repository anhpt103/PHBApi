using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.DOICHIEUSOLIEU;
using BTS.SP.PHB.SERVICE.Models.DOICHIEUSOLIEU;
using BTS.SP.PHB.SERVICE.REPORT.DOICHIEUSOLIEU;
using System.Security.Claims;
using BTS.SP.PHB.ENTITY;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Web;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/PhbDoiChieuSoLieu")]
    [Route("{id?}")]
    public class PhbDoiChieuSoLieuController : ApiController
    {
        private readonly IPhbDoiChieuSoLieuService _phbDoiChieuSoLieuService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ISysDvqhnsService _SysDvqhns;

        public PhbDoiChieuSoLieuController(PhbDoiChieuSoLieuService phbDoiChieuSoLieuService, IUnitOfWorkAsync unitOfWorkAsync, ISysDvqhnsService SysDvqhns)
        {
            _phbDoiChieuSoLieuService = phbDoiChieuSoLieuService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _SysDvqhns = SysDvqhns;
        }

        [Route("paging")]
        [HttpPost]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DOICHIEUSOLIEU>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DOICHIEUSOLIEUVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DOICHIEUSOLIEU>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1
            };
            try
            {
                var filterResult = await _phbDoiChieuSoLieuService.FilterAsync(filtered, query);
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


        [Route("GetTemplate")]
        [HttpPost]
        public HttpResponseMessage GetTemplate()
        {
            var dto = new DOICHIEUSOLIEUVm.TemplateExcel();
            HttpResponseMessage result = null;
            string file = dto.MA_DVQHNS;
            try
            {
                
                string urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/DOICHIEUSOLIEU/");
                file = _phbDoiChieuSoLieuService.CreateTemplate(file, urlFile, dto);
                if (!string.IsNullOrEmpty(file))
                {
                    if (!File.Exists(file))
                    {
                        result = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        result = Request.CreateResponse(HttpStatusCode.OK);
                        result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
                        result.Content.Headers.ContentType =
                            new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        result.Content.Headers.ContentDisposition.FileName = "Export_(" +
                                                                             DateTime.Now.ToString("dd-MM-yyyy") +
                                                                             ").xlsx";
                    }
                }


                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("Post")]
        [ResponseType(typeof(PHB_DOICHIEUSOLIEU))]
        public async Task<IHttpActionResult> Post(List<DOICHIEUSOLIEUVm.InsertModel> instance)
        {
            Response<string> response = new Response<string>();
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            for (int i = 0; i < instance.Count; i++)
            {
                if (string.IsNullOrEmpty(instance[i].MA_DVQHNS) || string.IsNullOrEmpty(instance[i].TEN_DVQHNS)) return BadRequest();
                try
                {
                    decimal tienTabmis = 0;
                    decimal.TryParse(instance[i].SOTIEN_TABMIS.ToString(), out tienTabmis);
                    PHB_DOICHIEUSOLIEU bieu01B = new PHB_DOICHIEUSOLIEU()
                    {
                        MA_DVQHNS = instance[i].MA_DVQHNS,
                        TEN_DVQHNS = instance[i].TEN_DVQHNS,
                        MA_DVQHNS_CHA = instance[i].MA_DVQHNS_CHA,
                        CAP_DUTOAN = instance[i].CAP_DUTOAN,
                        NGAY_TAO = DateTime.Now,
                        LOAI_DULIEU = instance[i].LOAI_DULIEU,
                        NAM = instance[i].NAM,
                        MA_DBHC = firstOrDefault.Value,
                        SOTIEN_DENGHI = instance[i].SOTIEN_DENGHI,
                        SOTIEN_TABMIS = tienTabmis,
                        ObjectState = ObjectState.Added
                    };
                    var checkExist = await _phbDoiChieuSoLieuService.Queryable().FirstOrDefaultAsync(x => x.MA_DVQHNS.Equals(bieu01B.MA_DVQHNS) && x.NAM.Equals(bieu01B.NAM) && x.LOAI_DULIEU == bieu01B.LOAI_DULIEU && x.CAP_DUTOAN == bieu01B.CAP_DUTOAN);
                    if (checkExist != null)
                    {
                        checkExist.ObjectState = ObjectState.Deleted;
                        _phbDoiChieuSoLieuService.Delete(checkExist);
                        await _unitOfWorkAsync.SaveChangesAsync();
                    }
                    _phbDoiChieuSoLieuService.Insert(bieu01B);
                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        response.Error = false;
                        response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                    }
                }
                catch (NullReferenceException ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                }
                catch (DbEntityValidationException ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_DATA;
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                }
            }
            return Ok(response);
        }

        public string ConvertData(string input)
        {
            string result = "";
            input = input.Substring(input.IndexOf(":") + 1);
            input = input.Trim();
            return input;
        }
        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            int number = 0;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            var startRowI = 8;
                            var endRowI = startRowI + 9;
                            string LoaiDuLieu = workSheet.Cells[1, 1].Value.ToString();
                            LoaiDuLieu = ConvertData(LoaiDuLieu).ToUpper();
                            string Nam = workSheet.Cells[2, 1].Value.ToString();
                            Nam = ConvertData(Nam);
                            string CapDuToan = workSheet.Cells[3, 1].Value.ToString();
                            CapDuToan = ConvertData(CapDuToan);
                            for (var i = startRowI; i <= endRowI; i++)
                            {
                                SYS_DVQHNS donvi = new SYS_DVQHNS();
                                decimal tienDeNghi = 0;
                                decimal tienTabmis = 0;
                                string MaDonViQuanHe = "";
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 1].Value.ToString()))
                                {
                                    MaDonViQuanHe = workSheet.Cells[i, 1].Value.ToString();
                                    donvi =
                                        await
                                            _SysDvqhns.Queryable()
                                                .FirstOrDefaultAsync(x => x.MA_DVQHNS == MaDonViQuanHe);

                                    decimal.TryParse(workSheet.Cells[i, 2].Value.ToString(), out tienTabmis);
                                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out tienDeNghi);
                                    var doichieu = new PHB_DOICHIEUSOLIEU
                                    {
                                        MA_DVQHNS = workSheet.Cells[i, 1].Value != null
                                            ? workSheet.Cells[i, 1].Value.ToString()
                                            : null,
                                        TEN_DVQHNS = donvi.TEN_DVQHNS,
                                        MA_DVQHNS_CHA = donvi.MA_DVQHNS_CHA,
                                        LOAI_DULIEU = LoaiDuLieu,
                                        NAM = int.Parse(Nam),
                                        CAP_DUTOAN = CapDuToan,
                                        SOTIEN_DENGHI = tienDeNghi,
                                        SOTIEN_TABMIS = tienTabmis,
                                        MA_DBHC = firstOrDefault.Value,
                                        NGAY_TAO = DateTime.Now,
                                        ObjectState = ObjectState.Added,
                                    };
                                    var checkExist = await _phbDoiChieuSoLieuService.Queryable().FirstOrDefaultAsync(x => x.MA_DVQHNS.Equals(doichieu.MA_DVQHNS) && x.NAM.Equals(doichieu.NAM) && x.LOAI_DULIEU == doichieu.LOAI_DULIEU && x.CAP_DUTOAN == doichieu.CAP_DUTOAN);
                                    if (checkExist != null)
                                    {
                                        checkExist.ObjectState = ObjectState.Deleted;
                                        _phbDoiChieuSoLieuService.Delete(checkExist);
                                        await _unitOfWorkAsync.SaveChangesAsync();
                                    }
                                    _phbDoiChieuSoLieuService.Insert(doichieu);
                                    number = await _unitOfWorkAsync.SaveChangesAsync();
                                }
                                else
                                {

                                }
                            }
                          }
                        }
                    if (number > 0)
                    {
                        response.Error = false;
                        response.Data = number + "";
                        response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                    }
                    }
                    catch (NullReferenceException ex)
                    {
                        WriteLogs.LogError(ex);
                        response.Error = true;
                        response.Message = ErrorMessage.EMPTY_DATA;
                    }
                    catch (DbEntityValidationException ex)
                    {
                        WriteLogs.LogError(ex);
                        response.Error = true;
                        response.Message = ErrorMessage.ERROR_DATA;
                    }
                    catch (Exception ex)
                    {
                        WriteLogs.LogError(ex);
                        return Ok(new Response()
                        {
                            Error = true,
                            Message = ErrorMessage.ERROR_DATA
                        });
                    }
            }
            else
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
            }
            return Ok(response);
        }

        #region DeleteItem
        [Route("DeleteItem")]
        [HttpPost]
        public IHttpActionResult DeleteItem(DOICHIEUSOLIEUVm.DeleteItem model)
        {
            if (string.IsNullOrEmpty(model.MA_DVQHNS)) return BadRequest();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    var result = new Response<string>();

                    connection.Open();
                    var command = connection.CreateCommand();
                    OracleTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.Transaction = transaction;
                    command.CommandType = CommandType.Text;
                    try
                    {
                        command.CommandText = "DELETE FROM PHB_DOICHIEUSOLIEU WHERE MA_DVQHNS='" + model.MA_DVQHNS + "' AND NAM = " + model.NAM + " AND LOAI_DULIEU = '" + model.LOAI_DULIEU + "' AND CAP_DUTOAN = " + model.CAP_DUTOAN + " ";
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        result.Error = false;
                        result.Message = "Xóa thành công.";
                        return Ok(result);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        WriteLogs.LogError(ex);
                        result.Error = true;
                        result.Message = "Gặp lỗi trong quá trình thực hiện. Thử lại sau.";
                        return Ok(result);
                    }
                }
            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
                return InternalServerError();
            }
        }
        #endregion
    }
}