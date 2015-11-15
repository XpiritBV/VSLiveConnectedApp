using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using ModernHttpClient;
using Refit;
using VSLiveConnectedApp.Data;
using VSLiveConnectedApp.Services.Refit;

namespace VSLiveConnectedApp.Services
{
    public class ServiceClient
    {
        public static string ApiBaseAddress = "http://vslivesampleservice.azurewebsites.net";

        private IConferenceApi _client;

        public ServiceClient()
        {
            var client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };
            _client = RestService.For<IConferenceApi>(client);
        }

        public async Task<List<City>> GetCities()
        {
            var cache = BlobCache.LocalMachine;
            var cachedCities = cache.GetAndFetchLatest("cities", GetRemoteCitiesAsync,
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return elapsed > new TimeSpan(hours: 0, minutes: 30, seconds: 0);
                });

            var cities = await cachedCities.FirstOrDefaultAsync();
            return cities;
        }

        public async Task<List<City>> GetRemoteCitiesAsync()
        {
            return await _client.GetCities();
        }

        public async Task<Schedule> GetScheduleForCity(string id)
        {
            try
            {
                return await _client.GetScheduleForCity(id);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }
    }
}

