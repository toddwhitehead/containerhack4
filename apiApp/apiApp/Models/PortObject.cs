using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiApp.Models
{
    public class PortObject
    {
        public string Name { get; set; }
        public string Protocol { get; set; }
        public int Port { get; set; }
        public int TargetPort { get; set; }
        public int NodePort { get; set; }

    }
}
