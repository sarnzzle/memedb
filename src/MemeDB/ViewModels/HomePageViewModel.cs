﻿using MemeDB.Entities;
using System.Collections.Generic;

namespace MemeDB.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Meme> Memes { get; set; }
    }
}
