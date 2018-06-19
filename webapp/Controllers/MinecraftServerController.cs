using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Controllers
{
    public class MinecraftServerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var viewmodel = await RefreshMinecraftServerViewModel();
            return View(viewmodel);
        }

        private async Task<MinecraftServerViewModel> RefreshMinecraftServerViewModel()
        {
            var viewmodel = new MinecraftServerViewModel();
            viewmodel.MinecraftServers = await MinecraftContext.GetMinecraftServerListAsync();
            return viewmodel;    
        }

        public async Task<IActionResult> Create()
        {
            if(await MinecraftContext.CreateMinecraftServer())
                ViewData["Message"] = "Minecraft server is created.";
            else
                ViewData["Messsage"] = "Error creating minecraft server...";
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            if(await MinecraftContext.DeleteMinecraftServer(id))
                ViewData["Message"] = $"Minecraft server {id} is deleted.";
            else
                ViewData["Messsage"] = $"Error deleting minecraft server {id}...";
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            var serverviewmodel = await RefreshMinecraftServerViewModel();
            var server = serverviewmodel.MinecraftServers.Find(x => x.Name.Contains(id));
            var statsviewmodel = new MinecraftServerStatsViewModel();
            statsviewmodel.Statistics = await MinecraftContext.GetMinecraftServerStatisticsAsync(server));
            return View(statsviewmodel);
        }
    }
}