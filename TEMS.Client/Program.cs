using Microsoft.EntityFrameworkCore;
using TEMS.Business.Interface;
using TEMS.Data.DBContext;
using TEMS.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("EventManagementSystem"))
);

builder.Services.AddTransient<EventDbContext, EventDbContext>();
builder.Services.AddTransient<IEventsRepository, EventsRepository>();
builder.Services.AddTransient<ICitiesRepository, CitiesRepository>();
builder.Services.AddTransient<ISpeakersRepository, SpeakersRepository>();
builder.Services.AddTransient<ITalkDetailsRepository, TalkDetailsRepository>();
builder.Services.AddTransient<IVenueRepository, VenueRepository>();

// Add services to the container.

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

app.UseCors(options =>
    options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
