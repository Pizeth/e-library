namespace e_library.Models
{
    public class UserWithToken : User
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Avatar = user.Avatar;   
            this.Username = user.Username;
            this.RoleId = user.RoleId;
            this.Phone = user.Phone;    
            this.Address = user.Address;
            this.Dob = user.Dob;
        }
    }
}
