using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIIntegrationTest.Api.Responses
{
    public record PokeSearchResponse
    {
        [JsonPropertyName("held_items")]
        public IReadOnlyList<HeldItemResponse> HeldItems { get; init; }
    }
}