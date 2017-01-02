﻿using System;
using System.Threading.Tasks;
using TornCityAPISharp.Utils;
using TornCityAPISharp.Enums;
using TornCityAPISharp.FactionStats;
using TornCityAPISharp.PropertyStats;
using TornCityAPISharp.CompanyStats;
using TornCityAPISharp.MarketStats;
using TornCityAPISharp.UserStats;
using TornCityAPISharp.TornStats;

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

        public async Task<T> GetFromPropertyApi<T>(string propertyId) where T : IPropertyStatistics, new()
        {
            var method = new T();
            if(string.IsNullOrWhiteSpace(propertyId) == true)
            {
                throw new ArgumentException("Property id cannot be null of whitespace");
            }

            string url = urlBase + Fields.property.ToString() + "/" + propertyId + "?selections=" + method.GetMethodName() + "&key=" + _apiKey.ApiKey;

            var response = await apiCaller.GetObject<T>(url);
            _apiKey.ApiCallCount++;

            return response;
        }

        public async Task<T> GetFromCompanyApi<T>(string propertyId) where T : ICompanyStatistics, new()
        {
            var method = new T();

            string url = urlBase + Fields.company.ToString() + "/" + propertyId + "?selections=" + method.GetMethodName() + "&key=" + _apiKey.ApiKey;

            var response = await apiCaller.GetObject<T>(url);
            _apiKey.ApiCallCount++;

            return response;
        }

        public async Task<T> GetFromMarketApi<T>(string itemId) where T : IMarketStatistics, new()
        {
            var method = new T();

            string url = urlBase + Fields.market.ToString() + "/" + itemId + "?selections=" + method.GetMethodName() + "&key=" + _apiKey.ApiKey;

            var response = await apiCaller.GetObject<T>(url);
            _apiKey.ApiCallCount++;

            return response;
        }

        public async Task<T> GetFromUserApi<T>(string itemId) where T : IUserStatistics, new()
        {
            var method = new T();

            string url = urlBase + Fields.user.ToString() + "/" + itemId + "?selections=" + method.GetMethodName() + "&key=" + _apiKey.ApiKey;

            var response = await apiCaller.GetObject<T>(url);
            _apiKey.ApiCallCount++;

            return response;
        }

        public async Task<T> GetFromTornApi<T>(string parameter) where T : ITornStats, new()
        {
            var method = new T();

            string url = urlBase + Fields.torn.ToString() + "/" + parameter + "?selections=" + method.GetMethodName() + "&key=" + _apiKey.ApiKey;

            var response = await apiCaller.GetObject<T>(url);
            _apiKey.ApiCallCount++;

            return response;
        }
        #endregion
    }
}
