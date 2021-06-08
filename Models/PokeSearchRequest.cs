namespace APIIntegrationTest.Models
{
    public record PokeSearchRequest
    {
        public PokeSearchRequest(string pokemonName)
        {
            PokemonName = pokemonName;
        }
        
        public string PokemonName { get; }
    }
}