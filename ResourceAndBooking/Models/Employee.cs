using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResourceAndBooking.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string Surname { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public required ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();
    }
}