namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                // we can use AddDefaultPolicy here instead if we want to apply Cors to all endpoints
                options.AddPolicy(Constants.StandardCorsPolicyName,
                    policy =>
                    {
                        policy.WithOrigins(new[]
                        {
                            "https://mango-ocean-01027a710.3.azurestaticapps.net",
                            "https://witty-meadow-0bbf34210.3.azurestaticapps.net"
                        })
                        .WithMethods(new[]
                        {
                            "GET", "OPTIONS", "PUT", "POST"
                        });
                    });
            });

            // Add services to the container.

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

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}