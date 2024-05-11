using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Npgsql;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort;

/// <summary>
/// Technology-specific implementation of <see cref="IDataContext"/>.
/// </summary>
public class PostgresPortDataContext : DbContext, IDataContext
{
    // Associated Database entries
    public DbSet<ToDoList>? TodoLists { get; set; }

    public DbSet<ToDoItem>? TodoItems { get; set; }

    public DbSet<XivotecRecipe>? XivotecRecipes { get; set; }

    public DbSet<FeatherDeviceRecipe>? FeatherDeviceRecipes { get; set; }

    private const string DbErrorString = "Database operation failed. \nCheck configuration " +
        "and server status and restart the application.";

    private readonly ILogger<PostgresPortDataContext> _logger;

    // Needed for generating Migrations independent of main application
    public PostgresPortDataContext()
    {
        _logger = new NullLogger<PostgresPortDataContext>();
    }

    public PostgresPortDataContext(
        DbContextOptions<PostgresPortDataContext> options,
        ILogger<PostgresPortDataContext> logger)
        : base(options)
    {
        _logger = logger;
        MigrateDatabase();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql()
            .UseLazyLoadingProxies();
    }

    private void MigrateDatabase()
    {
        try
        {
            Database.Migrate();
        }
        catch (NpgsqlException ex)
        {
            WeakReferenceMessenger.Default.Send(new ErrorMessage(DbErrorString));
            _logger.LogError(message: DbErrorString, exception: ex);
        }
    }
}