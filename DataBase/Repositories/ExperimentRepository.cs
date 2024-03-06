using Core.Interfaces.Models;
using Core.Interfaces.Repositories;
using DataBase.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories
{
    public class ExperimentRepository : IExperimentRepository
    {
        protected readonly BackendTZContext _context;

        public ExperimentRepository(BackendTZContext context)
        {
            _context = context;
        }

        //Creating a record in DB if everything correct and deciding which value we should use
        //Optimization was carried out by putting repeated code in a separate method
        public string CreateExperiment(string deviceToken, int keyId)
        {
            var values = _context.Values.FromSqlRaw("SELECT * FROM [Value] WHERE [KeyId] = {0}", keyId).ToList();
            // This query using LINQ: var values = _context.Values.Where(x => x.KeyId == keyId).ToList();

            if (values == null)
            {
                return string.Empty;
            }

            //Probability calculation, even taking into account the fact that the total probability must be 100
            //Because if we have 3 elements which are 33.3, our totalCount would be 99.9, not 100
            double totalCount = 0;

            foreach (var value in values)
            {
                totalCount += value.Probability;
            }

            var result = string.Empty;
            var resultId = 0;

            //Calculating which value we should use
            Random random = new Random();
            double randomValue = random.NextDouble() * totalCount;

            double cumulativeProbability = 0;
            foreach (var data in values)
            {
                cumulativeProbability += data.Probability;
                if (randomValue <= cumulativeProbability)
                {
                    result = data.Name;
                    resultId = data.Id;

                    break;
                }
            }

            //Adding record to DB
            try
            {
                string insertQuery = "INSERT INTO [Experiment] ([DeviceToken], [KeyId], [ValueId]) VALUES (@DeviceToken, @KeyId, @ValueId)";
                _context.Database.ExecuteSqlRaw(insertQuery,
                    new SqlParameter("@DeviceToken", deviceToken),
                    new SqlParameter("@KeyId", keyId),
                    new SqlParameter("@ValueId", resultId)
                );

                /*In LINQ
                _context.Experiments.Add(new Experiment
                {
                    DeviceToken = deviceToken,
                    KeyId = keyId,
                    ValueId = resultId
                });

                _context.SaveChanges();
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return string.Empty;
            }

            return result;
        }

        public string GetExperimentValue(string deviceToken, string key)
        {
            var data = _context.Experiments.FromSqlRaw("SELECT * FROM [Experiment] WHERE [DeviceToken] = {0}", deviceToken).ToList();
            //In LINQ
            //var data = _context.Experiments.Where(x => x.DeviceToken == deviceToken).ToList();

            var keys = _context.Keys.FromSqlRaw("SELECT TOP 1 * FROM [Key] WHERE [Name] = {0}", key).ToList();

            if (keys == null)
            {
                return string.Empty;
            }
            //In LINQ
            //var keyId = _context.Keys.FirstOrDefault(x => x.Name == key)?.Id;

            var keyId = keys[0].Id;

            //The case when we dont have records of this device token in the DB 
            if (data.Count == 0)
            {
                return CreateExperiment(deviceToken, (int)keyId);
            }

            //The case when we have records of this device token and key in the DB
            foreach (var exp in data)
            {
                if (exp.KeyId == keyId)
                {
                    var result = _context.Values.FromSqlRaw("SELECT TOP 1 * FROM [Value] WHERE [Id] = {0}", exp.ValueId).ToList();

                    if(result == null)
                    {
                        return string.Empty;
                    }

                    return result[0].Name;

                    //Using LINQ
                    //return _context.Values.FirstOrDefault(x => x.Id == exp.ValueId)?.Name ?? string.Empty;
                }
            }

            //The case when we have records of this device token, but not with the given key in the DB
            return CreateExperiment(deviceToken, (int)keyId);
        }

        public IEnumerable<IStatistic> GetStatistic()
        {
            //Using stored procedure to get data from experiment table
            return _context.Statistics.FromSqlInterpolated($"exec [dbo].[GetStatistic]").ToList();
        }
    }
}
