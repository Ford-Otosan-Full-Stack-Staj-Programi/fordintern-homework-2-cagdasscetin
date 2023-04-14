using OdevApi.Data;
using Microsoft.EntityFrameworkCore;

namespace OdevApi.Web.Extension;

public static class ProgramDbContextExtension
{
    public static void AppDbContextDI(this IServiceCollection services, IConfiguration configuration)
    {
        var dbtype = configuration.GetConnectionString("DbType");
        if (dbtype == "SQL")
        {
            var dbconfig = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbconfig));
        }
        else if (dbtype == "PostgreSQL")
        {
            var dbconfig = configuration.GetConnectionString("PostgreSQLConnection");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbconfig));
        }
    }

}
