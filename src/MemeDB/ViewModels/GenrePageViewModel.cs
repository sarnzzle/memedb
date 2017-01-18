using MemeDB.Entities;
using System.Collections.Generic;

namespace MemeDB.ViewModels
{
    public class GenrePageViewModel
    {
        public Genre Genre { get; set; }
        public IEnumerable<Meme> Memes { get; set; }
    }
}
