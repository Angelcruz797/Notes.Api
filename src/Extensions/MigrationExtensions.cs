namespace Notes.Api.Extensions;

using Microsoft.EntityFrameworkCore;
using Notes.Api.Data;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.Migrate();
    }
}