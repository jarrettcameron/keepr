namespace keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Vault Create(Vault vaultData)
    {
        string sql = @"INSERT INTO vault(name, description, isPrivate, img, creatorId)
        VALUES (@Name, @Description, @IsPrivate, @Img, @CreatorId);
        
        SELECT * FROM vault JOIN keeprAccounts ON keeprAccounts.id = vault.creatorId WHERE vault.id = LAST_INSERT_ID();";
        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    public Vault GetById(int vaultId)
    {
        string sql = @"
        SELECT * FROM vault JOIN keeprAccounts ON keeprAccounts.id = vault.creatorId WHERE vault.id = @vaultId;";
        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }

    public Vault Edit(Vault vaultData)
    {
        string sql = @"UPDATE vault SET name = @Name, isPrivate = @IsPrivate WHERE vault.id = @Id; SELECT * FROM vault JOIN keeprAccounts ON keeprAccounts.id = vault.creatorId WHERE vault.id = @Id;";
        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    public void Destroy(int vaultId)
    {
        string sql = "DELETE FROM vault WHERE id = @vaultId;";
        _db.Execute(sql, new { vaultId });
    }

    public List<Vault> GetByCreator(string creatorId)
    {
        string sql = @"
        SELECT * FROM vault JOIN keeprAccounts ON keeprAccounts.id = vault.creatorId WHERE vault.creatorId = @CreatorId;";
        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { creatorId }).ToList();
        return vaults;
    }
}