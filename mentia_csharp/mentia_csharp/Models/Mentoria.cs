using System.ComponentModel.DataAnnotations;

namespace MentiaApi.Models
{

    public class Mentoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

  
        public string? Description { get; set; }

        
        [Required]
        public int MentorId { get; set; }

      
        public User? Mentor { get; set; }
    }
}
