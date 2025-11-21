using System.ComponentModel.DataAnnotations;

namespace MentiaApi.Models
{
    /// <summary>
    /// Represents a mentorship session or track. A Mentoria links a mentor (User) to a topic
    /// and contains descriptive information.
    /// </summary>
    public class Mentoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Optional description of the mentorship.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Foreign key to the mentor (User).
        /// </summary>
        [Required]
        public int MentorId { get; set; }

        /// <summary>
        /// Navigation property to the mentor.
        /// </summary>
        public User? Mentor { get; set; }
    }
}