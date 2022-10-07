using Newtonsoft.Json;
using Nutri.Domain.Model;

namespace Nutri.Domain.Service;

public class NrvService
{
    private List<NrvModel>? _nrvs;

    public List<NrvModel> GetNrvs()
    {
        if (_nrvs is null)
        {
            _nrvs = new();

            string nrvJson = File.ReadAllText("nrv.json");

            var nrvs = JsonConvert.DeserializeObject<List<NrvModel>>(nrvJson);

            if (nrvs is not null)
                _nrvs.AddRange(nrvs);
        }

        return _nrvs;
    }
}