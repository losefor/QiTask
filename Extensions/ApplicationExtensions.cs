using Microsoft.EntityFrameworkCore;
using QiTask.Data.Data;

namespace QiTask.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationExtensions(this IServiceCollection services, IConfiguration config)
    {
        // Inject DBContext
        services.AddDbContext<DataContext>(opt =>
            opt.UseSqlite(config.GetConnectionString("DefaultConnection")));


        // Inject auto mapper 
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}