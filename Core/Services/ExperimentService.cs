using Core.Interfaces.Models;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class ExperimentService : IExperimentService
    {
        private readonly IExperimentRepository _experimentRepository;

        public ExperimentService(IExperimentRepository experimentRepository)
        {
            _experimentRepository = experimentRepository;
        }

        public string GetExperimentValue(string deviceToken, string key)
        {
            return _experimentRepository.GetExperimentValue(deviceToken, key);
        }

        public IEnumerable<IStatistic> GetStatistic()
        {
            return _experimentRepository.GetStatistic();
        }

    }
}
