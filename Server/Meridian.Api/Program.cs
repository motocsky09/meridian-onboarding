using Meridian.Infrastructure;
using Meridian.Application;
using Meridian.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed sample data in Development, if the database is empty.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var mongoContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await SeedData.SeedAsync(mongoContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
