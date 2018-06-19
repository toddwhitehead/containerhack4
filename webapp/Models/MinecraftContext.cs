using System;
using System.Collections.Generic;

namespace webapp.Models
{
    public static class MinecraftContext
    {
        public static List<MinecraftServer> GetMinecraftServerList()
        {
            return new List<MinecraftServer>() 
            {
                new MinecraftServer()
                {
                    Name = "tenant1",
                    Endpoints = new List<MinecraftServerEndpoint>() {
                        new MinecraftServerEndpoint() {
                            Name = "minecraft",
                            Address = "123.1.1.1:2557"
                        },
                        new MinecraftServerEndpoint() {
                            Name = "rcon",
                            Address = "123.1.1.1:2558"
                        }
                    }
                },
                new MinecraftServer()
                {
                    Name = "tenant2",
                    Endpoints = new List<MinecraftServerEndpoint>() {
                        new MinecraftServerEndpoint() {
                            Name = "minecraft",
                            Address = "123.1.1.2:2557"
                        },
                        new MinecraftServerEndpoint() {
                            Name = "rcon",
                            Address = "123.1.1.2:2558"
                        }
                    }
                }
            };
        }
    }
}