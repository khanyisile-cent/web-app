using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceAndBooking.Models
{
    public class Bookings
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [NotMapped]
        public string BookedBy { get; set; } = null!;

        [Required]
        public string Purpose { get; set; } = null!;

        // Navigation properties
        public virtual Employee Employee { get; set; } = null!;
        public virtual Resource Resource { get; set; } = null!;
    }
}