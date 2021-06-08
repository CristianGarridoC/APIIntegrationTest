using System.Threading.Tasks;
using APIIntegrationTest.Api.Responses;
using Refit;

namespace APIIntegrationTest.Api
{
    public interface IPokeApi
    {
        [Get("/pokemon/{pokemonName}")]
        Task<PokeSearchResponse> SearchByNameAsync(string pokemonName);
    }
}