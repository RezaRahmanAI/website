using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using TechNova.API.Models.Requests;
using TechNova.API.Models.Responses;
using TechNova.Core.Entities;
using TechNova.Core.Interfaces;
using TechNova.Infrastructure.Services;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenService _tokenService;

    public AuthController(IUnitOfWork unitOfWork, JwtTokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var existingUser = await _unitOfWork.Users.SearchAsync(user => user.NormalizedEmail == normalizedEmail, cancellationToken);
        if (existingUser.Any())
        {
            return Conflict("An account with this email already exists.");
        }

        var salt = RandomNumberGenerator.GetBytes(32);
        var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: request.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            numBytesRequested: 64));

        var user = new ApplicationUser
        {
            Email = request.Email,
            NormalizedEmail = normalizedEmail,
            PasswordHash = $"{Convert.ToBase64String(salt)}.{passwordHash}",
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = request.Role,
            CreatedBy = "system"
        };

        await _unitOfWork.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _tokenService.GenerateToken(user);

        var response = new AuthResponse
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(2),
            UserId = user.Id,
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            Role = user.Role.ToString()
        };

        return CreatedAtAction(nameof(RegisterAsync), response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = (await _unitOfWork.Users.SearchAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken)).FirstOrDefault();
        if (user is null)
        {
            return Unauthorized("Invalid credentials");
        }

        if (!VerifyPassword(user.PasswordHash, request.Password))
        {
            return Unauthorized("Invalid credentials");
        }

        var token = _tokenService.GenerateToken(user);

        var response = new AuthResponse
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(2),
            UserId = user.Id,
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            Role = user.Role.ToString()
        };

        return Ok(response);
    }

    private static bool VerifyPassword(string storedHash, string password)
    {
        var parts = storedHash.Split('.');
        if (parts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var hash = parts[1];
        var computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 100000, 64));
        return hash == computedHash;
    }
}
