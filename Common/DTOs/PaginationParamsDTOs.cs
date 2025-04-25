using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class PaginationParamsDTOs
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SearchTerm { get; set; } // for global search

        public int Skip => (PageNumber - 1) * PageSize;
    }
}
