using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTS.SP.STC.Layouts
{
    /// <summary>
    /// Đối tượng đóng gói các thông báo khi thêm, sửa, xóa...
    /// </summary>
    /// <modified>
    /// Author				    created date					comments
    /// LongND					05/07/2013				        Tạo mới
    ///</modified>
    public class Message
    {
            public Message()
            {
                ExtData = new List<object>();
            }
        /// <summary>
        /// ID của bản ghi được thêm, sửa, xóa
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Thông báo
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Có lỗi hay không có lỗi
        /// </summary>
        public bool Error { get; set; }
        /// <summary>
        /// Đối tượng attach kèm theo thông báo
        /// </summary>
        public object Object { get; set; }

        public bool Status { get; set; }

        public object Data { get; set; }

        public List<object> ExtData { get; set; }

        public int? Code { get; set; }
    }
}
