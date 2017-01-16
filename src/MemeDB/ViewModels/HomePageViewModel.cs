using MemeDB.Entities;
using System.Collections.Generic;

namespace MemeDB.ViewModels
{
    public class HomePageViewModel
    {
        public string CurrentMessage { get; set; }
        public IEnumerable<Meme> Memes { get; set; }
    }
}
