using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlags.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureFlagController : ControllerBase
    {
        private readonly ILogger<FeatureFlagController> _logger;
        private readonly IFeatureManager _featureManager;

        public FeatureFlagController(IFeatureManager featureManager, ILogger<FeatureFlagController> logger)
        {
            _featureManager = featureManager;
            _logger = logger;
        }

        [HttpGet("FeatureGate")]
        [FeatureGate("BooleanFilter")] // use for all actions if put on controller
        public IActionResult Get()
        {
            _logger.LogInformation("You're using new feature");
            return Ok("Feature enabled");
        }

        [HttpGet("BooleanFilter")]
        public async ValueTask<IActionResult> BooleanFiler()
        {
            if (await _featureManager.IsEnabledAsync("BooleanFilter"))
            {
                _logger.LogInformation("You're using new feature");
                return Ok("Feature enabled");
            }
            else
            {
                _logger.LogInformation("Feature not enabled");
                return BadRequest("Feature not enabled");
            }
        }

        [HttpGet("PercentageFiler")]
        public async ValueTask<IActionResult> PercentageFiler()
        {
            if (await _featureManager.IsEnabledAsync("PercentageFilter"))
            {
                _logger.LogInformation("You're using new feature");
                return Ok("Feature enabled");
            }
            else
            {
                _logger.LogInformation("Feature not enabled");
                return BadRequest("Feature not enabled");
            }
        }

        [HttpGet("CustomFilter")]
        public async ValueTask<IActionResult> CustomFilter()
        {
            if (await _featureManager.IsEnabledAsync("CustomFilter"))
            {
                _logger.LogInformation("You're using new feature");
                return Ok("Feature enabled");
            }
            else
            {
                _logger.LogInformation("Feature not enabled");
                return BadRequest("Feature not enabled");
            }
        }

        [HttpGet("TimeWindowFilter")]
        public async ValueTask<IActionResult> TimeWindowFilter()
        {
            _logger.LogInformation(DateTime.UtcNow.ToString());
            if (await _featureManager.IsEnabledAsync("TimeWindowFilter"))
            {
                _logger.LogInformation($"You're using new feature {DateTime.UtcNow}");
                return Ok($"Feature enabled {DateTime.UtcNow}");
            }
            else
            {
                _logger.LogInformation($"Feature not enabled {DateTime.UtcNow}");
                return BadRequest($"Feature not enabled {DateTime.UtcNow}");
            }
        }
    }
}
