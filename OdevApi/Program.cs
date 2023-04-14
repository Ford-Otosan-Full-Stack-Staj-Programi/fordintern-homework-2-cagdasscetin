using AutoMapper;
using OdevApi.Base;
using OdevApi.Data;
using Microsoft.Extensions.Configuration;
using OdevApi.Web.Extension;

public class Program
{
    public static JwtConfig JwtConfig { get; private set; }
    
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.


        // sonradan eklenenler ************
        // jwt inject
        JwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
        // inject - dependency injection **************
        builder.Services.AppDbContextDI(builder.Configuration);
        builder.Services.AddServices();
        builder.Services.AddMapperService();
        builder.Services.AddJwtAuthentication(); //incoming token validation
        builder.Services.AddCustomSwagger();
        // ********************************


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // jwt
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}