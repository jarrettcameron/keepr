namespace keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repository;

    public KeepsService(KeepsRepository repository)
    {
        _repository = repository;
    }

    public Keep Create(Keep keepData, Account userInfo)
    {
        keepData.CreatorId = userInfo.Id;
        return _repository.Create(keepData);
    }
}