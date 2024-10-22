using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace api.Models
{
    public class Option
    {
        public int AnswerId { get; set; }
        public int OptionNumber { get; set; }
        public string Description { get; set; } = string.Empty;

        [NotMapped, JsonIgnore]
        public Answer Answer { get; set; } = null!;
    }
}
