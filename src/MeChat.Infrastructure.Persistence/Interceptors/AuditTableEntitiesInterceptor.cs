using MeChat.Domain.Abstractions.Enitites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace MeChat.Infrastructure.Persistence.Interceptors;

public sealed class AuditTableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditTableEntitiesInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var utcNow = DateTimeOffset.UtcNow;

        // Date Tracking
        foreach (var entry in dbContext.ChangeTracker.Entries<IDateTracking>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(nameof(IDateTracking.CreatedDate)).CurrentValue = utcNow;
                    break;

                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Property(nameof(IDateTracking.ModifiedDate)).CurrentValue = utcNow;
                    break;
            }
        }

        // Soft Delete
        foreach (var entry in dbContext.ChangeTracker.Entries<ISoftDelete>())
        {
            if (entry.State != EntityState.Deleted)
                continue;

            entry.Property(nameof(ISoftDelete.DeleteAt)).CurrentValue = utcNow;
            entry.Property(nameof(ISoftDelete.IsDeleted)).CurrentValue = true;
            entry.State = EntityState.Modified;
        }

        // Get Current User
        var userIdString =
            _httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? _httpContextAccessor.HttpContext?.User?
                .FindFirst("sub")?.Value;

        Guid.TryParse(userIdString, out var currentUserId);

        // User Tracking
        var userTrackingEntries = dbContext.ChangeTracker
            .Entries<IUserTracking>()
            .Where(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified ||
                x.State == EntityState.Deleted)
            .ToList();

        if (userTrackingEntries.Any() && currentUserId == Guid.Empty)
        {
            throw new InvalidOperationException(
                "Saving entities implementing IUserTracking requires authenticated user.");
        }

        foreach (var entry in userTrackingEntries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(nameof(IUserTracking.CreatedBy)).CurrentValue = currentUserId;
                    break;

                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Property(nameof(IUserTracking.ModifiedBy)).CurrentValue = currentUserId;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
