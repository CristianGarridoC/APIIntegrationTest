using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIIntegrationTest.Api.Responses
{
    public record HeldItemResponse
    { 
        [JsonPropertyName("item")]
        public ItemResponse Item { get; init; }
        
        [JsonPropertyName("version_details")]
        public IReadOnlyList<VersionDetailResponse> VersionDetails { get; init; }
    }
}