using CloudCustomer.API.Config;
using CloudCustomer.API.Services;

namespace CloudCustomer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            ConfigureServices(builder.Services, builder.Configuration);
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
       static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UsersApiOptions>(configuration.GetSection("UsersApiOptions"));
            services.AddTransient<IUsersService, UsersService>();
            services.AddHttpClient<IUsersService, UsersService>(); // indicate that userservice should get http client
        }
    }
}