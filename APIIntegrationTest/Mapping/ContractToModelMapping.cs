using System.Linq;
using APIIntegrationTest.Api.Responses;
using APIIntegrationTest.Models;

namespace APIIntegrationTest.Mapping
{
    public static class ContractToModelMapping
    {
        public static HeldItemResult ToHeldItemResult(this HeldItemResponse heldItemResponse)
        {
            return new()
            {
                Name = heldItemResponse.Item.Name,
                VersionDetails = heldItemResponse.VersionDetails.Select(e => e.ToVersionDetailResult()).ToList()
            };
        }

        private static VersionDetailResult ToVersionDetailResult(this VersionDetailResponse versionDetailResponse)
        {
            return new()
            {
                Rarity = versionDetailResponse.Rarity,
                Name = versionDetailResponse.Version.Name
            };
        }
    }
}