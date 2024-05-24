namespace keepr.Controllers;

[ApiController]
[Route("api/vaultkeeps")]
public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsService _vaultKeepsService;

    private readonly Auth0Provider _auth0provider;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
    {
        _vaultKeepsService = vaultKeepsService;
        _auth0provider = auth0Provider;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_vaultKeepsService.Create(vaultKeepData, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultKeepId}")]
    public async Task<ActionResult<string>> Destroy(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_vaultKeepsService.Destroy(vaultKeepId, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}