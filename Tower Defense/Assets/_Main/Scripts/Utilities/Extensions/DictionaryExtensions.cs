﻿using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Utilities.Extensions
{
    public static partial class DictionaryExtensions
    {
        #region BEHAVIORS

        public static bool ContainsKeys(this IDictionary<string, object> dictionary, string[] keys)
        {
            try
            {
                for (int i = 0; i < keys.Length; i++)
                    dictionary = dictionary[keys[i]] as Dictionary<string, object>;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return true;
        }

        public static void CreateKeys(this IDictionary<string, object> dictionary, string[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (!dictionary.ContainsKey(keys[i]))
                    dictionary.Add(keys[i], new Dictionary<string, object>());

                dictionary = dictionary[keys[i]] as Dictionary<string, object>;
            }
        }

        public static float IncreaseValue(this IDictionary<string, object> dictionary, string[] keys, float value)
        {
            float currentValue = dictionary.GetValue<float>(keys, default(float));
            dictionary.SetValue(keys, currentValue + value);
            return dictionary.GetValue<float>(keys, default(float));
        }

        public static float DecreaseValue(this IDictionary<string, object> dictionary, string[] keys, float value)
        {
            float currentValue = dictionary.GetValue<float>(keys, default(float));
            dictionary.SetValue(keys, currentValue - value);
            return dictionary.GetValue<float>(keys, default(float));
        }

        public static void SetValue(this IDictionary<string, object> dictionary, string[] keys, object newValue)
        {
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (!dictionary.ContainsKey(keys[i]))
                    dictionary.Add(keys[i], new Dictionary<string, object>());

                dictionary = (Dictionary<string, object>)dictionary[keys[i]];
            }

            if (dictionary.ContainsKey(keys.Last()))
                dictionary[keys.Last()] = newValue;
            else
                dictionary.Add(keys.Last(), newValue);
        }

        public static T GetValue<T>(this IDictionary<string, object> dictionary, string[] keys, object defaultValue)
        {
            defaultValue = defaultValue == null ? default(T) : defaultValue;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (!dictionary.ContainsKey(keys[i]))
                    return CastObject<T>(defaultValue);

                if (dictionary[keys[i]].GetType() != typeof(Dictionary<string, object>))
                    return CastObject<T>(defaultValue);

                dictionary = dictionary[keys[i]] as Dictionary<string, object>;
            }

            if (!dictionary.ContainsKey(keys.Last()))
                return CastObject<T>(defaultValue);

            return CastObject<T>(dictionary[keys.Last()]);
        }

        public static List<T> GetValueList<T>(this IDictionary<string, object> dictionary, string[] keys, object defaultValue)
        {
            var list = dictionary.GetValue<object>(keys, defaultValue);
            string listContent = JsonConvert.SerializeObject(list);
            List<T> newList = JsonConvert.DeserializeObject<List<T>>(listContent);
            return newList;
        }

        public static void MergeValue(this IDictionary<string, object> dictionary, IDictionary<string, object> newDictionary)
        {
            dictionary.MergeWithValue(newDictionary, new List<string>());
        }

        public static bool HasValue(this IDictionary<string, object> dictionary, string[] keys)
        {
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (!dictionary.ContainsKey(keys[i]))
                    return false;

                if (dictionary[keys[i]].GetType() != typeof(Dictionary<string, object>))
                    return false;

                dictionary = dictionary[keys[i]] as Dictionary<string, object>;
            }

            return dictionary.ContainsKey(keys.Last());
        }

        private static void MergeWithValue(this IDictionary<string, object> dictionary, object newElement, List<string> keys)
        {
            if (newElement.GetType() == typeof(Dictionary<string, object>))
            {
                foreach (KeyValuePair<string, object> element in (Dictionary<string, object>)newElement)
                {
                    var newKeys = new List<string>(keys);
                    newKeys.Add(element.Key);
                    dictionary.MergeWithValue(element.Value, newKeys);
                }
            }
            else
            {
                dictionary.SetValue(keys.ToArray(), newElement);
            }
        }

        private static T CastObject<T>(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        #endregion
    }
}
