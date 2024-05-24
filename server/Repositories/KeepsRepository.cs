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
        string sql = @"SELECT keep.*,COUNT(vaultkeep.id) AS kept,keeprAccounts.* FROM keep LEFT JOIN vaultkeep ON vaultkeep.keepId = keep.id JOIN keeprAccounts ON keeprAccounts.id = keep.creatorId GROUP BY (keep.id);";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }).ToList();
        return keeps;
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
}