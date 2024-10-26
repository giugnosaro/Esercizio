// Controllers/PostController.cs
using Esercizio.Models;
using Esercizio.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Esercizio.Controllers
{
    public class PostController : Controller
    {
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;

        public PostController(PostRepository postRepository, UserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.UserId = (int)HttpContext.Session.GetInt32("UserId");
                _postRepository.AddPost(post);
                return RedirectToAction("Index"); 
            }
            return View(post);
        }

        public IActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId"); 
            var posts = _postRepository.GetPostsByUserId(userId);
            return View(posts);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _postRepository.DeletePost(id);
            return RedirectToAction("Index");
        }
    }
}
