
namespace Lean.Contracts.Administration
{
    public class UserRegistrationInputDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UserLoginInputDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}