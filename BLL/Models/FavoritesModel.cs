using System.ComponentModel;

namespace BLL.Models
{
    public class FavoritesModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Movie Name")]
        public string MovieName { get; set; }
    }
}