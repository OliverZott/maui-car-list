using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", a => a.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//var dbPath = Path.Join(Directory.GetCurrentDirectory(), "carlist.db");
//var conn = new SqliteConnection($"Data Source={dbPath}");
var conn = new SqliteConnection($"Data Source=C:\\data\\carlist.db");
builder.Services.AddDbContext<CarListDbContext>(o => o.UseSqlite(conn));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarListDbContext>();

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");


// API endpoints
app.MapGet("/cars", async (CarListDbContext db) => await db.Cars.ToListAsync());

app.MapGet("/cars/{id}", async (CarListDbContext db, int id) =>
    await db.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound()
);

app.MapPut("/cars/{id}", async (CarListDbContext db, int id, Car car) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record == null) Results.NotFound();

    // TODO Best practice use DTO
    record!.Make = car.Make;
    record.Model = car.Model;
    record.Vin = car.Vin;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/cars/{id}", async (int id, CarListDbContext db) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record == null) Results.NotFound();

    db.Remove(record);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPost("/cars", async (CarListDbContext db, Car car) =>
 {
     await db.AddAsync(car);
     await db.SaveChangesAsync();

     return Results.Created($"/cars/{car.Id}", car);
 });


app.Run();
