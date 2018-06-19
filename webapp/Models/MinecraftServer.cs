using System;
using System.Collections.Generic;

namespace webapp.Models
{
    public class MinecraftServer
    {
        public string Name;
        public List<MinecraftServerEndpoint> Endpoints;
    }

    public class MinecraftServerEndpoint
    {
        public string Name;
        public string Address;
    }
}