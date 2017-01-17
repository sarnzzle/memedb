using MemeDB.Entities;
using System.ComponentModel.DataAnnotations;

namespace MemeDB.ViewModels
{
    public class MemeEditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public Genre Genre { get; set; }
        [Required, MaxLength(255)]
        public string Url { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
    }
}
