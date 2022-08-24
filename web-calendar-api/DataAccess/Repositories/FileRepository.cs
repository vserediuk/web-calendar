using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly CalendarDbContext _db;

        public FileRepository(CalendarDbContext db) => _db = db;

        public File Add(File entity)
        {
            _db.Files.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public void Delete(File entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<File> GetAll()
        {
            throw new NotImplementedException();
        }

        public File GetById(int id) => _db.Files.AsNoTracking().Where(e => e.Id == id).FirstOrDefault();

        public File Update(File entity)
        {
            throw new NotImplementedException();
        }
    }
}
