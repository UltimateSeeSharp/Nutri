using Newtonsoft.Json;
using Nutri.Domain.Model;

namespace Nutri.Domain.Service;

public class NrvService
{
    private List<NrvModel>? _nrvs;

    public List<NrvModel> GetNrvs()
    {
        EnsureNrvLoaded();

        return _nrvs!;
    }

    public NrvModel? GetNrv(string name)
    {
        EnsureNrvLoaded();

        return _nrvs!.FirstOrDefault(x => x.Name.ToLower() == name.ToLower())!;
    }

    private void EnsureNrvLoaded()
    {
        if (_nrvs is not null)
            return;

        _nrvs = new();

        string nrvJson = File.ReadAllText("nrv.json");
        List<NrvModel> nrvs = JsonConvert.DeserializeObject<List<NrvModel>>(nrvJson)!;

        if (nrvs is null)
            throw new AccessViolationException();

        _nrvs.AddRange(nrvs);
    }
}