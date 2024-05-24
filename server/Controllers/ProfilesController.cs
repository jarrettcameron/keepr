namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfilesController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService, Auth0Provider auth0Provider)
    {
        _profilesService = profilesService;
        _auth0provider = auth0Provider;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetById(string profileId)
    {
        try
        {
            return Ok(_profilesService.GetById(profileId));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<Profile> GetKeeps(string profileId)
    {
        try
        {
            return Ok(_keepsService.GetByCreator(profileId));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaults(string profileId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_vaultsService.GetByCreator(profileId, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}