using System.Reflection.Metadata;
using HypertheoryApiUtils;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace ApiWithMongo.Adapters;

public class GenericMongoAdapter
{
    private readonly IMongoCollection<Document> _documentCollection;
    private readonly ILogger<GenericMongoAdapter> _logger;

    public GenericMongoAdapter(ILogger<GenericMongoAdapter> logger, IOptions<MongoConnectionOptions> options)
    {
        _logger = logger;
        var clientSettings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);
        if (options.Value.LogCommands)
        {
            clientSettings.ClusterConfigurator = db =>
            {
                db.Subscribe<CommandStartedEvent>(e =>
                {
                    _logger.LogInformation($"Running {e.CommandName} - the command looks like this {e.Command.ToJson()}");
                });
            };
        }
        var conn = new MongoClient(clientSettings);

        var db = conn.GetDatabase(options.Value.Database);

        _documentCollection = db.GetCollection<Document>(options.Value.Collection);

    }

    public IMongoCollection<Document> GetDocumentCollection() => _documentCollection;
}
