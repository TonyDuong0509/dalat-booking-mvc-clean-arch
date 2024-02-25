using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaLatBooking.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Giá 1 đêm")]
        [Range(400, 2000000)]
        public double Price { get; set; }
        public int Sqft { get; set; }
        [Range(1, 10)]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name = "Đường dẫn hình ảnh")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity> VillaAmenitiy { get; set; }
        [NotMapped]
        public bool IsAvailable { get; set; } = true;
    }
}
