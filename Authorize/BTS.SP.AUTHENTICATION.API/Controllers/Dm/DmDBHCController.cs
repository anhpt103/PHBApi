using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web;
using System.Net.Http.Headers;
using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;
using BTS.SP.AUTHENTICATION.API.ServiceFunc.DmDBHC;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Result;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Implimentations;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Types;
using BTS.SP.AUTHENTICATION.API.BuildQuery;
using BTS.SP.AUTHENTICATION.API.Dm.Entities;
using System.Security.Claims;

namespace BTS.SP.AUTHENTICATION.API.Controllers.Dm
{
    [RoutePrefix("api/Dm/DM_DBHC")]
    [Route("{id?}")]
    [Authorize]
    public class DmDBHController : ApiController
    {
        public readonly IDM_DBHCService _service;
        public DmDBHController(IDM_DBHCService service)
        {
            _service = service;
        }
        [Route("GetAll_DanhMucDiaBanHanhChinh/{maDBHC}")]
        [HttpGet]
        public IHttpActionResult GetAll_DanhMucDiaBanHanhChinh(string maDBHC)
        {
            var result = new TransferObj<IList<ChoiceObj>>();
            try
            {
                var repository = _service.Repository.DbSet.Where(x => x.MA_T.Equals(maDBHC)).ToList();
                var listData = Mapper.Map<List<DM_DBHC>, List<ChoiceObj>>(repository);
                if (listData.Count > 0)
                {
                    result.Status = true;
                    result.Data = listData;
                }
                else
                {
                    result.Status = false;
                    result.Data = null;
                }
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
                result.Data = null;
            }
            return Ok(result);
        }


        [Route("GetDataDBHCByUser/{maDBHC}")]
        [HttpGet]
        public IHttpActionResult GetDataDBHCByUser(string maDBHC)
        {
            var result = new TransferObj<IList<ChoiceObj>>();
            List<ChoiceObj> listData = new List<ChoiceObj>(); 
            try
            {
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = string.Format(@"SELECT A.* FROM DM_DBHC A WHERE A.MA_DBHC IN
                                    (SELECT MA_DBHC
                                    FROM DM_DBHC
                                    WHERE MA_DBHC IN
                                      (SELECT MA_DBHC
                                      FROM DM_DBHC
                                      WHERE MA_DBHC  = '{0}'
                                      OR MA_DBHC_CHA = '{0}'
                                      )
                                    OR MA_DBHC_CHA IN
                                      (SELECT MA_DBHC
                                      FROM DM_DBHC
                                      WHERE MA_DBHC  = '{0}'
                                      OR MA_DBHC_CHA = '{0}'
                                      )
                                    ) ORDER BY A.MA_DBHC", maDBHC);
                        using (var oracleDataReader = command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            if (!oracleDataReader.Result.HasRows)
                            {
                                result.Data = null;
                            }
                            else
                            {
                                while (oracleDataReader.Result.Read())
                                {
                                    ChoiceObj obj = new ChoiceObj();
                                    obj.Value = oracleDataReader.Result["MA_DBHC"]?.ToString();
                                    obj.Text = oracleDataReader.Result["TEN_DBHC"]?.ToString();
                                    obj.Parent = oracleDataReader.Result["MA_DBHC_CHA"]?.ToString();
                                    listData.Add(obj);
                                }
                            }
                        }
                    }
                }
                if (listData.Count > 0)
                {
                    result.Status = true;
                    result.Message = "Oke";
                    result.Data = listData;
                }
                else
                {
                    result.Status = false;
                    result.Message = "Null";
                    result.Data = null;
                }
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
                result.Data = null;
            }
            return Ok(result);
        }


