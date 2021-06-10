using System.Collections.Generic;

namespace APIIntegrationTest.Models
{
    public record PokeSearchResult
    {
        public IReadOnlyList<HeldItemResult> HeldItems { get; init; }
    }
}