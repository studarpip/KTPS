using KTPS.Model.Repositories;
using KTPS.Model.Repositories.GroupMembers;
using KTPS.Model.Repositories.Groups;
using KTPS.Model.Repositories.Guests;
using KTPS.Model.Repositories.PasswordReset;
using KTPS.Model.Repositories.Registration;
using KTPS.Model.Repositories.User;
using KTPS.Model.Services.Groups;
using KTPS.Model.Services.Login;
using KTPS.Model.Services.Registration;
using KTPS.Model.Services.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KTPS.Server.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        RegisterServices(builder.Services);

        var app = builder.Build();

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IRepository, Repository>();
        services.AddSingleton<IRegistrationRepository, RegistrationRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IRegistrationService, RegistrationService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ILoginService, LoginService>();
        services.AddSingleton<IPasswordResetRepository, PasswordResetRepository>();
        services.AddSingleton<IGroupsService, GroupsService>();
        services.AddSingleton<IGroupsRepository, GroupsRepository>();
        services.AddSingleton<IGroupMembersRepository, GroupMembersRepository>();
        services.AddSingleton<IGuestsRepository, GuestsRepository>();
    }
}
