using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models.DTOs;

public class TodoCreateDTO
{
    [Required(ErrorMessage = "Header Required"),
     MinLength(3, ErrorMessage = "Title must be at least 3 characters long"),
     MaxLength(20, ErrorMessage = "Title cannot exceed 20 characters")
     ]
    public string Title { get; set; } = "";
    [Required(ErrorMessage = "Description Required"),
     MinLength(5, ErrorMessage = "Description must be at least 5 characters long"),
     MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")
     ]
    public string Description { get; set; } = "";
    public bool IsCompleted { get; set; }
}
