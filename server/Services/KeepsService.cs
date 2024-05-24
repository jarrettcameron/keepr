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

    public List<Keep> GetAll()
    {
        return _repository.GetAll();
    }

    public Keep GetById(int keepId)
    {
        Keep keep = _repository.GetById(keepId);
        if (keep == null)
        {
            throw new Exception("NOT FOUND - Could not find object by ID.");
        }
        return keep;
    }

    public Keep Edit(int keepId, Keep keepData, Account userInfo)
    {
        Keep original = GetById(keepId);

        if (userInfo.Id != original.CreatorId)
        {
            throw new Exception("FORBIDDEN - Could not edit object that wasn't created by you.");
        }

        original.Name = keepData.Name ?? original.Name;
        original.Description = keepData.Description ?? original.Description;

        return _repository.Edit(original);
    }

    public string Destroy(int keepId, Account userInfo)
    {
        Keep keep = GetById(keepId);
        if (keep.CreatorId != userInfo.Id)
        {
            throw new Exception("FORBIDDEN - Could not delete object that wasn't created by you.");
        }

        _repository.Destroy(keepId);
        return "Deleted.";
    }
}