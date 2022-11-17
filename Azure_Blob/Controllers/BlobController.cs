﻿using Azure_Blob.Models;
using Azure_Blob.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_Blob.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }
        [HttpGet]
        public async Task<IActionResult> Manage(string containerName)
        {
            var blobObj = await _blobService.GetAllBlobs(containerName);
            return View(blobObj);
        }
        [HttpGet]
        public  IActionResult AddFile(string containerName)
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  AddFile(string containerName, Blob blob, IFormFile file)
        {
            if (file == null || file.Length < 1) return View();

            //file name - xps_img2.png 
            //new name - xps_img2_GUIDHERE.png
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            var result = await _blobService.UploadBlob(fileName, file, containerName, blob);

            if (result)
                return RedirectToAction("Index", "Container");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ViewFile(string name, string containerName)
        {

            return Redirect(await _blobService.GetBlob(name,containerName));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteFile(string name, string containerName)
        {
            await _blobService.DeleteBlob(name, containerName);
            return RedirectToAction("Index", "Home");
        }
    }
}