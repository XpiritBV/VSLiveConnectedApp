using Newtonsoft.Json;
using VSLiveConnectedApp.Services;

namespace VSLiveConnectedApp.Data
{
    public class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string ImageUrl
        {
            get { return ServiceClient.ApiBaseAddress + Logo; }
        }
    }
}
