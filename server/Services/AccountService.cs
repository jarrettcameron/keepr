namespace keepr.Services;

public class AccountService
{
    private readonly AccountsRepository _repo;

    public AccountService(AccountsRepository repo)
    {
        _repo = repo;
    }

    internal Account GetProfileByEmail(string email)
    {
        return _repo.GetByEmail(email);
    }

    internal Account GetProfileById(string id)
    {
        Account user = _repo.GetById(id);
        if (user == null)
        {
            throw new Exception("NOT FOUND - Could not find object by ID.");
        }
        return user;
    }

    internal Account GetOrCreateProfile(Account userInfo)
    {
        Account profile = _repo.GetById(userInfo.Id);
        if (profile == null)
        {
            return _repo.Create(userInfo);
        }
        return profile;
    }

    internal Account Edit(Account editData, Account userInfo)
    {
        Account original = GetProfileById(userInfo.Id);
        original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
        original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
        original.CoverImg = editData.CoverImg?.Length > 0 ? editData.CoverImg : original.CoverImg;
        original.Email = editData.Email?.Length > 0 ? editData.Email : original.Email;
        return _repo.Edit(original);
    }
}
