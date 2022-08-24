using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface ICalendarRepository : IGenericRepository<int, Calendar>
    {
        public IEnumerable<Calendar> GetAllByUserId(int id);
        public string GetOwnerById(int id);
    }
}