using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QiTask.Data.Data;
using QiTask.Dtos;
using QiTask.Models;

namespace QiTask.Controllers;

[ApiController]
[Route("[controller]")]
public class Auth : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IConfiguration _config;

    public Auth(IMapper mapper, DataContext context, IConfiguration config)
    {
        _mapper = mapper;
        _context = context;
        _config = config;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        // scope naming conventions should use camelCase
        // userName

        // Use ActionFilter aka Middleware to catch exceptions globally
        var user = await _context.Users.Where(x => x.Username == loginDto.Username).FirstOrDefaultAsync();

        if (user is null)
        {
            return Unauthorized();
        }

        //the above line should be like this
        // var User = await _context.Users.FirstOrDefaultAsync(x => x.Username == loginDto.Username);
        //because the above line could crash your api if the where returns a null value


        var isVerified = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);


        if (!isVerified)
        {
            return Unauthorized();
        }

        return Ok(new UserResponseDto
        {
            token = this.GenerateJwtToken(user),
            user = user
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        registerDto.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        var user = _mapper.Map<RegisterDto, User>(registerDto);

        await _context.Users.AddAsync(user);

        var isSaved = await _context.SaveChangesAsync() > 0;

        if (!isSaved)
        {
            return UnprocessableEntity();
        }

        return Ok(User);
    }


    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}