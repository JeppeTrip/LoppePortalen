using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

//Based on https://github.com/jasontaylordev/CleanArchitecture/blob/main/src/Infrastructure/Identity/IdentityService.cs
namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IApplicationDbContext _context;
        private readonly TokenValidationParameters _validationParameters;

        private readonly JwtConfig _jwtConfig;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IApplicationDbContext context,
            TokenValidationParameters validationParameters)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _jwtConfig = optionsMonitor.CurrentValue;
            _context = context;
            _validationParameters = validationParameters;
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<string> GenerateJwtToken(string userId)
        {
            var user = await GetUser(userId);
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email), //unique id - emails required unique in this instance.
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // used by the refresh token
                }),
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame), //read timespan from jwtConfig
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature //TODO: Review algorithm
                )
            };
            //Gen security token
            var token = jwtHandler.CreateToken(tokenDescriptor);
            //Convert security token into a usable string
            var jwtToken = jwtHandler.WriteToken(token);

            return jwtToken;
        }

        private async Task<SecurityToken> GenerateSecurityToken(string userId)
        {
            var user = await GetUser(userId);
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email), //unique id - emails required unique in this instance.
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // used by the refresh token
                }),
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame), //read timespan from jwtConfig
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature //TODO: Review algorithm
                )
            };
            //Gen security token
            var token = jwtHandler.CreateToken(tokenDescriptor);
            //Convert security token into a usable string

            return token;
        }

        public async Task<ApplicationUser> GetUser(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user;
        }

        public async Task<(Result Result, Tokens Tokens)> AuthenticateUser(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return (Result.Failure(new List<string>() { "Invalid email or password." }), null);
                } else
                {
                    var isValidPass = await _userManager.CheckPasswordAsync(user, password);
                    if (isValidPass) {
                        var token = await GenerateSecurityToken(user.Id);
                        var refreshToken = await GenerateRefreshToken(user.Id, token.Id);
                        return (Result.Success(), new Tokens { JwtToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = refreshToken});
                    } else
                    {
                        return (Result.Failure(new List<string>() { "Invalid email or password." }), null);
                    }
                }
            }
            catch (Exception e)
            {
                return (Result.Failure(new List<string>() { "Invalid email or password." }), null);
            }
        }

        public async Task<RefreshToken> GenerateRefreshToken(string userId, string jwtId)
        {
            var refreshToken = new RefreshToken
            {
                Token = $"{RandomStringGenerator(25)}_{Guid.NewGuid()}",
                ApplicationUser = await _userManager.FindByIdAsync(userId),
                UserId = userId,
                IsRewoked = false,
                IsUsed = false,
                JwtId = jwtId,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };
            
            return refreshToken;
        }

        private string RandomStringGenerator(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<Result> VerifyTokens(string JwtToken, string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(JwtToken, _validationParameters, out var validatedToken);

                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if(!result)
                    {
                        return Result.Failure(new List<string>() { "Invalid jwt token." });
                    }

                    //Check if jwt token has expired.
                    var utcExpiryDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                    DateTime expDate = UnixTimeToDateTime(utcExpiryDate);
                    if(expDate > DateTime.UtcNow)
                    {
                        return Result.Failure(new List<string> { "Jwt token has not expired." });
                    }

                    //Check validity of refresh token.
                    var refreshTokenExists = _context.RefreshTokens.FirstOrDefault(x => x.Token.ToLower() == refreshToken.ToLower());
                    if(refreshTokenExists == null)
                    {
                        return Result.Failure(new List<string>() { "Invalid refresh token." });
                    }

                    //Check expiry date of refresh token.
                    if(refreshTokenExists.ExpiryDate < DateTime.UtcNow)
                    {
                        return Result.Failure(new List<string>() { "Refresh token has expired, login again." });
                    }

                    //check if refresh token has been used.
                    if(refreshTokenExists.IsUsed)
                    {
                        return Result.Failure(new List<string>() { "Refresh token cannot be reused." });
                    }

                    //check if refresh token has been revoked.
                    if (refreshTokenExists.IsRewoked)
                    {
                        return Result.Failure(new List<string>() { "Refresh token has been revoked." });
                    }

                    //Check Jwt Token id is the one that matches the refresh token.
                    var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                    if(refreshTokenExists.JwtId != jti)
                    {
                        return Result.Failure(new List<string>() { "Jwt token and refresh token doesn't match." });
                    }

                    return Result.Success();
                }
                return Result.Failure(new List<string>() { "Not a JWT token." });
            }
            catch(Exception ex)
            {
                //TODO: Add better error handling.
                return Result.Failure(new List<string>() { "Unexpected error." });
            }
        }

        private DateTime UnixTimeToDateTime(long unixDate)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixDate);

            return dateTime;
        }

        public async Task<Tokens> GenerateTokens(string userId)
        {
            var jwt = await GenerateSecurityToken(userId);
            var refresh = await GenerateRefreshToken(userId, jwt.Id);

            return new Tokens() { JwtToken = new JwtSecurityTokenHandler().WriteToken(jwt), RefreshToken = refresh };
        }
    }
}
