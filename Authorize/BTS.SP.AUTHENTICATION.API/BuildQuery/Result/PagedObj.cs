using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API.BuildQuery.Implimentations;

namespace BTS.SP.AUTHENTICATION.API.BuildQuery.Result
{
    public class PagedObj : PagedObj<object>
    {
    }
    public class PagedObj<T>
    {
        private List<T> _data;

        public List<T> Data
        {
            get { return _data ?? (_data = new List<T>()); }
            set { _data = value; }
        }

        public bool takeAll { get; set; }

        public int itemsPerPage
        {
            get
            {
                return takeAll
                    ? 0
                    : _itemsPerPage > 0 ? _itemsPerPage : DefaultPageSize;
            }
            set { _itemsPerPage = value; }
        }

        private int _itemsPerPage;

        public int currentPage
        {
            get { return _currentPage > 0 ? _currentPage : 1; }
            set { _currentPage = value; }
        }

        private int _currentPage;

        public int totalPages
        {
            get
            {
                return itemsPerPage > 0
                    ? (totalItems / itemsPerPage) + (totalItems % itemsPerPage == 0 ? 0 : 1)
                    : 1;
            }
        }

        public int totalItems { get; set; }

        public int fromItem
        {
            get { return (currentPage - 1) * itemsPerPage + 1; }
            internal set
            {
                currentPage = (value > 0 && itemsPerPage > 0)
                    ? (value - 1) / itemsPerPage + 1
                    : 1;
            }
        }

        public int toItem
        {
            get
            {
                var result = totalItems;
                if (itemsPerPage > 0)
                {
                    result = currentPage * itemsPerPage;
                    if (result > totalItems)
                    {
                        result = totalItems;
                    }
                }
                return result;
            }
        }


        public static int DefaultPageSize = 10;

        public IQueryBuilder ToQueryBuilder()
        {
            return new QueryBuilder
            {
                TakeAll = takeAll,
                Take = itemsPerPage,
                Skip = fromItem - 1
            };
        }

        public PagedObj<T> FromQueryBuilder(IQueryBuilder queryBuilder)
        {
            takeAll = queryBuilder.TakeAll;
            itemsPerPage = queryBuilder.Take;
            fromItem = queryBuilder.Skip + 1;
            return this;
        }
    }
    [Serializable]
    [DataContract]
    public class PagedObjTranf<T>
    {

        [DataMember]
        public List<T> Data
        {
            get;
            set;
        }
        [DataMember]
        public bool TakeAll { get; set; }
        [DataMember]
        public int itemsPerPage
        {
            get;
            set;
        }


        [DataMember]
        public int currentPage
        {
            get;
            set;
        }


        [DataMember]
        public int totalPages
        {
            get;
            set;
        }
        [DataMember]
        public int totalItems { get; set; }
        [DataMember]
        public int fromItem
        {
            get;
            set;
        }
        [DataMember]
        public int toItem
        {
            get;
            set;
        }

    }
}
