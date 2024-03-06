using Core.Interfaces.Models;
using System;

namespace Core.Interfaces.Services
{
    public interface IExperimentService
    {
        string GetExperimentValue(string deviceToken, string key);
        IEnumerable<IStatistic> GetStatistic();
    }
}
