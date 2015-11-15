using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using VSLiveConnectedApp.Data;

namespace VSLiveConnectedApp.Services.Refit
{
    [Headers("Accent: application/json")]
    public interface IConferenceApi
    {
        [Get("/api/cities")]
        Task<List<City>> GetCities();

        [Get("/api/schedule/{id}")]
        Task<Schedule> GetScheduleForCity(string id);
    }
}
