using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models
{
    public class User
    {
        
        public int Id { get; set; }
        [Required, StringLength(maximumLength:50,MinimumLength = 1)]
        public string UserName { get; set; }

        [Required, StringLength(maximumLength:50,MinimumLength = 1)]
        public string UserPwd { get; set; }

    }
}