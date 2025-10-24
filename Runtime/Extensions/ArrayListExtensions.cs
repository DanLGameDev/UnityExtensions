using System.Collections.Generic;
using DGP.UnityExtensions.Helpers;

namespace DGP.UnityExtensions
{
    public static class ArrayListExtensions
    {
        public static T[] Shuffle<T>(this T[] items, RandomStream randomStream = null)
        {
            if (items == null) return null;
    
            var array = items.Clone() as T[];
            var n = array.Length;
    
            while (n > 1)
            {
                var k = randomStream?.Range(0, n--) ?? UnityEngine.Random.Range(0, n--);
                
                (array[n], array[k]) = (array[k], array[n]);
            }
            return array;
        }

        public static List<T> Shuffle<T>(this List<T> items, RandomStream randomStream = null)
        {
            if (items == null) return null;
    
            var list = new List<T>(items);
            var n = list.Count;
    
            while (n > 1)
            {
                var k = randomStream?.Range(0, n--) ?? UnityEngine.Random.Range(0, n--);
                (list[n], list[k]) = (list[k], list[n]);
            }
            return list;
        }
        
        public static T RandomItem<T>(this T[] items, RandomStream randomStream = null)
        {
            if (items == null || items.Length == 0) return default;
            var index = randomStream?.Range(0, items.Length) ?? UnityEngine.Random.Range(0, items.Length);
            return items[index];
        }
        
        public static T RandomItem<T>(this List<T> items, RandomStream randomStream = null)
        {
            if (items == null || items.Count == 0) return default;
            var index = randomStream?.Range(0, items.Count) ?? UnityEngine.Random.Range(0, items.Count);
            return items[index];
        }
        
        /// <summary>
        /// Removes an item at the specified index by swapping it with the last item,
        /// then removing the last item. O(1) operation but does not preserve order.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        public static void RemoveAtSwapBack<T>(this List<T> list, int index)
        {
            int lastIndex = list.Count - 1;
            list[index] = list[lastIndex];
            list.RemoveAt(lastIndex);
        }
    }
}