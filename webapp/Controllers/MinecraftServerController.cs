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

        public async Task<IActionResult> Delete(string name)
        {
            if(await MinecraftContext.DeleteMinecraftServer(name))
                ViewData["Message"] = "Minecraft server is deleted.";
            else
                ViewData["Messsage"] = "Error deleting minecraft server...";
            return View();
        }
    }
}