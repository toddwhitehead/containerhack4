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
            var viewmodel = new MinecraftServerViewModel();
            viewmodel.MinecraftServers = await MinecraftContext.GetMinecraftServerListAsync();    
            return View(viewmodel);
        }
    }
}