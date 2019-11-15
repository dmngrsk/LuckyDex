using System;

namespace LuckyDex.Front.Extensions
{
    public static class LongExtensions
    {
        public static DateTimeOffset? ToNullableDateTimeOffset(this long? ticks)
        {
            return ticks.HasValue
                ? (DateTimeOffset?)DateTimeOffset.FromUnixTimeSeconds(ticks.Value)
                : null;
        }
    }
}
