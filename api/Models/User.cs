using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        [NotMapped]
        public List<UserResponse> Responses { get; set; } = new List<UserResponse>();
    }
}
