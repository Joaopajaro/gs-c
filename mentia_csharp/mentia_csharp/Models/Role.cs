using System.ComponentModel.DataAnnotations;

namespace MentiaApi.Models
{
    /// <summary>
    /// Represents a role in the system (e.g. Administrator, User, Mentor). Roles group permissions
    /// and are associated with many users.
    /// </summary>
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<User>? Users { get; set; }
    }
}