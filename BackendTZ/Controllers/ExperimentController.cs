using BackendTZ.Models;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly IExperimentService _experimentService;

        public ExperimentController(IExperimentService experimentService)
        {
            _experimentService = experimentService;
        }

        [HttpGet]
        public IActionResult GetStatistic()
        {
            //Fill our table with data
            string deviceToken = "randomstring";

            for (int i = 0; i < 1000; i++)
            {
                _experimentService.GetExperimentValue(deviceToken + i, "button-color");
            }

            for (int i = 0; i < 1000; i++)
            {
                _experimentService.GetExperimentValue(deviceToken + i, "price");
            }

            var result = _experimentService.GetStatistic();

            // Dictionary to store the sum of ValueCount for each KeyId
            var numberOfParticipants = new Dictionary<int, int>();

            foreach (var item in result)
            {
                if (numberOfParticipants.ContainsKey(item.KeyId))
                {
                    numberOfParticipants[item.KeyId] += item.ValueCount;
                }
                else
                {
                    numberOfParticipants[item.KeyId] = item.ValueCount;
                }
            }

            return Ok(new {result, numberOfParticipants });
        }

        //Optimization was carried out by combining two methods into one universal.
        //Not just using button-collor or price in HttpGet, but using string key
        [HttpGet("{key}")]
        public IActionResult GetButtonColor(string key,[FromQuery(Name = "device-token")] string deviceToken)
        {
            //Checking whether we get device token
            if (string.IsNullOrWhiteSpace(deviceToken))
            {
                return BadRequest();
            }

            string value = _experimentService.GetExperimentValue(deviceToken, key);

            //Checking whether we have value
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest();
            }

            //If we have a value, we create a ExperimentResult
            var result = new ExperimentResult
            {
                Key = key,
                Value = value
            };

            return Ok(result);
        }

    }
}
