using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string? Email { get; set; }

        public string? avatar { get; set; }

    }
}