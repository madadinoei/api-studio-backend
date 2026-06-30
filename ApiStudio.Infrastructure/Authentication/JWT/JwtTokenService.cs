using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiStudio.Application.Authentication.Dtos;
using ApiStudio.Application.Authentication.Interfaces;
using ApiStudio.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ApiStudio.Infrastructure.Authentication.JWT;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _options;

    public JwtTokenService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> options)
    {
        _userManager = userManager;
        _options = options.Value;
    }

    public async Task<LoginResponse> GenerateTokenAsync(
        Guid identityUserId,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(identityUserId.ToString());

        if (user is null)
            throw new UnauthorizedAccessException();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(
            roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SecretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new LoginResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expires
        };
    }
}