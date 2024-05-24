namespace keepr.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    public Profile GetById(string profileId)
    {
        string sql = "SELECT * FROM keeprAccounts WHERE id = @profileId;";
        return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
    }
}