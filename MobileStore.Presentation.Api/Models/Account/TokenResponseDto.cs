using Ardalis.GuardClauses;

namespace MobileStore.Presentation.Api.Models.Account
{
    public class TokenResponseDto
    {
        public string Token { get; }

        public TokenResponseDto(string token)
        {
            Token = Guard.Against.NullOrEmpty(token);
        }
    }
}
