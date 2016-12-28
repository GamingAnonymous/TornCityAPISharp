﻿using System.Threading.Tasks;
using TornCityAPISharp.Utils;
using TornCityAPISharp.Enums;
using TornCityAPISharp.FactionStats;

namespace TornCityAPISharp
{
    public class TornApiWrapper
    {
        #region Accessors
        public int MaxNumberOfCalls { get; set; }

        public API ApiKey
        {
            get
            {
                return _apiKey;
            }

            private set
            {
                this._apiKey = value;
            }
        }

        public PlayerStatistics PlayerStatistics { get; private set; }

        public PropertyStatistics PropertyStatistics { get; private set; }
        #endregion

        #region Mutators
        private API _apiKey;
        private GetObjectFromUrl apiCaller = new GetObjectFromUrl();
        private string urlBase = @"https://api.torn.com/";
        #endregion

        #region Constructors
        public TornApiWrapper(string apiKey, int maxNumberOfCalls)
        {
            ApiKey = new API(apiKey);
           
            MaxNumberOfCalls = maxNumberOfCalls;
        }

        #endregion

        #region Methods
        public void ChangeApiKey(string apiKey)
        {
            this.ApiKey.ApiKey = apiKey;
        }

        public async Task<T> GetFromFactionApi<T>(string factionID) where T:IFactionStatistics,new()
        {
            var method = new T();
            string url = urlBase + Fields.faction.ToString() + "/"   + factionID + "?selections="+ method.GetMethodName() +"&key=" + _apiKey.ApiKey;
            
            var response = await apiCaller.GetObject<T>(url);
            _apiKey.ApiCallCount++;

            return response;
        }
        #endregion
    }
}
