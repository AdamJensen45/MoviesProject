using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class MovieModel
    {
        public Movie Record { get; set; }

        public string Name => Record.Name;

        [DisplayName("Release Date")]
        public string ReleaseDate => !Record.ReleaseDate.HasValue ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy");

        [DisplayName("Total Revenue")]
        public string TotalRevenue => Record.TotalRevenue.HasValue ? Record.TotalRevenue.Value.ToString("N2") : "0";

        public string Director => Record.Director?.Name + " " + Record.Director?.Surname;

        // For displaying genres as string
        public string Genres => string.Join("<br>", Record.MovieGenres?.Select(mg => mg.Genre?.Name));

        [DisplayName("Genres")]
        public List<int> GenreIds
        {
            get => Record.MovieGenres?.Select(mg => mg.GenreId).ToList();
            set => Record.MovieGenres = value.Select(v => new MovieGenre() { GenreId = v }).ToList();
        }
    }
}