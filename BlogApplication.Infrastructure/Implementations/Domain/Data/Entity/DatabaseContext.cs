using System;
using System.Data.Entity;
using System.Threading.Tasks;
using BlogApplication.Domain.Interfaces.Data.Entity;
using BlogApplication.Domain.Interfaces.Providers;
using BlogApplication.Infrastructure.Database;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Constants.Messages;
using BlogApplication.Models.Entities.Base;
using BlogApplication.Models.Enums;
using BlogApplication.Shared.Interfaces.Facades;

namespace BlogApplication.Infrastructure.Implementations.Domain.Data.Entity
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public class DatabaseContext : IDatabaseContext
    {
        private readonly BlogDbContext _context;
        private readonly IDateTimeFacade _dateTimeFacade;
        private readonly IHttpContextProvider _httpContextProvider;

        public DatabaseContext(
            BlogDbContext context,
            IDateTimeFacade dateTimeFacade,
            IHttpContextProvider httpContextProvider)
        {
            _context = context;
            _dateTimeFacade = dateTimeFacade;
            _httpContextProvider = httpContextProvider;
        }

        public async Task<int> SaveChangesAsync()
        {
            TrackChanges();
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CurrentTransaction?.Commit();
        }

        public void Rollback()
        {
            _context.Database.CurrentTransaction?.Rollback();
        }

        private void TrackChanges()
        {
            var dateTime = _dateTimeFacade.Now();
            var userId = _httpContextProvider.UserId;

            if (string.IsNullOrEmpty(userId))
                throw new Exception(ExceptionMessages.UserIsNotLoggedIn);

            var entries = _context.ChangeTracker.Entries();

            foreach (var entry in entries)
                if (entry.Entity is EntityBase auditable)
                {
                    auditable.ModifiedDateTime = dateTime;
                    auditable.ModifiedByUserId = userId;

                    switch (entry.State)
                    {
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Added:
                            auditable.AddedDateTime = dateTime;
                            auditable.AddedByUserId = userId;
                            break;
                        case EntityState.Deleted:
                            break;
                        case EntityState.Modified:
                            _context.Entry(auditable).Property(t => t.AddedDateTime).IsModified = false;
                            _context.Entry(auditable).Property(t => t.AddedByUserId).IsModified = false;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
        }
    }
}