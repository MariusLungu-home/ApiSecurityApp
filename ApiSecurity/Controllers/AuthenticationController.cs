using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public record AutheticationData(string? UserName, string? Password);
    public record UserData(int UserId, string UserName, string Title, string EmployeeId);
    
    private IConfiguration _iconfig;

    public AuthenticationController(IConfiguration iconfig)
    {
        _iconfig = iconfig;
    }

    // api/authentication/token
    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody] AutheticationData data) 
    {
        var user = ValidateCredentials(data);
        if (user == null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);
        return Ok(token);
    }

    private string GenerateToken(UserData user) 
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_iconfig.GetValue<string>("Authentication:SecretKey")));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
        claims.Add(new("title", user.Title));
        claims.Add(new("employeeId", user.EmployeeId));

        var token = new JwtSecurityToken(
            _iconfig.GetValue<string>("Authentication:Issuer"),
            _iconfig.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,                       // when this token is valid
            DateTime.UtcNow.AddMinutes(1),         // when this token expires
            signinCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserData? ValidateCredentials(AutheticationData data)
    {
        // THIS IS NOT PRODUCTION CODE - THIS IS ONLY FOR DEMONSTRATION PURPOSES - DO NO USE THIS IN REAL LIFE
        
        if (CompareValues(data.UserName, "tcorey") && CompareValues(data.Password, "Test123"))
        { 
            return new UserData(1, data.UserName!, "Business Owner", "E001");
        }

        if (CompareValues(data.UserName, "sstorm") && CompareValues(data.Password, "Test123"))
        {
            return new UserData(2, data.UserName!, "Head of Security", EmployeeId: null!);
        }

        if (CompareValues(data.UserName, "mlungu") && CompareValues(data.Password, "Test123"))
        {
            return new UserData(3, data.UserName!, "Head of Development", "E002");
        }

        return null;
    }

    private bool CompareValues(string? actual, string expected)
    {
        if (actual is not null)
        {
            return actual.Equals(expected);
        }

        return false;
    }
}
