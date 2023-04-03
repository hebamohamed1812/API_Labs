using lab3.Data.Models;
using lab3.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;

namespace lab3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<Student> _userManager;

    public UsersController(IConfiguration configuration,
        UserManager<Student> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<UserTokenDto>> Login(UserLoginDto credentials)
    {
        Student? student = await _userManager.FindByNameAsync(credentials.UserName);
        if (student is null)
        {
            return Ok(new { Message = "User Not Found" });
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(student, credentials.Password);
        if (!isPasswordCorrect)
        {
            return Unauthorized();
        }

        var claims = await _userManager.GetClaimsAsync(student);
        DateTime exp = DateTime.Now.AddDays(10);

        var tokenString = GenerateToken(claims, exp);
        return new UserTokenDto(tokenString);
    }

    [HttpPost]
    [Route("UserRegister")]
    public async Task<ActionResult> UserRegister(UserRegisterDto registerDto)
    {
        var student = new Student
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Degree = 150
        };

        var studentCreationResult = await _userManager.CreateAsync(student, registerDto.Password);
        if (!studentCreationResult.Succeeded)
        {
            return BadRequest(studentCreationResult.Errors);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, student.Id),
            new Claim(ClaimTypes.Role, "User")
        };

        await _userManager.AddClaimsAsync(student, claims);

        return BadRequest((new { Message = "User added successfully" }));
    }

    [HttpPost]
    [Route("AdminRegister")]
    public async Task<ActionResult> AdminRegister(AdminRegisterDto registerDto)
    {
        var student = new Student
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Degree = 250
        };

        var studentCreationResult = await _userManager.CreateAsync(student, registerDto.Password);
        if (!studentCreationResult.Succeeded)
        {
            return BadRequest(studentCreationResult.Errors);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, student.Id),
            new Claim(ClaimTypes.Role, "Admin")
        };

        await _userManager.AddClaimsAsync(student, claims);

        return BadRequest((new { Message = "Admin added successfully" }));
    }

    private string GenerateToken(IList<Claim> claimsList, DateTime exp)
    {
        var secretKeyString = _configuration.GetValue<string>("SecretKey") ?? string.Empty;
        var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString);
        var securityKey = new SymmetricSecurityKey(secretKeyInBytes);

        var signingCredentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256Signature);

        var jwt = new JwtSecurityToken(
            claims: claimsList,
            expires: exp,
            signingCredentials: signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(jwt);

        return tokenString;
    }
}
