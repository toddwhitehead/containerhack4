using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using k8s;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinecraftServerController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            var client = new Kubernetes(config);
            return new string[] { "value1", "value2" };
        }
    }
}