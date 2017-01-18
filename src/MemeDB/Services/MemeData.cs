using System;
using System.Collections.Generic;
using System.Linq;
using MemeDB.Entities;

namespace MemeDB.Services
{
    public interface IMemeData
    {
        IEnumerable<Meme> GetAll();
        Meme Get(int id);
        Meme Add(Meme newMeme);
        IEnumerable<Meme> GetByGenre(int id);
        void Delete(Meme meme);
        void Commit();
    }

    public class SqlMemeData : IMemeData
    {
        private MemeDbContext _context;

        public SqlMemeData(MemeDbContext context)
        {
            _context = context;
        }

        public Meme Add(Meme newMeme)
        {
            _context.Add(newMeme);
            _context.SaveChanges();
            return newMeme;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete(Meme meme)
        {
            _context.Remove(meme);
            _context.SaveChanges();
        }

        public Meme Get(int id)
        {
            return _context.Memes.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Meme> GetAll()
        {
            return _context.Memes;
        }

        public IEnumerable<Meme> GetByGenre(int id)
        {
            return _context.Memes.Where(me => me.Genre == (Genre)id);
        }
    }    
}
