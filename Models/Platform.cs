using System.ComponentModel.DataAnnotations;

namespace Assessment.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        public string UniqeuName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Well> Wells { get; set; } = new List<Well>();
    }
}
