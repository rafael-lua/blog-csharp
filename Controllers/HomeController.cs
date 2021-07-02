using Blog.Data;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);

            return View(post);
        }

        // Dynamic images use this stream pattern, with routing and controller. Static images can be served directly.
        [HttpGet("/Image/{imageName}")]
        public IActionResult Image(string imageName)
        {
            var mime = imageName.Substring(imageName.LastIndexOf(".") + 1); // get only the type without the dot
            return new FileStreamResult(_fileManager.ImageStream(imageName), $"image/{mime}");
        }
    }
}
