using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersControllers : ControllerBase
{
    private IConfiguration _iconfig;
    public UsersControllers(IConfiguration iconfig)
    {
        _iconfig = iconfig;
    }
    // GET: api/<UsersControllers>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<UsersControllers>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return _iconfig.GetConnectionString("DefaultConnection");
    }

    // POST api/<UsersControllers>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<UsersControllers>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersControllers>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
