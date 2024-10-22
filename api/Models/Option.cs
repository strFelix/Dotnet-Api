using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Option
    {
        public int AnswerId { get; set; }
        public int OptionNumber { get; set; }
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public Answer Answer { get; set; }
    }
}
