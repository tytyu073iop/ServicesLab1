var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

DBManager dbm = new("db2.sqlite3");

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapGet("/", () => dbm.GetAllRosters());

app.MapPost("/", (Roster roster) =>
{
    try
    {
        dbm.AddRoster(roster);
        return Results.Created($"/roster/{roster.Playerid}", roster);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.Problem(".");
    }
});

app.Run();
