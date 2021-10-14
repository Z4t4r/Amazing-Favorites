using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Search.AutoSuggest;
using Microsoft.Extensions.Logging;

namespace Newbe.BookmarkManager.Services
{
    public class BingSuggestService:IBingSuggestService
    {

        private readonly AutoSuggestClient _autoSuggestClient;
        private readonly ILogger<BingSuggestService> _logger;

        public BingSuggestService()
        {
            var credentials =
                new Microsoft.Azure.CognitiveServices.Search.AutoSuggest.ApiKeyServiceClientCredentials("");
            _autoSuggestClient = new AutoSuggestClient(credentials, new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = ""
            };
        }
        
        public async Task TestAsync()
        {
            var result = await _autoSuggestClient.AutoSuggestMethodAsync("xb");
            var groups = result.SuggestionGroups;
            if (!groups.Any())
            {
                _logger.LogInformation("resultEmpty");
            }

            foreach (var suggest in groups)
            {
                _logger.LogInformation("Suggest:");
                _logger.LogInformation(JsonSerializer.Serialize(suggest));
            }
            
        }
    }
}