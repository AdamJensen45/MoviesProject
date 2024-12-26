using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class MovieService : ServiceBase, IService<Movie, MovieModel>
    {
        public MovieService(Db db) : base(db)
        {
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies.Include(m => m.Director)
                           .Include(m => m.MovieGenres)
                           .ThenInclude(mg => mg.Genre)
                           .OrderByDescending(m => m.ReleaseDate)
                           .ThenBy(m => m.Name)
                           .Select(m => new MovieModel() { Record = m });
        }

        public ServiceBase Create(Movie record)
        {
            if (_db.Movies.Any(m => m.Name.ToLower() == record.Name.ToLower().Trim() &&
                                   m.DirectorId == record.DirectorId))
                return Error("Movie with the same name and director already exists!");

            record.Name = record.Name?.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges();
            return Success("Movie created successfully.");
        }

        public ServiceBase Update(Movie record)
        {
            if (_db.Movies.Any(m => m.Id != record.Id &&
                                   m.Name.ToLower() == record.Name.ToLower().Trim() &&
                                   m.DirectorId == record.DirectorId))
                return Error("Movie with the same name and director already exists!");

            var entity = _db.Movies.Include(m => m.MovieGenres)
                                 .SingleOrDefault(m => m.Id == record.Id);
            if (entity is null)
                return Error("Movie not found!");

            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            entity.Name = record.Name?.Trim();
            entity.ReleaseDate = record.ReleaseDate;
            entity.TotalRevenue = record.TotalRevenue;
            entity.DirectorId = record.DirectorId;
            entity.MovieGenres = record.MovieGenres;

            _db.Movies.Update(entity);
            _db.SaveChanges();
            return Success("Movie updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies.Include(m => m.MovieGenres)
                                 .SingleOrDefault(m => m.Id == id);
            if (entity is null)
                return Error("Movie not found!");

            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return Success("Movie deleted successfully.");
        }
    }
}