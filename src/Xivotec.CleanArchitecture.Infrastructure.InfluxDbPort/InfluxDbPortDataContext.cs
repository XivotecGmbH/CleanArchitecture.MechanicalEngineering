using CommunityToolkit.Mvvm.Messaging;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Common.Interfaces;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort;

/// <summary>
/// Technology-specific implementation of <see cref="IDataContext"/>.
/// </summary>
public class InfluxDbPortDataContext : IDataContext
{
    private const string DbErrorString = "Database operation failed. \nCheck configuration " +
                                         "and server status and restart the application.";

    private readonly IConfiguration _configuration;
    private readonly ILogger<InfluxDbPortDataContext> _logger;
    private string _orgId = string.Empty;

    public Dictionary<Type, string> InfluxTypesToBucketDictionary { get; private set; } = [];

    public InfluxDBClient? Client { get; private set; }

    public string Org { get; private set; } = string.Empty;

    public InfluxDbPortDataContext(IConfiguration configuration, ILogger<InfluxDbPortDataContext> logger)
    {
        _configuration = configuration;
        _logger = logger;
        MigrateDatabase();
    }

    private void MigrateDatabase()
    {
        try
        {
            InfluxTypesToBucketDictionary = GenerateBucketNames();

            var url = _configuration.GetConnectionString("InfluxUrl");
            var token = _configuration.GetConnectionString("InfluxToken");
            Org = _configuration.GetConnectionString("InfluxOrg")!;

            Client = new InfluxDBClient(url, token);
            _orgId = GetOrganizationId(Org);
            MigrateBuckets(InfluxTypesToBucketDictionary);
        }
        catch (Exception ex)
        {
            WeakReferenceMessenger.Default.Send(new ErrorMessage(DbErrorString));
            _logger.LogError(message: DbErrorString, exception: ex);
        }

    }

    private void MigrateBuckets(Dictionary<Type, string> bucketDefinitions)
    {
        var bucketsApi = Client!.GetBucketsApi();
        foreach (var (_, name) in bucketDefinitions)
        {
            var foundBucket = bucketsApi.FindBucketByNameAsync(name).Result;
            if (foundBucket is null)
            {
                _ = bucketsApi.CreateBucketAsync(
                    name,
                    new BucketRetentionRules(everySeconds: 0),
                    _orgId).Result;
            }
        }
    }

    private string GetOrganizationId(string organizationName)
    {
        var orgApi = Client!.GetOrganizationsApi();
        var organizations = orgApi.FindOrganizationsAsync().Result;
        foreach (var organization in organizations)
        {
            if (organization.Name.Equals(organizationName))
            {
                return organization.Id;
            }
        }

        throw new InfluxMigrationException(
            "Specified organization does not exist. Please finish the setup process and set the created organization in app settings.");
    }

    private Dictionary<Type, string> GenerateBucketNames()
    {
        var typesDictionary = new Dictionary<Type, string>();
        var influxType = typeof(IInfluxPortType);
        var implementedTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => influxType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
            .ToList();

        foreach (var type in implementedTypes)
        {
            var bucketName = type.Name.Replace("EntryInfluxType", "");
            typesDictionary.Add(type, bucketName);
        }

        return typesDictionary;
    }

    public void Dispose()
    {
        Client!.Dispose();
    }
}