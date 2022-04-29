using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Model
{
    public class UserProfile
    {
        /// <summary>
        /// User Name
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        /// <summary>
        /// Pass Word
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
