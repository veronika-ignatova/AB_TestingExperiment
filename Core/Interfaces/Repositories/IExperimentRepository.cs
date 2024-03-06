using Core.Interfaces.Models;

namespace Core.Interfaces.Repositories
{
    public interface IExperimentRepository
    {
        string GetExperimentValue(string deviceToken, string key);
        IEnumerable<IStatistic> GetStatistic();
    }
}
