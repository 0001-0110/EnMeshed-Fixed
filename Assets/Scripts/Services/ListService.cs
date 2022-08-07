using System;
using System.Collections.Generic;

namespace Services
{
    internal static class ListService
    {
        public static T Min<T>(List<T> list, Converter<T, float> key)
        {
            if (list.Count <= 0)
                throw new ArgumentException("You good ?");
            T min = list[0];
            foreach (T t in list)
            {
                if (key(t) < key(min))
                    min = t;
            }
            return min;
        }

        public static List<U> ForEach<T, U>(List<T> list, Func<T, U> converter)
        {
            List<U> result = new List<U>();
            foreach (T item in list)
                result.Add(converter(item));
            return result;
        }
    }
}