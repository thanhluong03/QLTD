namespace QLTD.Models
{
    public class UserRegistrationViewModel
    {
        public UserModel User { get; set; } = new UserModel();
        public CompanyModel Company { get; set; } = new CompanyModel();
    }
}
