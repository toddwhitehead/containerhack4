using Newtonsoft.Json;
using System.Collections.Generic;

namespace apiApp.Models
{
    public class LoadBalancer
    {
        [JsonProperty("ingress")]
        public List<Ingress> Ingress { get; set; }
    }
}