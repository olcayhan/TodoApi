using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Todo : BaseEntity
    {
        [Required]
        public string Title { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        public bool IsCompleted { get; set; }
    }
}