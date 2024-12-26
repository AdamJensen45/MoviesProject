using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoviesProject.Controllers
{
    [Authorize]
    public class FavoritesController : MvcController
    {
        const string SESSIONKEY = "Favorites";

        private readonly HttpServiceBase _httpService;
        private readonly IService<Movie, MovieModel> _movieService;

        public FavoritesController(HttpServiceBase httpService, IService<Movie, MovieModel> movieService)
        {
            _httpService = httpService;
            _movieService = movieService;
        }

        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == "Id").Value);

        private List<FavoritesModel> GetSession(int userId)
        {
            var favorites = _httpService.GetSession<List<FavoritesModel>>(SESSIONKEY);
            return favorites?.Where(f => f.UserId == userId).ToList();
        }

        public IActionResult Get()
        {
            return View("List", GetSession(GetUserId()));
        }

        public IActionResult Remove(int movieId)
        {
            var favorites = GetSession(GetUserId());
            var favoritesItem = favorites.FirstOrDefault(c => c.MovieId == movieId);
            favorites.Remove(favoritesItem);
            _httpService.SetSession(SESSIONKEY, favorites);
            return RedirectToAction(nameof(Get));
        }

        public IActionResult Add(int movieId)
        {
            int userId = GetUserId();
            var favorites = GetSession(userId);
            favorites = favorites ?? new List<FavoritesModel>();
            if (!favorites.Any(f => f.MovieId == movieId))
            {
                var movie = _movieService.Query().SingleOrDefault(p => p.Record.Id == movieId);
                var favoritesItem = new FavoritesModel()
                {
                    MovieId = movieId,
                    UserId = userId,
                    MovieName = movie.Name
                };
                favorites.Add(favoritesItem);
                _httpService.SetSession(SESSIONKEY, favorites);
                TempData["Message"] = $"\"{movie.Name}\" added to favorites.";
            }
            return RedirectToAction("Index", "Movies");
        }
    }
}