using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class PagedResultDTOs<T>
    {
        //public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        //public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
        public int TotalPages { get; set; } // Set manually
        public List<T> Items { get; set; } = new();
    }
}
