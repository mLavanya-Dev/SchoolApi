using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using SchoolApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<SchoolDatabaseSettings>(builder.Configuration.GetSection("SchoolDatabaseSettings"));
builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var settings = new MongoClientSettings()
    {
        Scheme=ConnectionStringScheme.MongoDB,
        Server=new MongoServerAddress("localhost",27017),
    };
    return new MongoClient(settings);
});
builder.Services.AddSingleton<IStudentService,StudentService>();

builder.Services.AddSingleton<ICourseService,CourseService>();
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
