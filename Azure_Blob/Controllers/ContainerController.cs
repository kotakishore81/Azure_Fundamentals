﻿using Azure_Blob.Models;
using Azure_Blob.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_Blob.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerService _containerService;

        public ContainerController(IContainerService containerService) {
            _containerService = containerService;
        }
        public  async Task<IActionResult> Index()
        {
            var allContainer = await _containerService.GetAllContainer();
            return View(allContainer);
        }
        public async Task<IActionResult> Delete(string containerName)
        {
            await _containerService.DeleteContainer(containerName);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Create()
        {          
            return View(new Container());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Container container)
        {
            await _containerService.CreateContainer(container.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}
