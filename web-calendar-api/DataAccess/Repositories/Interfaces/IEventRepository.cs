using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IEventRepository : IGenericRepository<int, Event>
    {
        public IEnumerable<Event> GetAllByCalendarId(int id);
        public string GetOwnerById(int id);
        public void AddRange(IEnumerable<Event> entities);
        public void DeleteById(int id);
        public bool Exists(int id);
    }
}