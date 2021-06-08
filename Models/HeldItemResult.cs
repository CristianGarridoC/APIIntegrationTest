using System.Collections.Generic;

namespace APIIntegrationTest.Models
{
    public record HeldItemResult
    {
        public string Name { get; init; }
        public IReadOnlyList<VersionDetailResult> VersionDetails { get; init; }
    }
}