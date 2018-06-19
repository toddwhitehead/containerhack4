using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiApp.Models
{
    public class Metadata
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string selfLink { get; set; }
        public string Uid { get; set; }
        public string ResourceVersion { get; set; }
        public string CreationTimestamp { get; set; }
    }
}
