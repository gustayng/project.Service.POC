using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cloudstarter.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        // Comment 1
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HomeController> _logger;
        ServiceBus _serviceBus;

        public HomeController(ILogger<HomeController> logger, ServiceBus serviceBus)
        {
            _logger = logger;
            _serviceBus = serviceBus;
        }

        [Route("/Get")]
        [HttpGet]
        public void Get()
        {
            
        }

    }
}
