using MassTransit;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Spg.Mongo.DomainModel.Model;
using Spg.Mongo.DomainModel.Settings;
using Spg.Mongo.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

builder.Services.AddSingleton(options =>
{
    var mongoSettings = builder.Configuration.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
    var mongoClient = new MongoClient(mongoSettings?.ConnectionString ?? string.Empty);
    return mongoClient.GetDatabase(serviceSettings?.ServiceName ?? string.Empty);
});

builder.Services.AddSingleton(options =>
{
    var database = options.GetService<IMongoDatabase>();
    return new ProductRepository(database!, "Catalog");
});

builder.Services.AddMassTransit(options => {
    options.UsingRabbitMq((context, configurator) => 
    {
        var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
        configurator.Host(rabbitMqSettings.Host);
        configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.addMongo
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
