namespace keepr.Controllers;

[ApiController]
[Route("api/vaults")]
public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth0provider;

    public VaultsController(VaultsService vaultsService, KeepsService keepsService, Auth0Provider auth0Provider)
    {
        _vaultsService = vaultsService;
        _auth0provider = auth0Provider;
        _keepsService = keepsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_vaultsService.Create(vaultData, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_vaultsService.GetById(vaultId, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> Edit(int vaultId, [FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_vaultsService.Edit(vaultId, vaultData, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<string>> Destroy(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_vaultsService.Destroy(vaultId, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeepView>>> GetKeepsByVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_keepsService.GetKeepsByVault(vaultId, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}