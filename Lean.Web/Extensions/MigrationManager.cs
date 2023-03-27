using Lean.Domain.Exceptions;
using Lean.Domain.Exceptions.DataBase;
using Lean.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lean.Web.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {

                        throw new MigrationBadRequestException();
                    }
                }
            }
            return host;
        }
    }
}
