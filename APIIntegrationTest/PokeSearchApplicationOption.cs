using CommandLine;

namespace APIIntegrationTest
{
    public class PokeSearchApplicationOption
    {
        [Option('p', "pokemonName", Required = true, HelpText = "Ingresa el nombre del pokemón a buscar")]
        public string PokemonName { get; init; }
    }
}