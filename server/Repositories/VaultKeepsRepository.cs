namespace keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public VaultKeep Create(VaultKeep vaultKeepData)
    {
        string sql = @"INSERT INTO vaultkeep(creatorId, keepId, vaultId)
        VALUES (@CreatorId, @KeepId, @VaultId);
        
        SELECT * FROM vaultkeep WHERE id = LAST_INSERT_ID();";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    public void Destroy(int vaultKeepId)
    {
        string sql = "DELETE FROM vaultkeep WHERE vaultkeep.id = @vaultKeepId;";
        _db.Execute(sql, new { vaultKeepId });
    }

    public VaultKeep GetById(int vaultKeepId)
    {
        string sql = "SELECT * FROM vaultkeep WHERE id = @vaultKeepId;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }
}