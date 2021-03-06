using Application.Common.Models;
using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<string> GenerateJwtToken(string userId);

        Task<(Result Result, Tokens Tokens)> AuthenticateUser(string email, string password);

        Task<RefreshToken> GenerateRefreshToken(string userId, string jwtId);

        Task<Result> VerifyTokens(string JwtToken, string refreshToken);

        Task<Tokens> GenerateTokens(string userId);

    }
}
