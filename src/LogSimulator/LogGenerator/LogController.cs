using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace LogSimulator.LogGenerator
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogger<LogController> _logger;

        private class SampleObjectOutput
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost("good")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesErrorResponseType(typeof(Exception))]

        public async Task<IActionResult> GenerateRandomizeGoodLogEntries()
        {
            _logger.LogTrace("Generating log entries");
            
            _logger.LogDebug("Sample basic text entry with basic value, a Guid:{SomeValue} and a text value {TextValue}",
                Guid.NewGuid(), "Hello, I'm a log entry");
            _logger.LogDebug("Sample entry with object type, output is destructured: {@ObjectResult}",
                new SampleObjectOutput
                {
                    Key = "SampleKey", Value = "SampleValue"
                });
            
            _logger.LogTrace("Finish generating log entries");
            return await Task.FromResult(Accepted());
        }

        [HttpPost("bad")]
        [ProducesErrorResponseType(typeof(LogException))]

        public IActionResult GenerateExceptionLogEntries()
        {
            _logger.LogTrace("Generating exception type log entries");

            try
            {
                // This should generate math library exception
                var value = 123;
                // ReSharper disable once IntDivisionByZero
                return Ok(value / 0);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "This operation is deliberately generate an error");
                throw;
            }
            finally
            {
                _logger.LogTrace("Finish generating exception type log entries");
            }
        }
    }
}