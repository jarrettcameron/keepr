namespace keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Keep Create(Keep keepData)
    {
        string sql = @"
        INSERT INTO keep(name, description, img, creatorId) VALUES (@Name, @Description, @Img, @CreatorId);
        SELECT keep.*, COUNT(vaultkeep.id) AS kept, keeprAccounts.* FROM keep
        LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id
        JOIN keeprAccounts ON keeprAccounts.id = @CreatorId
        WHERE keep.id = LAST_INSERT_ID()
        GROUP BY (keep.id);";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, keepData).FirstOrDefault();
        return keep;
    }

    public List<Keep> GetAll()
    {
        string sql = @"SELECT keep.*,COUNT(vaultkeep.id) AS kept,keeprAccounts.* FROM keep LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id JOIN keeprAccounts ON keeprAccounts.id = keep.creatorId GROUP BY (keep.id) ORDER BY keep.createdAt DESC;";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }).ToList();
        return keeps;
    }

    public void IncrementViews(int keepId)
    {
        string sql = @"UPDATE keep SET views = views + 1 WHERE id = @keepId;";
        _db.Execute(sql, new { keepId });
    }

    public Keep GetById(int keepId)
    {
        string sql = @"SELECT keep.*,COUNT(vaultkeep.id) AS kept,keeprAccounts.* FROM keep LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id JOIN keeprAccounts ON keeprAccounts.id = keep.creatorId WHERE keep.id = @keepId GROUP BY (keep.id);";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    public Keep Edit(Keep keepData)
    {
        string sql = @"UPDATE keep SET
            name = @Name,
            description = @Description
            WHERE id = @Id;

            SELECT keep.*,COUNT(vaultkeep.id) AS kept,keeprAccounts.* FROM keep LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id JOIN keeprAccounts ON keeprAccounts.id = keep.creatorId WHERE keep.id = @Id GROUP BY (keep.id);
        ";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, keepData).FirstOrDefault();
        return keep;
    }

    public void Destroy(int keepId)
    {
        string sql = "DELETE FROM keep WHERE id = @keepId;";
        _db.Execute(sql, new { keepId });
    }

    public List<VaultKeepView> GetKeepsByVault(int vaultId)
    {
        string sql = @"SELECT keep.*, COUNT(vaultkeep.id) as kept, vaultkeep.id, keeprAccounts.* FROM keep
        JOIN vaultkeep ON vaultkeep.keepId = keep.id
        JOIN keeprAccounts ON keeprAccounts.id = keep.creatorId
        WHERE vaultkeep.vaultId = @vaultId
        GROUP BY (vaultkeep.id);";
        List<VaultKeepView> keeps = _db.Query<VaultKeepView, VaultKeep, Profile, VaultKeepView>(sql, (vkv, vk, p) =>
        {
            vkv.Creator = p;
            vkv.VaultKeepId = vk.Id;
            return vkv;
        }, new { vaultId }).ToList();
        return keeps;
    }

    public List<Keep> GetByCreator(string creatorId)
    {
        string sql = @"SELECT keep.*,COUNT(vaultkeep.id) AS kept,keeprAccounts.* FROM keep LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id JOIN keeprAccounts ON keeprAccounts.id = keep.creatorId WHERE keep.creatorId = @CreatorId GROUP BY (keep.id);";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { creatorId }).ToList();
        return keeps;
    }
}