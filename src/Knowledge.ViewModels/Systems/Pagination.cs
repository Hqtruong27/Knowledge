using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.ViewModels.Systems
{
    public class Pagination<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
    }
}
