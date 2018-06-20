using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using k8s;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinecraftServerController : ControllerBase
    {
        private static KubernetesClientConfiguration localConfig = new KubernetesClientConfiguration { Host = "http://127.0.0.1:8001" };
        private Kubernetes client;

        [HttpPost]
        public ActionResult<bool> Create()
        {
            ConnectToKube();
            try
            {
                var model = new k8s.Models.V1Deployment();
                model.ApiVersion = "V1";
                model.Kind = "Deployment";
                var guid = new Guid();
                model.Metadata = new k8s.Models.V1ObjectMeta();
                model.Metadata.Name = String.Concat("minecraft", guid);
                model.Spec = new k8s.Models.V1DeploymentSpec();
                model.Spec.Replicas = 1;
                model.Spec.Template = new k8s.Models.V1PodTemplateSpec();
                model.Spec.Template.Metadata = new k8s.Models.V1ObjectMeta();
                model.Spec.Template.Metadata.Labels = new Dictionary<string, string>();
                model.Spec.Template.Metadata.Labels.Add(new KeyValuePair<string, string>( "app", model.Metadata.Name ));
                model.Spec.Template.Spec = new k8s.Models.V1PodSpec();
                model.Spec.Template.Spec.Containers = new List<k8s.Models.V1Container>();
                model.Spec.Template.Spec.Containers.Add(new k8s.Models.V1Container()
                {
                    Name = "minecraft",
                    Image = "openhack/minecraft-server:1.0"
                });
                model.Spec.Template.Spec.Containers[0].Ports = new List<k8s.Models.V1ContainerPort>();
                model.Spec.Template.Spec.Containers[0].Ports.Add(new k8s.Models.V1ContainerPort()
                {
                    Name = "minecraft",
                    ContainerPort = 25565
                });
                model.Spec.Template.Spec.Containers[0].Ports.Add(new k8s.Models.V1ContainerPort()
                {
                    Name = "rcon",
                    ContainerPort = 25575
                });
                model.Spec.Template.Spec.Containers[0].Env = new List<k8s.Models.V1EnvVar>();
                model.Spec.Template.Spec.Containers[0].Env.Add(new k8s.Models.V1EnvVar()
                {
                    Name = "EULA",
                    Value = "TRUE"
                });
                client.CreateNamespacedDeployment(model, "default");
            } catch (Exception e)
            {
                return false;
            }
            return true;
        }
        [HttpGet]
        public ActionResult<IEnumerable<MinecraftServer>> Get()
        {
            //ConnectToKubeCorrectly();
            ConnectToKube();
            try
            {
                var deploymentList = client.ListNamespacedDeployment("default");
                var serviceList = client.ListNamespacedService("default");
                var serverList = new List<MinecraftServer>();
                MinecraftServer server;
                KeyValuePair<string, string> selector;
                foreach (var deployment in deploymentList.Items)
                {
                    server = new MinecraftServer
                    {
                        Name = deployment.Metadata.Name,
                        Endpoints = new List<MinecraftServerEndpoint>()
                    };
                    selector = new KeyValuePair<string, string>("app", server.Name);
                    foreach (var service in serviceList.Items.Where(x => x.Spec.Selector != null && x.Spec.Selector.Contains(selector)))
                    {
                        foreach(var port in service.Spec.Ports)
                        {
                            server.Endpoints.Add(new MinecraftServerEndpoint() { Name = port.Name, Address = String.Concat(service.Spec.ClusterIP, ":", port.Port) });
                        }
                    }
                    serverList.Add(server);
                }
                return serverList;
            }
            catch (Microsoft.Rest.HttpOperationException httpOperationException)
            {
                var phase = httpOperationException.Response.ReasonPhrase;
                var content = httpOperationException.Response.Content;
                throw httpOperationException;
            }
            
        }

        private void ConnectToKubeCorrectly()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            client = new Kubernetes(config);
            client.ListNamespacedPod("default");
        }
        private void ConnectToKube()
        {
            //var token = Environment.GetEnvironmentVariable("TOKEN");
            //var serviceCredentials = new Microsoft.Rest.TokenCredentials(token);
            //var uri = new Uri("https://hectagoncl-minecrafthectago-8e81bd-5ad96b4a.hcp.westus2.azmk8s.io");
            //var client = new Kubernetes(uri, serviceCredentials);
            //var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            //config = new KubernetesClientConfiguration { Host = "http://127.0.0.1:8001" };
            client = new Kubernetes(localConfig);
        }
    }
}