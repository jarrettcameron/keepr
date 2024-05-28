namespace keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
    }

    public VaultKeep Create(VaultKeep vaultKeepData, Account userInfo)
    {
        Vault vault = _vaultsService.GetById(vaultKeepData.VaultId, userInfo);
        if (vault.CreatorId != userInfo.Id)
        {
            throw new Exception("FORBIDDEN - Could not edit object that wasn't created by you.");
        }
        vaultKeepData.CreatorId = userInfo.Id;
        return _repository.Create(vaultKeepData);
    }

    public string Destroy(int vaultKeepId, Account userInfo)
    {
        VaultKeep vaultKeep = _repository.GetById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception("NOT FOUND - Could not find object by ID.");
        }
        Vault vault = _vaultsService.GetById(vaultKeep.VaultId, userInfo);
        if (vault.CreatorId != userInfo.Id)
        {
            throw new Exception("FORBIDDEN - Could not delete object that wasn't created by you.");
        }
        _repository.Destroy(vaultKeepId);
        return "Deleted.";
    }
}