using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using System.Reflection;
using System.Text;

using Swashbuckle.AspNetCore.Filters;

using WebAPI.Services;

namespace WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            var connectionString = Configuration["ConnectionStrings:Connection"];
            services.AddDbContext<APIDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAppleService, AppleService>();
            services.AddTransient<IStreetService, StreetService>();

            services.AddHttpClient<IUserService, UserService>();
            services.AddHttpClient<IAppleService, AppleService>();

            var key = Encoding.ASCII.GetBytes(Configuration["AppSettings:SecretKey"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async (context) =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        await context.HttpContext.Response.WriteAsync("Acceso denegado");
                    }
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "2.0.0",
                    Title = "API de prueba",
                    Description = "Esta es una descripcion de prueba",
                    Contact = new OpenApiContact
                    {
                        Name = "Geosystems S.A.",
                        Url = new Uri("https://www.geosystems.com.ar")
                    }
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Escriba bearer y pegue el token de login separado por un espacio \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Prueba API v2");
                    c.InjectStylesheet("/swagger-ui/custom.css");
                });
            }

            app.UseHttpsRedirection();
        }
    }
}
