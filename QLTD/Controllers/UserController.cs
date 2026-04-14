using Microsoft.AspNetCore.Mvc;
using QLTD.Models.Repository;
using QLTD.Models;

namespace QLTD.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly DataContext _context;

        public UserController(UserRepository userRepository, DataContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        // Display user list
        public IActionResult Index()
        {
            var users = _userRepository.GetAllUsers();
            return View(users);
        }

        // Show user profile
        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                // If no ID provided, use current logged-in user
                var userIdStr = HttpContext.Session.GetString("UserID");
                if (!int.TryParse(userIdStr, out int userId))
                    return RedirectToAction("Login", "Account");
                id = userId;
            }

            var user = _userRepository.GetUserById(id.Value);
            if (user == null) return NotFound();
            return View(user);
        }

        // Handle profile edit
        [HttpPost]
        public IActionResult Profile(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Get the old user data
                var oldUser = _userRepository.GetUserById(user.UserID);

                // If password is the same as old password or empty, keep old password
                if (string.IsNullOrEmpty(user.Password) || user.Password == oldUser?.Password)
                {
                    user.Password = oldUser?.Password ?? user.Password;
                }

                _userRepository.UpdateUser(user);

                // Update session with new user information
                HttpContext.Session.SetString("FullName", user.FullName ?? "");
                HttpContext.Session.SetString("UserName", user.UserName ?? "");
                HttpContext.Session.SetString("Email", user.Email ?? "");

                TempData["Success"] = "Cập nhật thông tin thành công";
                return RedirectToAction("Profile", new { id = user.UserID });
            }
            return View(user);
        }

        // Show register form (User + Company)
        public IActionResult Register()
        {
            var model = new UserRegistrationViewModel
            {
                User = new UserModel(),
                Company = new CompanyModel()
            };
            return View(model);
        }

        // Handle register
        [HttpPost]
        public IActionResult Register(UserRegistrationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Create company first
                    var company = new CompanyModel
                    {
                        CompanyName = model.Company.CompanyName,
                        Email = model.Company.Email,
                        Address = model.Company.Address,
                        Scalecompany = model.Company.Scalecompany,
                        Status = true
                    };

                    _context.Companies.Add(company);
                    _context.SaveChanges();

                    // Create user with company ID
                    var user = new UserModel
                    {
                        FullName = model.User.FullName,
                        UserName = model.User.UserName,
                        Password = model.User.Password,
                        Email = model.User.Email,
                        Position = model.User.Position,
                        Status = true,
                        PermissionID = 1, // Default permission (employee)
                        CompanyID = company.CompanyID
                    };

                    _userRepository.AddUser(user);

                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                        return Ok();

                    return RedirectToAction("Login", "Account");
                }

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return BadRequest(ModelState);

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return BadRequest(ex.Message);
                return View(model);
            }
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

        // Handle delete - Accept both GET and POST
        [HttpGet]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userRepository.DeleteUser(id);
            // Nếu là AJAX, trả về 200 OK
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Ok();
            return RedirectToAction("Users", "Admin");
        }
    }
}
