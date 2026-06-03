using Microsoft.AspNetCore.Mvc;
using love4animals.Models;
using love4animals.DTOs;
using love4animals.Repositories;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace love4animals.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config; // Para leer appsettings.json

    public AuthController(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Password)) return BadRequest("La contraseña es obligatoria.");
        if (_userRepository.GetByEmail(dto.Email) != null) return Conflict("Email ya registrado"); 

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12),
            Role = dto.Role.ToLower() // donadores o misioneros
        };

        _userRepository.Add(user); 
        return Created($"/api/users/{user.Id}", new { user.Id, user.Name, user.Email, user.Role });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _userRepository.GetByEmail(dto.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return Unauthorized("Credenciales incorrectas"); 
        }

        user.LastLoginAt = DateTime.UtcNow;
        _userRepository.Update(user); 

        //jwt desde appsettings
        var jwt = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Claims 
        var claims = new[] 
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        //token de 20 minuts
        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpiresMinutes"]!)),
            signingCredentials: creds
        );

       
        var refreshToken = Guid.NewGuid().ToString("N");

        return Ok(new 
        { 
            message = "Login exitoso", 
            token = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = refreshToken 
        });
    }
}