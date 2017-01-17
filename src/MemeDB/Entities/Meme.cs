using System.ComponentModel.DataAnnotations;

namespace MemeDB.Entities
{
    public enum Genre
    {
        None,
        Cats,
        Gif,
        Reactions,
        RageComic
    }

    public class Meme
    {
        public int Id { get; set; }
        [Required, MaxLength(80)]
        [Display(Name="Meme Name")]
        public string Name { get; set; }
        public Genre Genre { get; set; }
        [Required, MaxLength(255)]
        [Display(Name="Meme URL")]
        public string Url { get; set; }
        [MaxLength(255)]
        [Display(Name="Description")]
        public string Description { get; set; }
    }
}
