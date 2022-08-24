using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess
{
    public class CalendarDbContext : IdentityUserContext<User, int>
    {
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<File> Files { get; set; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(exp => exp.Calendars)
                .WithOne(exp => exp.User)
                .HasForeignKey(fk => fk.OwnerUserId)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasMany(exp => exp.SharedCalendars)
                .WithMany(exp => exp.InvitedUsers);

            modelBuilder.Entity<Event>()
                .HasOne(exp => exp.File)
                .WithOne(exp => exp.Event)
                .IsRequired(false);

            modelBuilder.Entity<User>().Property(u => u.FirstName).HasMaxLength(24).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).HasMaxLength(24).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(36).IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Calendar>().Property(c => c.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Calendar>().Property(c => c.OwnerUserId).IsRequired();

            modelBuilder.Entity<Event>().Property(e => e.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Event>().Property(e => e.CalendarId).IsRequired();
            modelBuilder.Entity<Event>().Property(e => e.StartDate).IsRequired();
            modelBuilder.Entity<Event>().Property(e => e.EndDate).IsRequired();
            modelBuilder.Entity<Event>().Property(e => e.RepeatTypeId).IsRequired();

            modelBuilder.Entity<File>().Property(f => f.Name).IsRequired();
            modelBuilder.Entity<File>().Property(f => f.Path).IsRequired();
        }
    }
}