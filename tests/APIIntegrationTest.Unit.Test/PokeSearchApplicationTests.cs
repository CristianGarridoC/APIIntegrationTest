using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using APIIntegrationTest.Api;
using APIIntegrationTest.Api.Responses;
using APIIntegrationTest.Mapping;
using APIIntegrationTest.Models;
using APIIntegrationTest.Output;
using APIIntegrationTest.Services;
using AutoFixture;
using NSubstitute;
using Xunit;

namespace APIIntegrationTest.Unit.Test
{
    public class PokeSearchApplicationTests
    {
        private readonly IPokeSearchService _pokeSearchService = Substitute.For<IPokeSearchService>();
        private readonly IConsoleWriter _consoleWriter = Substitute.For<IConsoleWriter>();
        private readonly IFixture _fixture = new Fixture();
        private readonly PokeSearchApplication _suc;

        public PokeSearchApplicationTests()
        {
            _suc = new PokeSearchApplication(_pokeSearchService, _consoleWriter);
        }

        [Fact]
        public async Task PokemonSearchApplication_ShouldShowResults_WhenPokemonNameIsValid()
        {
            //Arrenge
            const string pokemonName = "ditto";
            var args = new [] {"-p", pokemonName};
            var pokeSearchResult = _fixture.Create<PokeSearchResult>();
            var pokeSearchRequest = new PokeSearchRequest(pokemonName);
            _pokeSearchService.SearchPokemonByNameAsync(pokeSearchRequest).Returns(pokeSearchResult);
            var expectedResult = JsonSerializer.Serialize(pokeSearchResult, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            
            //Act
            await _suc.RunAsync(args);

            //Assert
            _consoleWriter.Received(1).WriteLine(Arg.Is(expectedResult));
        }
        
        /*[Fact]
        public async Task PokemonSearchApplication_ShouldShowErrors_WhenPokemonNameIsInvalid()
        {
            
        }*/
        
    }
}