using ExamenPoo1KennethGaldamez.Services;
using ExamenPoo1KennethGaldamez.Services.Interfaces;

namespace ExamenPoo1KennethGaldamez
{
    public class Startup
    {
            private IConfiguration configuration { get; }

            public Startup(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
                services.AddTransient<ITareasService, TareasService>();

            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseRouting();
                app.UseAuthorization();
                app.UseHttpsRedirection();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }



