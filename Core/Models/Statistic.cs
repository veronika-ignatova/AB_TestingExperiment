using Core.Interfaces.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Models
{
    [Keyless]
    public class Statistic : IStatistic
    {
        public string ValueName { get; set; }
        public double Probability { get; set; }
        public int ValueCount { get; set; }
        public string KeyName { get; set; }
        public int KeyId { get; set; }
    }
}
