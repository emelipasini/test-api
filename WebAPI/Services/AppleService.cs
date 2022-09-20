using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IAppleService
    {
        public Task<GeoserverResponse<Apple>> FindById(int id);
        public Task<GeoserverResponse<Apple>> FindByDate(string date);
    }

    public class AppleService : IAppleService
    {
        private readonly IConfiguration Configuration;
        private readonly HttpClient _httpClient;

        public AppleService(IConfiguration configuration, HttpClient httpClient)
        {
            Configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<GeoserverResponse<Apple>> FindById(int id)
        {
            try
            {
                string url = $"{Configuration["Callings:apples"]}&CQL_Filter=(id={id})";
                var response = await _httpClient.GetAsync(url);
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new GeoserverResponse<Apple>() { features = new List<Feature<Apple>>() };
                }
                var responseContent = response.Content;
                var apple = await responseContent.ReadFromJsonAsync<GeoserverResponse<Apple>>();
                if(apple != null)
                {
                    return apple;
                }
                return new GeoserverResponse<Apple>() { features = new List<Feature<Apple>>() };
            }
            catch (Exception err)
            {
                Console.Write($"Ha ocurrido un error. {err.Message}");
                return new GeoserverResponse<Apple>() { features = new List<Feature<Apple>>() };
            }
        }

        public async Task<GeoserverResponse<Apple>> FindByDate(string date)
        {
            try
            {
                string url = $"{Configuration["Callings:apples"]}&CQL_Filter=(fecha_modif AFTER {date})";
                var response = await _httpClient.GetAsync(url);
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new GeoserverResponse<Apple>() { features = new List<Feature<Apple>>() };
                }
                var responseContent = response.Content;
                var apples = await responseContent.ReadFromJsonAsync<GeoserverResponse<Apple>>();
                if(apples != null)
                {
                    return apples;
                }
                return new GeoserverResponse<Apple>() { features = new List<Feature<Apple>>() };
            }
            catch (Exception err)
            {
                Console.Write($"Ha ocurrido un error. {err.Message}");
                return new GeoserverResponse<Apple>() { features = new List<Feature<Apple>>() };
            }
        }
    }
}

