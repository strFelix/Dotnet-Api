using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }

        [NotMapped]
        public User User { get; set; } = new User();

        [NotMapped]
        public Answer Answer { get; set; } = new Answer();
    }
}
