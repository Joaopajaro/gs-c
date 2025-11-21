using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentiaApi.Models
{
    /// <summary>
    /// Represents a user in the Mentia platform. Each user has a Role and can act as a mentor.
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key to the Role entity.
        /// </summary>
        [Required]
        public int RoleId { get; set; }
        
        /// <summary>
        /// Navigation property to the Role.
        /// </summary>
        public Role? Role { get; set; }
    }
}