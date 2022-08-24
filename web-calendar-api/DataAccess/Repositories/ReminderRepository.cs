using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly CalendarDbContext _db;

        public Reminder Add(Reminder entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Reminder entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reminder> GetAll()
        {
            throw new NotImplementedException();
        }

        public Reminder GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Reminder Update(Reminder entity)
        {
            throw new NotImplementedException();
        }
    }
}