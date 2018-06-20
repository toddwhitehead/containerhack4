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

        public async static Task<MinecraftEvent> GetMinecraftServerStatisticsAsync(MinecraftServer server)
        {
            var endpoint = server.Endpoints.Find(x => x.Name.Contains("rcon"));
            return await GetMinecraftServerStatisticsAsync(endpoint.Address);
        }

        public async static Task<MinecraftEvent> GetMinecraftServerStatisticsAsync(string address)
        {
            var ipaddress = address.Substring(0, address.IndexOf(":"));
            var url = $"https://mcapi.us/server/status?ip={ipaddress}";
            var response = await client.GetAsync(url);
            var responseFromServer = await response.Content.ReadAsStringAsync();
            var minecraftEvent = JsonConvert.DeserializeObject<MinecraftEvent>(responseFromServer);
            return minecraftEvent;
        }

        public async static Task<bool> DeleteMinecraftServer(string name)
        {
            try{
                string url = $"https://hectagonminecraftfunctions.azurewebsites.net/api/DeleteMinecraftServer?name={name}";
                var response = await client.GetAsync(url);
                if(response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            } catch {
                return false;
            }
        }

        public async static Task<bool> CreateMinecraftServer()
        {
            try{
                //string url = "https://hectagonminecraftfunctions.azurewebsites.net/api/CreateMinecraftServer";
                string url = "https://localhost:44359/api/MinecraftServer";
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
            var serverlist = JsonConvert.DeserializeObject<List<MinecraftServer>>(jsonserverlist);
            return serverlist;
        }

        private async static Task<string> GetMinecraftServerListFromAPIAsync()
        {
            try
            {
                //string url = "https://hectagonminecraftfunctions.azurewebsites.net/api/GetMinecraftServerList";
                string url = "https://localhost:44359/api/MinecraftServer";
                //client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            } catch (Exception)
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