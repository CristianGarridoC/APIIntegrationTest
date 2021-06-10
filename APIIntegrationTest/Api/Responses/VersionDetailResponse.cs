using System.Text.Json.Serialization;

namespace APIIntegrationTest.Api.Responses
{
    public record VersionDetailResponse
    {
        [JsonPropertyName("rarity")] 
        public int Rarity { get; init; }
        
        [JsonPropertyName("version")]
        public ItemResponse Version { get; init; }
    }
}