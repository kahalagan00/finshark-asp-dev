using api.Interfaces;
using api.Models;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;
        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            
        }
        public Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
