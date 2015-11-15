using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Connectivity.Plugin;
using Fusillade;
using ModernHttpClient;
using Refit;
using VSLiveConnectedApp.Data;
using VSLiveConnectedApp.Services.Refit;

namespace VSLiveConnectedApp.Services
{
    public class ServiceClient
    {
        public static string ApiBaseAddress = "http://vslivesampleservice.azurewebsites.net";

        public ServiceClient()
        {
            Func<HttpMessageHandler, IConferenceApi> createClient = messageHandler =>
            {
                var client = new HttpClient(messageHandler)
                {
                    BaseAddress = new Uri(ApiBaseAddress)
                };

                return RestService.For<IConferenceApi>(client);
            };

            _background = new Lazy<IConferenceApi>(() => createClient(
                new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.Background)));

            _userInitiated = new Lazy<IConferenceApi>(() => createClient(
                new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.UserInitiated)));

            _speculative = new Lazy<IConferenceApi>(() => createClient(
                new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.Speculative)));
        }

        private readonly Lazy<IConferenceApi> _background;
        private readonly Lazy<IConferenceApi> _userInitiated;
        private readonly Lazy<IConferenceApi> _speculative;

        public IConferenceApi Background
        {
            get { return _background.Value; }
        }

        public IConferenceApi UserInitiated
        {
            get { return _userInitiated.Value; }
        }

        public IConferenceApi Speculative
        {
            get { return _speculative.Value; }
        }

        public async Task<List<City>> GetCities(bool isUserInitiated = false)
        {
            var cache = BlobCache.LocalMachine;
            var cachedCities = cache.GetAndFetchLatest("cities", GetRemoteCitiesAsync,
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return isUserInitiated || elapsed > new TimeSpan(hours: 0, minutes: 30, seconds: 0);
                });

            var cities = await cachedCities.FirstOrDefaultAsync();
            return cities;
        }

        private async Task<List<City>> GetRemoteCitiesAsync()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return await UserInitiated.GetCities();
            }
            return null;
        }

        public async Task<Schedule> GetScheduleForCity(string id, bool isUserInitiated)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var client = isUserInitiated ? UserInitiated : Speculative;
                    return await client.GetScheduleForCity(id);
                }
                return null;
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

