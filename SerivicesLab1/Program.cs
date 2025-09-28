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

#if DEBUG
DBManager dbm = new(":memory:");
DBPreparation.PrepareInMemoryDb(dbm);
#else
DBManager dbm = new(PathHelper.GetFilesDirectory("db2.sqlite3"));
#endif

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
    catch (Microsoft.Data.Sqlite.SqliteException ex)
    {
        Console.WriteLine(ex.Message);
        return ex.ErrorCode switch
        {
            19 => Results.Conflict("Tried to put the same data"),
            _ => Results.Problem("Something unknown happend with database"),
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.Problem("Something unknown happend or you tried to add roster with uncompatable stats");
    }
});

app.Run();
