using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq1
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> x, Func<T, bool> pre)
        {
            foreach (var item in x)
            {
                if (pre(item))
                {
                    yield return item;
                }
            }
        }
        //public static IEnumerable<T> MyWhere<T>(this T[] x, Func<T, bool> pre)
        //{
        //    foreach (var item in x)
        //    {
        //        if (pre(item))
        //        {
        //            yield return item;
        //        }
        //    }
        //}
    }
}
