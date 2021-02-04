using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.Infrastructure.Models
{
    public class Pagination
    {
        public object Items { get; set; }
        public int TotalRecords { get; set; }
    }
}
