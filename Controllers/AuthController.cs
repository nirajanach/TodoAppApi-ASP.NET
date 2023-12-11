using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApps.Models;
using TodoApps.ViewModels;

namespace TodoApps.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : BaseAuthController
{
    private TodoDbContext _context;
    private IConfiguration _config;
    public AuthController(TodoDbContext context, IConfiguration config)
    {
       _context = context;
        _config = config;

    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = new User
        {
            Name = model.Name,
            UserName = model.UserName,
            Password = model.Password
        };
        _context.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user.Id + "Registered Successflully");

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.UserName == model.UserName && user.Password == model.Password);
        if ( user == null)
        {
            return Unauthorized("Invalid login credentials");
        }
        var token = GenerateToken(user);

        return Ok(token);

    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
