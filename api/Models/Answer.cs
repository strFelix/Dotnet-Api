using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int CorrectOption {  get; set; }

        [NotMapped]
        public List<Option> Options { get; set; } = new List<Option>();

        [NotMapped]
        public List<UserResponse> Responses { get; set; } = new List<UserResponse>();
    }
}
