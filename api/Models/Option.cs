using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Option
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public Answer Answer { get; set; } = new Answer();
    }
}
