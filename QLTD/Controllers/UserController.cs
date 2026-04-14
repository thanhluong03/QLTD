using Microsoft.AspNetCore.Mvc;
using QLTD.Models.Repository;
using QLTD.Models;

namespace QLTD.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Display user list
        public IActionResult Index()
        {
            var users = _userRepository.GetAllUsers();
            return View(users);
        }

        // Show create form
        public IActionResult Create()
        {
            return View(new UserModel());
        }

        // Handle create
        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);
                // Nếu là AJAX, trả về 200 OK
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Ok();
                return RedirectToAction(nameof(Index));
            }
            // Nếu là AJAX, trả về lỗi
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return BadRequest();
            return View(user);
        }


        // Show edit form
        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // Handle edit
        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.UpdateUser(user);
                // Nếu là AJAX, trả về 200 OK
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Ok();
                return RedirectToAction(nameof(Index));
            }
            // Nếu là AJAX, trả về lỗi
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return BadRequest();
            return View(user);
        }

        // Show delete confirmation
        public IActionResult DeleteConfirm(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound();
            return View("Delete", user);
        }

        // Handle delete (AJAX or normal)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userRepository.DeleteUser(id);
            // Nếu là AJAX, trả về 200 OK
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Ok();
            return RedirectToAction(nameof(Index));
        }
    }
}
