using System.Text.Json;
using System.Threading.Tasks;
using APIIntegrationTest.Models;
using APIIntegrationTest.Output;
using APIIntegrationTest.Services;
using CommandLine;

namespace APIIntegrationTest
{
    public class PokeSearchApplication
    {
        private readonly IPokeSearchService _pokeSearchService;
        private readonly IConsoleWriter _consoleWriter;

        public PokeSearchApplication(IPokeSearchService pokeSearchService, IConsoleWriter consoleWriter)
        {
            _pokeSearchService = pokeSearchService;
            _consoleWriter = consoleWriter;
        }
        
        public async Task RunAsync(string[] args)
        {
            await Parser.Default
                .ParseArguments<PokeSearchApplicationOption>(args)
                .WithParsedAsync(async option =>
                {
                    var request = new PokeSearchRequest(option.PokemonName);
                    var result = await _pokeSearchService.SearchPokemonByNameAsync(request);
                    result.Switch(HandleResult, HandleResult);
                });
        }

        private void HandleResult<T>(T response)
        {
            var responseText = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            _consoleWriter.WriteLine(responseText);
        }
    }
}