        [Route("Select_Page")]
        public async Task<IHttpActionResult> Select_Page(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DM_DBHCVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<DM_DBHC>>();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var currentMA_DBHC = identity.Claims.FirstOrDefault(c => c.Type == "MA_DBHC").Value.ToString();
            var crrDBHC = _service.Repository.DbSet.FirstOrDefault(x => x.MA_DBHC == currentMA_DBHC);
            if (crrDBHC!=null)
            {
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
                                Property = ClassHelper.GetProperty(() => new DM_DBHC().MA_DBHC),
                                Method = FilterMethod.EqualTo,
                                Value = filtered.AdvanceData.MA_DBHC
                            },
                            new QueryFilterLinQ
                            {
                                Property = ClassHelper.GetProperty(() => new DM_DBHC().MA_T),
                                Method = FilterMethod.EqualTo,
                                Value = crrDBHC.MA_T,
                            },
                        },
                        Method = FilterMethod.And
                    },
                    Orders = new List<IQueryOrder>()
                {
                    new QueryOrder()
                    {
                        Field ="MA_DBHC",
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
            else {
                return InternalServerError();
            }

        }
        [Route("GetDataByCode/{code}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDataByCode(string code)
        {
            var result = new TransferObj();
            var instance = _service.UnitOfWork.Repository<DM_DBHC>().DbSet.FirstOrDefault(x => x.MA_DBHC == code);
            if (instance == null)
            {
                result.Data = null;
                result.Status = false;
            }
            else
            {
                result.Data = instance;
                result.Status = true;
            }
            return Ok(result);
        }
        [Route("AddNew")]
        [ResponseType(typeof(DM_DBHC))]
        public async Task<IHttpActionResult> AddNew(DM_DBHC instance)
        {
            var result = new TransferObj<DM_DBHC>();
            try
            {
                Guid id = Guid.NewGuid();
                instance.Id = id.ToString();
                var item = _service.Insert(instance);
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Message = "Thêm mới thành công!";
                result.Data = item;
                return Ok(result);
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
                return Ok(result);
            }
            return CreatedAtRoute("DefaultApi", new { controller = this, id = instance.Id }, result);
        }

        [Route("GetDataXaByCode/{code}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDataXaByCode(string code)
        {
            var result = new TransferObj();
            //var currentDBHC = 
            var temp = _service.UnitOfWork.Repository<DM_DBHC>().DbSet.Where(x => x.MA_DBHC == code).FirstOrDefault();
            if (temp == null)
            {
                result.Status = false;
                result.Data = null;
            }
            else
            {
                if (temp.LOAI == 2)
                {
                    var resultData = new List<DM_DBHC>();
                    var instance = _service.UnitOfWork.Repository<DM_DBHC>().DbSet.Where(x => x.MA_DBHC_CHA == code).ToList(); // ra huyen
                    if (instance == null)
                    {
                        result.Data = null;
                        result.Status = false;
                        return Ok(result);
                    }

                    foreach (var item in instance)
                    {
                        resultData.Add(item);
                        // resultData.AddRange(_service.UnitOfWork.Repository<DM_DBHC>().DbSet.Where(x => x.MA_DBHC_CHA == item.MA_DBHC).ToList());
                    }
                    result.Data = resultData;
                    result.Status = true;
                }
                else if (temp.LOAI == 3)
                {
                    var instance = _service.UnitOfWork.Repository<DM_DBHC>().DbSet.Where(x => x.MA_DBHC_CHA == code).ToList();
                    if (instance == null)
                    {
                        result.Data = null;
                        result.Status = false;
                    }
                    else
                    {
                        result.Data = instance;
                        result.Status = true;
                    }
                }
            }
            return Ok(result);
        }

        [Route("getListDataByCode/{code}")]
        [HttpGet]
        public async Task<IHttpActionResult> getListDataByCode(string code)
        {
            var result = new TransferObj();
            var instance = _service.UnitOfWork.Repository<DM_DBHC>().DbSet.Where(x => x.MA_DBHC.Contains(code)).ToList();
            if (instance.Count == 0)
            {
                result.Data = null;
                result.Status = false;
            }
            else
            {
                result.Data = instance;
                result.Status = true;
            }
            return Ok(result);
        }

        [Route("getListDataByName/{name}")]
        [HttpGet]
        public async Task<IHttpActionResult> getListDataByName(string name)
        {
            var result = new TransferObj();
            var instance = _service.UnitOfWork.Repository<DM_DBHC>().DbSet.Where(x => x.TEN_DBHC.ToLower().Contains(name.ToLower())).ToList();
            if (instance.Count == 0)
            {
                result.Data = null;
                result.Status = false;
            }
            else
            {
                result.Data = instance;
                result.Status = true;
            }
            return Ok(result);
        }


        [Route("Update/{id}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> Update(string id, DM_DBHC instance)
        {
            var result = new TransferObj<DM_DBHC>();
            if (id != instance.Id)
            {
                result.Status = false;
                result.Message = "Id không hợp lệ";
                return Ok(result);
            }
            try
            {
                var item = _service.Update(instance);
                _service.UnitOfWork.Save();
                result.Status = true;
                result.Message = "Sửa thành công!";
                result.Data = item;
                return Ok(result);
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Message = e.Message;
                return Ok(result);
            }
        }


        [Route("DeleteItem/{id}")]
        [ResponseType(typeof(DM_DBHC))]
        [HttpPut]
        public async Task<IHttpActionResult> DeleteItem(string Id)
        {
            //int id = int.Parse(Id);
            DM_DBHC instance = _service.FindById(Id);
            if (instance == null)
            {
                return NotFound();
            }
            try
            {
                _service.Delete(instance.Id);
                await _service.UnitOfWork.SaveAsync();
                return Ok(instance);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        //[Route("Export")]
        //[AllowAnonymous]
        //public HttpResponseMessage Export()
        //{
        //    HttpResponseMessage result = null;
        //    string file = null;

        //    try
        //    {

        //        file = CreateExcelFile();
        //        if (!string.IsNullOrEmpty(file))
        //        {
        //            if (!File.Exists(file))
        //            {
        //                result = Request.CreateResponse(HttpStatusCode.NoContent);
        //            }
        //            else
        //            {
        //                result = Request.CreateResponse(HttpStatusCode.OK);
        //                result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
        //                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //                result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
        //            }
        //        }


        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //private string CreateExcelFile()
        //{
        //    var lst = new List<DM_DBHCVm.DM_DBHCs>();
        //    var data = _service.Repository.DbSet;
        //    lst = data.Select(x => new DM_DBHCVm.DM_DBHCs { MA_DBHC = x.MA_DBHC, TEN_DBHC = x.TEN_DBHC, MA_DBHC_CHA = x.MA_DBHC_CHA, NGAY_HL = x.NGAY_HL }).OrderBy(x => x.MA_DBHC).ToList();
        //    DateTime now = DateTime.Now;
        //    string date = now.ToString("dd-MM-yyyy");
        //    var urlFile = HttpContext.Current.Server.MapPath("~/UploadFile/ExcelTemp/");
        //    var filename = urlFile + "Export_DM_DBHC_(" + date + ")_TM" + now.Ticks + ".xls";
        //    if (lst.Count > 0)
        //    {
        //        FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
        //        using (var excelPackage = new ExcelPackage())
        //        {
        //            excelPackage.Workbook.Properties.Author = "SystemAccount";
        //            excelPackage.Workbook.Properties.Title = "Export_DM_TKKB";
        //            excelPackage.Workbook.Worksheets.Add(date);
        //            var workSheet = excelPackage.Workbook.Worksheets[1];
        //            BindingDataToExcel(workSheet, lst);

        //            excelPackage.SaveAs(file);
        //            file.Close();
        //        }
        //    }
        //    else
        //    {
        //        filename = "";
        //    }

        //    return filename;

        //}


        //private void BindingDataToExcel(ExcelWorksheet ws, List<DM_DBHCVm.DM_DBHCs> lst)
        //{
        //    ws.Cells[1, 1].Value = "STT";
        //    ws.Cells[1, 2].Value = "Mã địa bàn";
        //    ws.Cells[1, 3].Value = "Tên địa bàn";
        //    ws.Cells[1, 4].Value = "Mã địa bàn cha";
        //    ws.Cells[1, 5].Value = "Ngày hiệu lực";

        //    ws.Cells[1, 1, 1, 5].Style.Font.Size = 12;
        //    ws.Cells[1, 1, 1, 5].Style.Font.Bold = true;
        //    ws.Cells[1, 1, 1, 5].Style.Font.Name = "Time New Roman";
        //    ws.Cells[1, 1, 1, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //    var NumOfRow = (lst.Count + 1).ToString();
        //    string modelRange = "A1:E" + NumOfRow;
        //    var modelTable = ws.Cells[modelRange];

        //    // Assign borders
        //    modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //    modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //    modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //    modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

        //    modelTable.Style.Font.Name = "Time New Roman";

        //    int r = 2;
        //    for (int i = 0; i < lst.Count; i++)
        //    {
        //        var item = lst[i];
        //        ws.Cells[r + i, 1].Value = i + 1;
        //        ws.Cells[r + i, 2].Value = item.MA_DBHC;
        //        ws.Cells[r + i, 3].Value = item.TEN_DBHC;
        //        ws.Cells[r + i, 4].Value = item.MA_DBHC_CHA;
        //        ws.Cells[r + i, 5].Value = item.NGAY_HL.ToShortDateString();
        //    }
        //    ws.Column(1).AutoFit();
        //    ws.Column(2).AutoFit();
        //    ws.Column(3).AutoFit();
        //    ws.Column(4).AutoFit();
        //    ws.Column(5).AutoFit();

        //}
    }
}