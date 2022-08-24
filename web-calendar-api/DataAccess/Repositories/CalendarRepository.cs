using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly CalendarDbContext _db;

        public CalendarRepository(CalendarDbContext db) => _db = db;

        public Calendar Add(Calendar calendar)
        {
            _db.Calendars.Add(calendar);
            _db.SaveChanges();
            return calendar;
        }

        public void Delete(Calendar entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Calendar> GetAll()
        {
            throw new NotImplementedException();
        }

        public Calendar GetById(int id) => _db.Calendars.Find(id);
        public string GetOwnerById(int id) =>
            (from calendars in _db.Calendars
             join users in _db.Users on calendars.OwnerUserId equals users.Id
             where calendars.Id == id
             select users.Email).AsNoTracking().FirstOrDefault();

        public Calendar Update(Calendar entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Calendar> GetAllByUserId(int id) =>
            (from calendar in _db.Calendars
                //join userCalendar in _db.UserCalendarRelations on calendar.OwnerUserId equals userCalendar.UserId
                where calendar.OwnerUserId == id
                select calendar).ToList();
    }
}