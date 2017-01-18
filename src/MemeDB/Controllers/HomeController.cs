using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MemeDB.Entities;
using MemeDB.Services;
using MemeDB.ViewModels;

namespace MemeDB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IMemeData _memeData;

        public HomeController(IMemeData memeData)
        {
            _memeData = memeData;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Memes = _memeData.GetAll();

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Genre(int id)
        {
            var model = new GenrePageViewModel();
            model.Memes = _memeData.GetByGenre(id);
            model.Genre = (Genre)id;

            return View(model);
        }
        
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var model = _memeData.Get(id);
            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _memeData.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, MemeEditViewModel model)
        {
            var meme = _memeData.Get(id);
            if (ModelState.IsValid)
            {
                meme.Genre = model.Genre;
                meme.Name = model.Name;
                meme.Description = model.Description;
                meme.Url = model.Url;
                _memeData.Commit();

                return RedirectToAction("Details", new { id = meme.Id });
            }
            else
            {
                return View(meme);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MemeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newMeme = new Meme();
                newMeme.Genre = model.Genre;
                newMeme.Name = model.Name;
                newMeme.Description = model.Description;
                newMeme.Url = model.Url;

                newMeme = _memeData.Add(newMeme);
                _memeData.Commit();

                return RedirectToAction("Details", new { id = newMeme.Id });
            }
            else
            {
                return View();
            }
        }
    }
}
