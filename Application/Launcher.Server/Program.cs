var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string dataFolder =
#if DEBUG
    @"..\..\data";
#else
    "/app/data";
#endif

app.MapGet(
    "/{*filename}",
    (string filename) =>
    {
        var path = Path.Combine(dataFolder, filename);

        path = Path.GetFullPath(path);

        return Results.File(path);
    });

app.MapMethods(
    "/{*filename}",
    ["HEAD"],
    async (string filename, HttpContext httpContext) =>
    {
        var path = Path.Combine(dataFolder, filename);

        path = Path.GetFullPath(path);

        await using var fileStream = File.OpenRead(path);

        httpContext.Response.Headers.ContentLength = fileStream.Length;

        return httpContext.Response.CompleteAsync();
    });

app.Run();