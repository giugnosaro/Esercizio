using Esercizio.Models;
using Esercizio.Service;
using Microsoft.AspNetCore.Mvc;

namespace Esercizio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Profile(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateProfile(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.UpdateUser(user);
                return RedirectToAction("Profile", new { id = user.Id });
            }
            return View("Profile", user);
        }

        [HttpPost]
        public IActionResult DeleteAccount(int id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Register");
        }
    }
}
