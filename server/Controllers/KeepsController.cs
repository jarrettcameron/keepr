namespace keepr.Controllers;

[ApiController]
[Route("api/keeps")]
public class KeepsController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;
    private readonly KeepsService _keepsService;

    public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
    {
        _keepsService = keepsService;
        _auth0provider = auth0Provider;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return _keepsService.Create(keepData, userInfo);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}