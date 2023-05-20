using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public record AutheticationData(string? UserName, string? Password);
    public record UserData(int UserId, string UserName);

    // api/authentication/token
    [HttpPost("token")]
    public ActionResult<string> Authenticate([FromBody] AutheticationData data) 
    {
        var user = ValidateCredentials(data);
        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(user);
    }

    private string GenerateToken(UserData userData) 
    {
    
    }

    private UserData? ValidateCredentials(AutheticationData data)
    {
        // THIS IS NOT PRODUCTION CODE - THIS IS ONLY FOR DEMONSTRATION PURPOSES - DO NO USE THIS IN REAL LIFE
        
        if (CompareValues(data.UserName, "tcorey") &&
            CompareValues(data.Password, "Test123"))
        { 
            return new UserData(1, data.UserName!);
        }

        if (CompareValues(data.UserName, "sstorm") &&
            CompareValues(data.Password, "Test123"))
        {
            return new UserData(2, data.UserName!);
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
