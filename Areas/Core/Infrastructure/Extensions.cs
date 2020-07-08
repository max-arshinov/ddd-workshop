using System;

namespace DddWorkshop.Areas.Core.Infrastructure
{
    public static class Extensions
    {
        public static TResult PipeTo<T, TResult>(this T obj, Func<T, TResult> func)
            => func(obj);
    }
}