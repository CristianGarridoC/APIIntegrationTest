using System.Collections.Generic;

namespace APIIntegrationTest.Models
{
    public record PokeSearchErrorResult
    {
        public PokeSearchErrorResult(IReadOnlyList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
        public IReadOnlyList<string> ErrorMessages { get; }
    }
}