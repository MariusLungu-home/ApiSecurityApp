using ApiSecurity.Constants;
using ApiSecurity.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Users : ControllerBase
{
    private IConfiguration _iconfig;
    public Users(IConfiguration iconfig)
    {
        _iconfig = iconfig;
    }
    // GET: api/Users/get
    [HttpGet]
    //[Authorize(Policy = PolicyConstants.MustHaveEmployeeId)]
    //[Authorize(Policy = PolicyConstants.MustBeTheOwner)]
    [CustomAuthorization(new []{PolicyConstants.MustHaveEmployeeId, PolicyConstants.MustBeTheOwner })]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/Users/get/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return _iconfig.GetConnectionString("DefaultConnection");
    }

    // POST api/Users/post
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/Users/put/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/Users/delete/5
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
        return Ok($"The user with the id: {id} has been deleted.");
    }
}
