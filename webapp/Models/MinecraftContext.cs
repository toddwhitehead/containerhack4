using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace webapp.Models
{
    public static class MinecraftContext
    {
        private static HttpClient client = new HttpClient();

        public async static Task<bool> CreateMinecraftServer()
        {
            try{
                string url = "https://hectagonminecraftfunctions.azurewebsites.net/api/CreateMinecraftServer";
                var response = await client.PostAsync(url, null);
                if(response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            } catch{
                return false;
            }
        }

        public async static Task<List<MinecraftServer>> GetMinecraftServerListAsync()
        {
            var jsonserverlist = await GetMinecraftServerListFromAPIAsync();
            return JsonConvert.DeserializeObject<List<MinecraftServer>>(jsonserverlist);
        }

        private async static Task<string> GetMinecraftServerListFromAPIAsync()
        {
            try
            {
                string url = "https://hectagonminecraftfunctions.azurewebsites.net/api/GetMinecraftServerList";
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            } catch (Exception e)
            {
                return null;
            }
        }

        public static List<MinecraftServer> GetHardCodedMinecraftServerList()
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