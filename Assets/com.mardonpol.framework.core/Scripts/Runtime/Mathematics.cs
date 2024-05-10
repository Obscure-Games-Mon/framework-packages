using UnityEngine;

namespace Framework.Mathematics
{
    public static class Mathematics
    {
        /// <summary>
        /// Maps a value given from `a` space to `b`.
        /// </summary>
        public static float MapValue(this float x, float a0, float a1, float b0, float b1)
        {
            float value = b0 + (b1 - b0) * ((x - a0) / (a1 - a0));
            return Mathf.Clamp(value, Mathf.Min(b0, b1), Mathf.Max(b0, b1));
        }
        
        /// <summary>
        /// Safely retrieves an element from an array of objects.
        /// </summary>
        /// <typeparam name="T">The type of the desired element.</typeparam>
        /// <param name="objectArray">The array of objects.</param>
        /// <param name="index">The index of the desired element.</param>
        /// <param name="defaultValue">The default value to return if the element is not found.</param>
        /// <returns>The element at the specified index or the default value.</returns>
        public static T SafeGetAt<T>(this object[] objectArray, int index, T defaultValue = default)
        {
            if (objectArray != null && objectArray.Length > index && index >= 0)
            {
                object obj = objectArray[index];

                if (obj is T obj1)
                {
                    return obj1;
                }
            }
            
            return defaultValue;
        }
        
        /// <summary>
        /// Safely retrieves an element from an array of generics.
        /// </summary>
        /// <typeparam name="T">The type of the desired element.</typeparam>
        /// <param name="objectArray">The array of generics.</param>
        /// <param name="index">The index of the desired element.</param>
        /// <param name="defaultValue">The default value to return if the element is not found.</param>
        /// <returns>The element at the specified index or the default value.</returns>
        public static T SafeGetAt<T>(this T[] objectArray, int index, T defaultValue = default)
        {
            if (objectArray != null && objectArray.Length > index && index >= 0)
            {
                object obj = objectArray[index];

                if (obj is T obj1)
                {
                    return obj1;
                }
            }
            
            return defaultValue;
        }
    }
}