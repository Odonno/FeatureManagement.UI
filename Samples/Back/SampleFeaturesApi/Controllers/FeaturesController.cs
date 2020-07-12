using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SampleFeaturesApi.FeatureManagement;
using SampleFeaturesApi.FeatureManagement.Data;

namespace SampleFeaturesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeaturesService _featuresService;

        public FeaturesController(IFeaturesService featuresService)
        {
            _featuresService = featuresService;
        }

        [HttpGet]
        public IEnumerable<Feature> Get()
        {
            return _featuresService.GetAll();
        }
    }
}
