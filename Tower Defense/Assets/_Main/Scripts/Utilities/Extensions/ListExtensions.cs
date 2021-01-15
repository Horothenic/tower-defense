using System;
using System.Collections.Generic;

namespace Utilities.Extensions
{
    public static class ListExtensions
    {
        #region BEHAVIORS

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int numberOfElements = list.Count;
            while (numberOfElements > 1)
            {
                numberOfElements--;
                int randomizedIndex = new Random().Next(numberOfElements + 1);
                T element = list[randomizedIndex];
                list[randomizedIndex] = list[numberOfElements];
                list[numberOfElements] = element;
            }

            return list;
        }

        #endregion
    }
}
