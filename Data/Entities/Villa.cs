using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MagicVilla_DB.Data.Stores
{
    [Table(name: "villas")]
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Column(name: "town_id")]
        [ForeignKey("Town")]
        [JsonIgnore]
        public Guid? TownId { get; set; }
        public virtual Town? Town { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(name: "name")]
        public string Name { get; set; }

        [MaxLength(255)]
        [Column(name: "details")]
        public string? Details { get; set; }

        [Column(name: "rate")]
        public double Rate { get; set; }

        [Column(name: "occupancy")]
        public int Occupancy { get; set; }

        [MaxLength(255)]
        [Column(name: "image_url")]
        public string? ImageUrl { get; set; }

        [Column(name: "amenity")]
        public string? Amenity { get; set; } = "";

        [Column(name: "created_date")]
        public DateTime CreatedDate { get; set; }

        [Column(name: "updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}
