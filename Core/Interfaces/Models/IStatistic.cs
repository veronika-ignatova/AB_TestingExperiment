using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Models
{
    public interface IStatistic
    {
        public string ValueName { get; set; }
        public double Probability { get; set; }
        public int ValueCount { get; set; }
        public string KeyName { get; set; }
        public int KeyId { get; set; }
    }
}
