using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IGenreService
    {
        public IQueryable<GenreModel> Query();
        public ServiceBase Create(Genre record);
        public ServiceBase Update(Genre record);
        public ServiceBase Delete(int id);
    }

    public class GenreService : ServiceBase, IGenreService
    {
        public GenreService(Db db) : base(db)
        {
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderBy(g => g.Name)
                           .Select(g => new GenreModel() { Record = g });
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(g => g.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists!");

            record.Name = record.Name?.Trim();
            _db.Genres.Add(record);
            _db.SaveChanges();
            return Success("Genre created successfully.");
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(g => g.Id != record.Id &&
                                   g.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists!");

            var entity = _db.Genres.SingleOrDefault(g => g.Id == record.Id);
            if (entity is null)
                return Error("Genre not found!");

            entity.Name = record.Name?.Trim();
            _db.Genres.Update(entity);
            _db.SaveChanges();
            return Success("Genre updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(g => g.MovieGenres)
                                  .SingleOrDefault(g => g.Id == id);
            if (entity is null)
                return Error("Genre not found!");


            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully.");
        }
    }
}