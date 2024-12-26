using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class DirectorService : ServiceBase, IService<Director, DirectorModel>
    {
        public DirectorService(Db db) : base(db)
        {
        }

        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors.OrderBy(d => d.Name)
                              .ThenBy(d => d.Surname)
                              .Select(d => new DirectorModel() { Record = d });
        }

        public ServiceBase Create(Director record)
        {
            if (_db.Directors.Any(d => d.Name.ToUpper() == record.Name.ToUpper().Trim() &&
                                     d.Surname.ToUpper() == record.Surname.ToUpper().Trim()))
                return Error("Director with the same name and surname exists!");

            record.Name = record.Name?.Trim();
            record.Surname = record.Surname?.Trim();
            _db.Directors.Add(record);
            _db.SaveChanges();
            return Success("Director created successfully.");
        }

        public ServiceBase Update(Director record)
        {
            if (_db.Directors.Any(d => d.Id != record.Id &&
                                     d.Name.ToUpper() == record.Name.ToUpper().Trim() &&
                                     d.Surname.ToUpper() == record.Surname.ToUpper().Trim()))
                return Error("Director with the same name and surname exists!");

            var entity = _db.Directors.SingleOrDefault(d => d.Id == record.Id);
            if (entity is null)
                return Error("Director not found!");

            entity.Name = record.Name?.Trim();
            entity.Surname = record.Surname?.Trim();
            entity.IsRetired = record.IsRetired;

            _db.Directors.Update(entity);
            _db.SaveChanges();
            return Success("Director updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.Include(d => d.Movies)
                                    .SingleOrDefault(d => d.Id == id);
            if (entity is null)
                return Error("Director not found!");

            

            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return Success("Director deleted successfully.");
        }
    }
}