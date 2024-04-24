using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MagicVilla_DB.Data.Stores
{
    [Table(name: "towns")]
    public class Town
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(name: "name")]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Villa>? Villas { get; set; }

    }
}
