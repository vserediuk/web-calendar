using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly CalendarDbContext _db;

        public EventRepository(CalendarDbContext db) => _db = db;

        public Event Add(Event entity)
        {
            _db.Events.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public void Delete(Event entity)
        {
            _db.Events.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<Event> GetAll() => _db.Events.ToList();

        public Event GetById(int id) => _db.Events.AsNoTracking().Where(e => e.Id == id).FirstOrDefault();
        public string GetOwnerById(int id) =>
            (from calendars in _db.Calendars
             join users in _db.Users on calendars.OwnerUserId equals users.Id
             where calendars.Id == GetById(id).CalendarId
             select users.Email).AsNoTracking().FirstOrDefault();

        public Event Update(Event entity)
        {
            _db.Events.Update(entity);
            _db.Entry(entity)
               .Property(e => e.CalendarId).IsModified = false;
            _db.Entry(entity)
               .Property(e => e.GroupCode).IsModified = false;
            _db.Entry(entity)
               .Property(e => e.JobId).IsModified = false;
            _db.SaveChanges();
            return entity;
        }

        public IEnumerable<Event> GetAllByCalendarId(int id) =>
            (from calendarEvent in _db.Events
             where calendarEvent.CalendarId == id
             select calendarEvent).ToList();

        public void AddRange(IEnumerable<Event> entities)
        {
            _db.Events.AddRange(entities);
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            _db.Events.Remove(_db.Events.Find(id));
            _db.SaveChanges();
        }

        public bool Exists(int id) => _db.Events.AsNoTracking().Any(e => e.Id == id);
    }
}