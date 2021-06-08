using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIIntegrationTest.Api;
using APIIntegrationTest.Mapping;
using APIIntegrationTest.Models;
using FluentValidation;
using OneOf;
using Refit;

namespace APIIntegrationTest.Services
{
    public class PokeSearchService : IPokeSearchService
    {
        private readonly IPokeApi _pokeApi;
        private readonly IValidator<PokeSearchRequest> _validator;

        public PokeSearchService(IPokeApi pokeApi, IValidator<PokeSearchRequest> validator)
        {
            _pokeApi = pokeApi;
            _validator = validator;
        }
        
        public async Task<OneOf<PokeSearchResult, PokeSearchErrorResult>> SearchPokemonByNameAsync(PokeSearchRequest pokeSearchRequest)
        {
            var validationResult = await _validator.ValidateAsync(pokeSearchRequest);
            if (!validationResult.IsValid)
            {
                return new PokeSearchErrorResult(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            try
            {
                var response = await _pokeApi.SearchByNameAsync(pokeSearchRequest.PokemonName);
                return new PokeSearchResult()
                {
                    HeldItems = response.HeldItems.Select(e => e.ToHeldItemResult()).ToList()
                };
            }
            catch (ApiException)
            {
                return new PokeSearchErrorResult(new List<string>() { $"No se encontro el pokemón {pokeSearchRequest.PokemonName}" });
            }
        }
    }
}