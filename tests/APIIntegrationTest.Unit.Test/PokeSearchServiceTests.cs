using System.Linq;
using System.Threading.Tasks;
using APIIntegrationTest.Api;
using APIIntegrationTest.Api.Responses;
using APIIntegrationTest.Mapping;
using APIIntegrationTest.Models;
using APIIntegrationTest.Services;
using APIIntegrationTest.Validators;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace APIIntegrationTest.Unit.Test
{
    public class PokeSearchServiceTests
    {
        private readonly PokeSearchService _sut;
        private readonly IPokeApi _pokeApi = Substitute.For<IPokeApi>();
        private readonly IValidator<PokeSearchRequest> _validator = new PokeSearchRequestValidator();
        private readonly IFixture _fixture = new Fixture();

        public PokeSearchServiceTests()
        {
            _sut = new PokeSearchService(_pokeApi, _validator);
        }

        [Fact]
        public async Task SearchPokemonByNameAsync_ShouldReturnResults_WhenPokemonNameIsValid()
        {
            //Arrange
            const string pokemonName = "ditto";
            var request = new PokeSearchRequest(pokemonName);
            var apiResponse = _fixture.Create<PokeSearchResponse>();
            _pokeApi.SearchByNameAsync(pokemonName).Returns(apiResponse);
            var expectedResult = new PokeSearchResult
            {
                HeldItems = apiResponse.HeldItems.Select(e => e.ToHeldItemResult()).ToList()
            };
            
            //Act
            var result = await _sut.SearchPokemonByNameAsync(request);

            //Assert
            result.AsT0.Should().BeEquivalentTo(expectedResult, options => options.ComparingByMembers<PokeSearchResult>().ComparingByMembers<HeldItemResult>());
        }
    }
}