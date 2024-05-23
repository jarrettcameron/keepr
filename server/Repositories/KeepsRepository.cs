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
}