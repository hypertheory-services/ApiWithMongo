using ApiWithMongo.Adapters;
using HypertheoryApiUtils;

var builder = WebApplication.CreateBuilder(args);
// Routing
// Add the BsonId route Constraint. (e.g. "/professionals/{id:bsonid}")
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("bsonid", typeof(BsonIdConstraint));
});

// Config and Options
builder.Services.Configure<MongoConnectionOptions>(builder.Configuration.GetSection(MongoConnectionOptions.SectionName));
// Add services to the container.

builder.Services.AddSingleton<GenericMongoAdapter>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
