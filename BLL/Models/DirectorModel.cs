using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class DirectorModel
    {
        public Director Record { get; set; }

        public string Name => Record.Name;

        public string Surname => Record.Surname;

        [DisplayName("Full Name")]
        public string FullName => Record.Name + " " + Record.Surname;

        [DisplayName("Retired")]
        public string IsRetired => Record.IsRetired ? "Yes" : "No";
    }
}