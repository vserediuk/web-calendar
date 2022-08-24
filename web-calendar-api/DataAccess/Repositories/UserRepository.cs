using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CalendarDbContext _db;

        public UserRepository(CalendarDbContext db)
        {
            _db = db;
        }

        public User Add(User entity)
        {
            _db.Users.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id) => _db.Users.Find(id);

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}