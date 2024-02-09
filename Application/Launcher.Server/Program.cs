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
    async (string filename, HttpContext httpContext) =>
    {
        var path = Path.Combine(dataFolder, filename);

        path = Path.GetFullPath(path);

        await using var fileStream = File.OpenRead(path);

        var rangeHeaderValues = httpContext.Request.Headers.Range;

        foreach (var headerValue in rangeHeaderValues)
        {
            if (headerValue is null)
                continue;

            var segments = headerValue.Split('=');

            if (segments.Length < 2)
                continue;

            var key = segments[0];

            if (!string.Equals(key, "bytes", StringComparison.OrdinalIgnoreCase))
                continue;

            var range = segments[1];

            var rangeValues = range.Split('-');

            if (rangeValues.Length != 2)
                continue;

            if (!long.TryParse(rangeValues[0], out var fromValue))
                continue;

            if (!long.TryParse(rangeValues[1], out var toValue))
                continue;

            fileStream.Seek(fromValue, SeekOrigin.Begin);

            var bufferSize = (int) (toValue - fromValue + 1);
            var array = new byte[bufferSize];
            var buffer = new Memory<byte>(array, 0, bufferSize);

            _ = await fileStream.ReadAsync(buffer);
            await httpContext.Response.Body.WriteAsync(buffer);

            await httpContext.Response.CompleteAsync();

            return;
        }

        await fileStream.CopyToAsync(httpContext.Response.Body);

        await httpContext.Response.CompleteAsync();
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