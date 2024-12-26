using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public List<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}