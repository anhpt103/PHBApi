using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BTS.SP.TOOLS;
using BTS.SP.TOOLS.BuildQuery;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using BTS.SP.TOOLS.BuildQuery.Result.Types;
using BTS.SP.TOOLS.BuildQuery.Types;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System.Linq.Dynamic;
using BTS.SP.PHB.ENTITY;
using System.Collections.Generic;
using BTS.SP.PHB.ENTITY.Auth;
using System.Security.Claims;
using System.Web;

namespace BTS.SP.PHB.SERVICE.SERVICES
{
    public interface IBaseService<T> : IService<T> where T : BaseEntity
    {
        T FindById(long id);
        Task<T> FindByIdAsync(long id);
        ResultObj<PagedObj<T>> Filter<TSearch>(FilterObj<TSearch> filtered, IQueryBuilder query = null) where TSearch : IDataSearch;

        Task<ResultObj<PagedObj<T>>> FilterAsync<TSearch>(FilterObj<TSearch> filtered, IQueryBuilder query = null) where TSearch : IDataSearch;

        List<T> GetAll();
        AU_NGUOIDUNG GetCurrentUser();
    }
    public class BaseService<T>: Service<T>,IBaseService<T> where T: BaseEntity
    {
        private readonly IRepositoryAsync<T> _repository;
        public BaseService(IRepositoryAsync<T> repository) : base(repository)
        {
            _repository = repository;
        }

        public T FindById(long id)
        {
            return _repository.Queryable().FirstOrDefault(x => x.ID == id);
        }

        public async Task<T> FindByIdAsync(long id)
        {
            return await _repository.Queryable().FirstOrDefaultAsync(x => x.ID == id);
        }

        public ResultObj<PagedObj<T>> Filter<TSearch>(FilterObj<TSearch> filtered, IQueryBuilder query = null) where TSearch : IDataSearch
        {
            query = query ?? new QueryBuilder();
            var advanceData = filtered.AdvanceData;
            if (!filtered.IsAdvance)
            {
                advanceData.LoadGeneralParam(filtered.Summary);
            }
            var filters = advanceData.GetFilters();
            if (filters.Count > 0)
            {
                var newQuery = new QueryFilterLinQ
                {
                    Method = filtered.IsAdvance ? FilterMethod.And : FilterMethod.Or,
                    SubFilters = filters,
                };
                if (query.Filter == null)
                {
                    query.Filter = newQuery;
                }
                else
                {
                    query.Filter.MergeFilter(newQuery);
                }
            }
            var quickFilters = advanceData.GetQuickFilters();
            if (quickFilters != null && quickFilters.Any())
            {
                var newQuery = new QueryFilterLinQ
                {
                    Method = FilterMethod.And,
                    SubFilters = quickFilters,
                };
                if (query.Filter == null)
                {
                    query.Filter = newQuery;
                }
                else
                {
                    query.Filter.MergeFilter(newQuery);
                }
            }
            // load order 
            if (!string.IsNullOrEmpty(filtered.OrderBy))
            {
                query.OrderBy(new QueryOrder
                {
                    Field = filtered.OrderBy,
                    MethodName = filtered.OrderType
                });
            }
            // at lease one order for paging
            if (query.Orders.Count == 0)
            {
                query.OrderBy(new QueryOrder { Field = advanceData.DefaultOrder });
            }
            // query
            var result = new ResultObj<PagedObj<T>>();
            try
            {
                var data = QueryPaged(query);
                result.Value = data;
                result.State = ResultState.Success;
            }
            catch (Exception ex)
            {
                result.SetException = ex;
            }
            return result;
        }

        private PagedObj<T> QueryPaged(IQueryBuilder query)
        {
            var result = new PagedObj<T>();
            IQueryable<T> data = _repository.Queryable();
            var filterString = query.BuildFilter();
            var orderString = query.BuildOrder();
            if (!string.IsNullOrEmpty(filterString))
                data = data.Where(filterString, query.Filter.QueryStringParams.Params.ToArray());
            if (!string.IsNullOrEmpty(orderString))
                data = data.OrderBy(orderString);
            result.totalItems = data.Count();
            if (query.Skip > 0)
            {
                data = data.Skip(query.Skip);
            }
            if (query.Take > 0)
            {
                data =data.Take(query.Take);
            }
            result.Data.AddRange(data.ToList());
            return result;
        }

        public async Task<ResultObj<PagedObj<T>>> FilterAsync<TSearch>(FilterObj<TSearch> filtered,IQueryBuilder query = null) where TSearch : IDataSearch
        {
            query = query ?? new QueryBuilder();
            var advanceData = filtered.AdvanceData;
            if (!filtered.IsAdvance)
            {
                advanceData.LoadGeneralParam(filtered.Summary);
            }
            var filters = advanceData.GetFilters();
            if (filters.Count > 0)
            {
                var newQuery = new QueryFilterLinQ
                {
                    Method = filtered.IsAdvance ? FilterMethod.And : FilterMethod.Or,
                    SubFilters = filters,
                };
                if (query.Filter == null)
                {
                    query.Filter = newQuery;
                }
                else
                {
                    query.Filter.MergeFilter(newQuery);
                }
            }
            var quickFilters = advanceData.GetQuickFilters();
            if (quickFilters != null && quickFilters.Any())
            {
                var newQuery = new QueryFilterLinQ
                {
                    Method = FilterMethod.And,
                    SubFilters = quickFilters,
                };
                if (query.Filter == null)
                {
                    query.Filter = newQuery;
                }
                else
                {
                    query.Filter.MergeFilter(newQuery);
                }
            }
            // load order 
            if (!string.IsNullOrEmpty(filtered.OrderBy))
            {
                query.OrderBy(new QueryOrder
                {
                    Field = filtered.OrderBy,
                    MethodName = filtered.OrderType
                });
            }
            // at lease one order for paging
            if (query.Orders.Count == 0)
            {
                query.OrderBy(new QueryOrder { Field = advanceData.DefaultOrder });
            }
            // query
            var result = new ResultObj<PagedObj<T>>();
            try
            {
                var data = await QueryPagedAsync(query);
                result.Value = data;
                result.State = ResultState.Success;
            }
            catch (Exception ex)
            {
                result.SetException = ex;
            }
            return result;
        }

        private async Task<PagedObj<T>> QueryPagedAsync(IQueryBuilder query)
        {
            var result = new PagedObj<T>();
            IQueryable<T> data = _repository.Queryable();
            var filterString = query.BuildFilter();
            var orderString = query.BuildOrder();
            if (!string.IsNullOrEmpty(filterString))
                data = data.Where(filterString, query.Filter.QueryStringParams.Params.ToArray());
            if (!string.IsNullOrEmpty(orderString))
                data = data.OrderBy(orderString);
            result.totalItems = await data.CountAsync();
            if (query.Skip > 0)
            {
                data = data.Skip(query.Skip);
            }
            if (query.Take > 0)
            {
                data = data.Take(query.Take);
            }
            result.Data.AddRange(await data.ToListAsync());
            return result;
        }

        public List<T> GetAll()
        {
            var result = new List<T>();
            result = _repository.Queryable().ToList();
            return result;
        }

        private AU_NGUOIDUNG GetCurrentUser()
        {
            try
            {
                var cur = new AU_NGUOIDUNG();
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                if (identity != null && !string.IsNullOrEmpty(identity.Name))
                {
                    cur.USERNAME = identity.Name;
                    cur.MA_DBHC = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"))?.Value;
                    cur.MA_DONVI = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DONVI"))?.Value;

                }
                return cur;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        AU_NGUOIDUNG IBaseService<T>.GetCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}
