using System.Collections.Generic;

using Newtonsoft.Json;
using Utilities.Extensions;

namespace Utilities.Data
{
    public class UserData
    {
        #region FIELDS

        private const string IdKey = "id";
        private const string UsernameKey = "username";
        private const string DataKey = "data";

        #endregion

        #region PROPERTIES

        [JsonProperty(IdKey)] public string Id { get; internal set; } = null;
        [JsonProperty(UsernameKey)] public string Username { get; internal set; } = null;
        [JsonProperty(DataKey)] public Dictionary<string, object> Data { get; private set; } = null;

        #endregion

        #region CONSTRUCTORS

        public UserData() : this(null, null, null) { }

        public UserData(UserData user) : this(user.Id, user.Username, user.Data) { }

        [JsonConstructor]
        public UserData(string id = null, string username = null, Dictionary<string, object> data = null)
        {
            Id = id;
            Username = username;
            Data = data == null ? new Dictionary<string, object>() : data;
        }

        #endregion

        #region BEHAVIORS

        internal void SetData<T>(string[] keys, T data)
        {
            var userData = Data;
            userData.SetValue(keys, data);
        }

        internal T GetData<T>(string[] keys, object defaultValue)
        {
            var userData = Data;
            return userData.GetValue<T>(keys, defaultValue);
        }

        internal List<T> GetDataList<T>(string[] keys, object defaultValue)
        {
            var userData = Data;
            return userData.GetValueList<T>(keys, defaultValue);
        }

        internal void MergeData(Dictionary<string, object> newDictionary)
        {
            var userData = Data;
            userData.MergeValue(newDictionary);
        }

        internal bool HasData(string[] keys)
        {
            var userData = Data;
            return userData.HasValue(keys);
        }

        public bool HasInformation()
        {
            return string.IsNullOrEmpty(Username) || Data.Count > default(int);
        }

        #endregion
    }
}
