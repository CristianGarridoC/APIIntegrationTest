using System.Threading.Tasks;
using APIIntegrationTest.Models;
using OneOf;

namespace APIIntegrationTest.Services
{
    public interface IPokeSearchService
    {
        Task<OneOf<PokeSearchResult, PokeSearchErrorResult>> SearchPokemonByNameAsync(PokeSearchRequest pokeSearchRequest);
    }
}