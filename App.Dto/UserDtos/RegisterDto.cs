

namespace App.Dto.UserDtos
{
    public class RegisterDto
    {

        public string UserFullName { get; set; }
        public string UserProfilePictureUrl { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }

    }
}
