using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace apiApp.Controllers
{
    [Route("api/[controller]")]
    public class ClusterController : Controller
    {
        private IClusterService _clusterService { get; set; }

        public ClusterController(IClusterService clusterService)
        {
            _clusterService = clusterService;

        }

        // GET api/values
        [HttpGet("list")]
        public string List()
        {

            return _clusterService.GetServerList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
