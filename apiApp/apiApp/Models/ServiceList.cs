using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiApp.Models
{
    public class ServiceList
    {
        public string Kind { get; set; }
        public string ApiVersion { get; set; }
        public ServiceMetadata Metadata { get; set; }

        public List<ServiceObject> Items { get; set; }
    }
}
