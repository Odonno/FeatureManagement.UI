using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SampleFeaturesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Feature> Get()
        {
            return new List<Feature>
            {
                new Feature
                {
                    Name = "Beta",
                    Enabled = true
                }
            };
        }
    }
}
