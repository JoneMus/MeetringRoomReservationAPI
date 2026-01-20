using Microsoft.EntityFrameworkCore;
using MeetingRoomReservationAPI.Data;
using MeetingRoomReservationAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookingContext>(options =>
    options.UseInMemoryDatabase("MeetingRoomDb"));

builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

// Varmistetaan, että tietokanta luodaan ja seed-data ajetaan
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookingContext>();
    context.Database.EnsureCreated();
}

app.Run();
