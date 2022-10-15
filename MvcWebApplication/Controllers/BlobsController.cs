using AzureStorageLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcWebApplication.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApplication.Controllers
{
    public class BlobsController : Controller
    {
        private readonly IBlogStorage _blogStorage;

        public BlobsController(IBlogStorage blogStorage)
        {
            _blogStorage = blogStorage;
        }

        public async Task<IActionResult> Index()
        {
            var names = _blogStorage.GetNames(EcontainerName.pictures);
            var blobUrl = $"{_blogStorage.BlobUrl}/{EcontainerName.pictures.ToString()}";
            ViewBag.blobs = names.Select(x => new FileBlob { Name = x, Url = $"{blobUrl}/{x}" }).ToList();

            ViewBag.logs = await _blogStorage.GetLogAsync("controller.txt");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile picture)
        {
            await _blogStorage.SetLogAsync("Upload methoduna giriş yapıldı", "controller.txt");
            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
            await _blogStorage.UploadAsync(picture.OpenReadStream(), newFileName, EcontainerName.pictures);
            await _blogStorage.SetLogAsync("Upload methoduna çıkış yapıldı", "controller.txt");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string fileName)
        {
           var stream = await _blogStorage.DownloadAsync(fileName, EcontainerName.pictures);
            return File(stream, "application/octet-stream", fileName);
        }

        public async Task<IActionResult> Delete(string fileName)
        {
            await _blogStorage.DeleteAsync(fileName, EcontainerName.pictures);
            return RedirectToAction("Index");
        }
    }
}