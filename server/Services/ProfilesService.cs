namespace keepr.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _repository;

    public ProfilesService(ProfilesRepository repository)
    {
        _repository = repository;
    }

    public Profile GetById(string profileId)
    {
        Profile profile = _repository.GetById(profileId);

        if (profile == null)
        {
            throw new Exception("NOT FOUND - Could not find object by ID.");
        }

        return profile;
    }
}