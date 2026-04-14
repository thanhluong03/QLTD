using System.Collections.Generic;
using System.Linq;
namespace QLTD.Models.Repository
{
    public class UserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        // Add a new user
        public void AddUser(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Update an existing user
        public void UpdateUser(UserModel user)
        {
            var existingUser = _context.Users.Find(user.UserID);
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.Position = user.Position;
                existingUser.Status = user.Status;
                existingUser.PermissionID = user.PermissionID;
                existingUser.CompanyID = user.CompanyID;
                _context.SaveChanges();
            }
        }

        // Delete a user by ID
        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // Optional: Get all users
        public List<UserModel> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Optional: Get user by ID
        public UserModel GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }
    }
}
