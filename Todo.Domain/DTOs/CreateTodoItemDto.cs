using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class CreateTodoItemDto
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime? DueDate { get; set; }
    }
}
