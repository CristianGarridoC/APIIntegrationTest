using System.Text.Json.Serialization;

namespace APIIntegrationTest.Api.Responses
{
    public record ItemResponse
    {
        [JsonPropertyName("name")] 
        public string Name { get; init; }
    }
}