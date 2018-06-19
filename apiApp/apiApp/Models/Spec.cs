using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiApp.Models
{
    public class Spec
    {
        
        public Selector Selector { get; set; }

        [JsonProperty("clusterIP")]
        public string ClusterIP { get; set; }
        public string Type { get; set; }
        public string SessionAffinity { get; set; }
        public string ExternalTrafficPolicy { get; set; }
    }
}
