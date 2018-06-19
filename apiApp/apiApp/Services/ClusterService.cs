using apiApp.Models;
using apiApp.Response;
using apiApp.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace apiApp.Services
{
    public class ClusterService : IClusterService
    {
        public RestClient Client { get; set; }

        public ClusterService()
        {
            Client = new RestClient("http://aks-jlloyd-rg-ae-jlloyd-ope-8e81bd-01447977.hcp.australiaeast.azmk8s.io/api/v1/namespaces/default/services");
        }

        public string GetServerList()
        {
            Client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            var response = Client.Execute<ServiceList>(GetRequest());
            var serverListResult = response.Data;

            var serverList = new List<ServerObj>();

            var serverListResponse = serverListResult.Items.Where(x => x?.Status?.loadBalancer != null).ToList();

            return Newtonsoft.Json.JsonConvert.SerializeObject(serverListResponse);
        }


        private RestRequest GetRequest()
        {
            var request = new RestRequest(Method.GET);

            
            var bearerToken = Environment.GetEnvironmentVariable("AUTHORIZATION");

            if (bearerToken != null)
                request.AddHeader("Authorization", bearerToken);
            else
                request.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJrdWJlcm5ldGVzL3NlcnZpY2VhY2NvdW50Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9uYW1lc3BhY2UiOiJkZWZhdWx0Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9zZWNyZXQubmFtZSI6ImFwaXVzZXItdG9rZW4tNnp2aHgiLCJrdWJlcm5ldGVzLmlvL3NlcnZpY2VhY2NvdW50L3NlcnZpY2UtYWNjb3VudC5uYW1lIjoiYXBpdXNlciIsImt1YmVybmV0ZXMuaW8vc2VydmljZWFjY291bnQvc2VydmljZS1hY2NvdW50LnVpZCI6ImJiMTViZWNkLTczNzgtMTFlOC05YTFiLTFhMGZlZTI3M2JmNiIsInN1YiI6InN5c3RlbTpzZXJ2aWNlYWNjb3VudDpkZWZhdWx0OmFwaXVzZXIifQ.PhCJaDx7gMm88shFnhAQbR-YdEy3ncqDUnk9tz652yxhCiJtKpn_ZPsmsMNEWuPipIUch9y1ob6vTp1-p0EV1aHZ8vhlXqK7Ctlvk0I_W1S21RNjsq_C7pnJZTU0TJ6YgtMlYIQIiUa4A3BnAVldEHmjxYpnv9mamNlwvLn2r-FcszR0MQvh9wxqKMJMKkTo9jaiAhvra1rePmjtyJJg4QLcf0dNOJB-kGhp9CoaM-JYGbgvM9OWsKOlZ6kJsqSUxKGzeD-gToxr6Ai191Iahko_F0fJhgNpZc0yAxkx0urI2lPc_RM01t243hE8bxbZeMlA5ZqDGbFb39oRwa3BF7nWO3NFxQPj0HRvONnuwiZZGGx6kHTKeeBOC1rPU1qBZR7MAgZDSMpkefnTJxl9bO_bys6yLqZiVdSgchpgGalZzOG0qSZ7zqu0zR5DrSm9ISh2a31h86EnNQe_gcswGfhYbop-43C8jJJ8LNL5oKCWM8i2MgodwAS9ArPWgGW7T4e9udh7WDA6xyWLkyLPO0j5OAXKdlE_oxlSAu7xYci-vGaISCAcJNxPOKmqqiaCVA1NEdR5asPllL_DcQP5GGg9XxaYVLPEfa2koVc5J0XRSSpt4kBHFdqBCSKVzdRUzDw6Eioi0sEQXpAJ0pBFdfTLWhkTeseFvGN_qSo8ZBk");
  
            return request;
        }
    }
}
