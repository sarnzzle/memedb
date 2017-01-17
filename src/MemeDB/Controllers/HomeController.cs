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
        private IGreeter _greeter;
        private IMemeData _memeData;

        public HomeController(IMemeData memeData, IGreeter greeter)
        {
            _memeData = memeData;
            _greeter = greeter;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Memes = _memeData.GetAll();
            model.CurrentMessage = _greeter.GetGreeting();

            return View(model);
        }
        
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
