using AutoMapper;
using OdevApi.Data;
using OdevApi.Service;
using OdevApi.Service.Abstract;
using OdevApi.Service.Concrete;

namespace OdevApi.Web.Extension;

public static class ProgramExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        //UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //repository
        services.AddScoped<IGenericRepository<Account>, GenericRepository<Account>>();
        services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();

        //services
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IAccountService, AccountService>();

        //jwt
        services.AddScoped<ITokenManagementService, TokenManagementService>();
    }

    public static void AddMapperService(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        services.AddSingleton(mapperConfig.CreateMapper());
    }
}
