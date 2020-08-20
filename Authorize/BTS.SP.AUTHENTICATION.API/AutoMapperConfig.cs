using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Result;

namespace BTS.SP.AUTHENTICATION.API
{ 
    public static class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.CreateMap(typeof(PagedObj), typeof(PagedObj));
            Mapper.CreateMap(typeof(PagedObj<>), typeof(PagedObj<>));
        }
        public static IMappingExpression<TSource, TDestination> IgnoreDataInfoSelfMapping<TSource, TDestination>(
           this IMappingExpression<TSource, TDestination> map)
            where TSource : DataInfoEntity
            where TDestination : DataInfoEntity
        {
            return map;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreDataInfo<TSource, TDestination>(
          this IMappingExpression<TSource, TDestination> map)
            where TSource : DataDto
            where TDestination : DataInfoEntity
        {
            return map;
        }

        public class IdResolver : ValueResolver<string, string>
        {
            protected override string ResolveCore(string source)
            {
                return Guid.NewGuid().ToString();
            }
        }

        public class UpdateDateResolver : ValueResolver<DateTime?, DateTime?>
        {
            protected override DateTime? ResolveCore(DateTime? source)
            {
                return DateTime.Now;
            }
        }

        public class UpdateByResolver : ValueResolver<string, string>
        {
            protected override string ResolveCore(string source)
            {
                var result = source;
                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
                    result = HttpContext.Current.User.Identity.Name;
                return result;
            }
        }

    }
}
