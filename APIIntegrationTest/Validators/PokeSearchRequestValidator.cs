using APIIntegrationTest.Models;
using FluentValidation;

namespace APIIntegrationTest.Validators
{
    public class PokeSearchRequestValidator : AbstractValidator<PokeSearchRequest>
    {
        public PokeSearchRequestValidator()
        {
            RuleFor(request => request.PokemonName)
                .Matches("^([A-Za-z])*$")
                .MaximumLength(10);
        }
    }
}