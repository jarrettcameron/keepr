namespace keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repository;

    public VaultsService(VaultsRepository repository)
    {
        _repository = repository;
    }

    public Vault Create(Vault vaultData, Account userInfo)
    {
        vaultData.CreatorId = userInfo.Id;
        return _repository.Create(vaultData);
    }

    public Vault GetById(int vaultId, Account userInfo)
    {
        Vault vault = _repository.GetById(vaultId);
        if (vault == null)
        {
            throw new Exception("NOT FOUND - Could not find object by ID.");
        }

        if (vault.IsPrivate.GetValueOrDefault())
        {
            if (userInfo?.Id == vault.CreatorId)
            {
                return vault;
            }
            throw new Exception("FORBIDDEN - You cannot access this object.");
        }
        else
        {
            return vault;
        }
    }

    public Vault Edit(int vaultId, Vault vaultData, Account userInfo)
    {
        Vault original = GetById(vaultId, userInfo);

        if (original.CreatorId != userInfo.Id)
        {
            throw new Exception("FORBIDDEN - Could not edit object that wasn't created by you.");
        }

        original.Name = vaultData.Name ?? original.Name;
        original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;

        return _repository.Edit(original);
    }

    public string Destroy(int vaultId, Account userInfo)
    {
        Vault vault = GetById(vaultId, userInfo);
        if (vault.CreatorId != userInfo.Id)
        {
            throw new Exception("FORBIDDEN - Could not delete object that wasn't created by you.");
        }
        _repository.Destroy(vaultId);
        return "Deleted.";
    }

    public List<Vault> GetByCreator(string creatorId, Account userInfo)
    {
        List<Vault> vaults = _repository.GetByCreator(creatorId);
        if (userInfo?.Id != creatorId)
        {
            vaults = vaults.FindAll((vault) => !vault.IsPrivate.GetValueOrDefault());
        }
        return vaults;
    }
}