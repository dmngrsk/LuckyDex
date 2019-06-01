using System.Collections.Generic;
using System.Linq;

namespace LuckyDex.Api.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int size)
        {
            return source
                .Select((v, i) => new { v, groupIndex = i / size })
                .GroupBy(x => x.groupIndex)
                .Select(g => g.Select(x => x.v));
        }
    }
}
