using System.ComponentModel.DataAnnotations;

namespace ResourceAndBooking.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();
    }
}