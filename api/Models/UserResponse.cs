using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace api.Models
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }

        [NotMapped, JsonIgnore]
        public User User { get; set; } = null!;

        [NotMapped, JsonIgnore]
        public Answer Answer { get; set; } = null!;
    }
}
