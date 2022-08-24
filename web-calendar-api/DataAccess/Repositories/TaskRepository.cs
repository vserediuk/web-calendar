using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly CalendarDbContext _db;

        public Task Add(Task entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Task entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Task entity)
        {
            throw new NotImplementedException();
        }
    }
}