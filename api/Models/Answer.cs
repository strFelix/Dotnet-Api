using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace api.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int CorrectOption {  get; set; }

        [NotMapped]
        public List<Option> Options { get; set; } = new List<Option>();

        [NotMapped, JsonIgnore]
        public List<UserResponse> Responses { get; set; } = new List<UserResponse>();
    }
}
