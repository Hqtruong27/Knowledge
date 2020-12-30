using Knowledge.Web.API.UnitTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge.Web.API.UnitTest.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> AsAsyncQueryable<T>(this IEnumerable<T> input)
        {
            return new NotInDbSet<T>(input);
        }
    }
}
