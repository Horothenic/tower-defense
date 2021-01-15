using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Utilities.Data
{
    public class DataManager : MonoBehaviour
    {
        #region FIELDS

        public const string UserDataKey = "UserData";
        public const string TemporalUserDataKey = "TemporalUserData";
        private const int DefaultLevel = 1;

        #endregion

        #region PROPERTIES

        public UserData User { get; private set; }
        public UserData TemporalUser { get; private set; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            LoadLocalData();
        }

        private void LoadLocalData()
        {
            User = JsonConvert.DeserializeObject<UserData>(PlayerPrefs.GetString(UserDataKey), new GenericConverter());
            TemporalUser = JsonConvert.DeserializeObject<UserData>(PlayerPrefs.GetString(TemporalUserDataKey), new GenericConverter());

            if (User == null)
                User = new UserData();

            if (TemporalUser == null)
                TemporalUser = new UserData();

            SaveLocalData();
        }

        public void ResetUser()
        {
            User = new UserData();
            TemporalUser = new UserData();
            SaveLocalData();
        }

        public void ResetTemporalUser()
        {
            TemporalUser = new UserData();
            SaveLocalData();
        }

        private void SaveLocalData()
        {
            PlayerPrefs.SetString(UserDataKey, SerializeUser(User));
            PlayerPrefs.SetString(TemporalUserDataKey, SerializeUser(TemporalUser));
        }

        public void DeleteUserData()
        {
            PlayerPrefs.DeleteKey(UserDataKey);
            PlayerPrefs.DeleteKey(TemporalUserDataKey);
        }

        internal string SerializeUser(object user)
        {
            return JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public void SetUser(UserData user)
        {
            if (user == null)
                return;

            User = user;
            TemporalUser = new UserData();
            SaveLocalData();
        }

        public void SetUsername(string username)
        {
            User.Username = username;
            SaveLocalData();
        }

        public void MergeData(Dictionary<string, object> dataDictionary)
        {
            User.MergeData(dataDictionary);
            TemporalUser.MergeData(dataDictionary);
            SaveLocalData();
        }

        public void SetData<T>(string[] keys, T data)
        {
            User.SetData(keys, data);
            TemporalUser.SetData(keys, data);
            SaveLocalData();
        }

        public void SetData<T>(string key, T data)
        {
            var keys = new string[] { key };
            SetData(keys, data);
        }

        public List<T> GetDataList<T>(UserData user, string[] keys, object defaultValue)
        {
            return user.GetDataList<T>(keys, defaultValue);
        }

        public List<T> GetDataList<T>(string[] keys, object defaultValue)
        {
            return User.GetDataList<T>(keys, defaultValue);
        }

        public T GetData<T>(UserData user, string[] keys, object defaultValue = null)
        {
            return user.GetData<T>(keys, defaultValue);
        }

        public T GetData<T>(string[] keys, object defaultValue = null)
        {
            return User.GetData<T>(keys, defaultValue);
        }

        public T GetData<T>(string key, object defaultValue = null)
        {
            var keys = new string[] { key };
            return User.GetData<T>(keys, defaultValue);
        }

        public bool HasData(string[] keys)
        {
            return User.HasData(keys);
        }

        public int GetId()
        {
            return int.Parse(User.Id);
        }

        public static void ResetKey(string[] keys, object newValue)
        {
            DataManager dataManager = new GameObject().AddComponent<DataManager>().GetComponent<DataManager>();
            dataManager.LoadLocalData();
            dataManager.SetData<object>(keys, newValue);
            DestroyImmediate(dataManager.gameObject);
        }

        public static string[] GenerateKeys(params object[] keys)
        {
            return keys.Select(x => x.ToString()).ToArray();
        }

        public static string[] GenerateKeys(string[] prefixKeys, params object[] keys)
        {
            List<string> keyList = new List<string>(prefixKeys);
            keyList.AddRange(GenerateKeys(keys));
            return keyList.ToArray();
        }

        public static void DeleteUsers()
        {
            PlayerPrefs.DeleteKey(UserDataKey);
            PlayerPrefs.DeleteKey(TemporalUserDataKey);
        }

        #endregion
    }
}
