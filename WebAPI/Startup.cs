using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using System.Reflection;
using System.Text;

using Swashbuckle.AspNetCore.Filters;
using Newtonsoft.Json.Serialization;

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
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ).AddNewtonsoftJson(options =>
                options.SerializerSettings.ContractResolver = new DefaultContractResolver()
            );
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            var connectionString = Configuration["ConnectionStrings:Connection"];
            services.AddDbContext<APIDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

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
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Prueba API",
                    Description = "Una API de prueba",
                    Contact = new OpenApiContact
                    {
                        Name = "Geosystems S.A.",
                        Url = new Uri("https://www.geosystems.com.ar")
                    }
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Autorizacion estandar, Usar Bearer. Ejemplo \"bearer {token}\"",
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba API v1");
                    c.InjectStylesheet("/swagger-ui/custom.css");
                });
            }

            app.UseHttpsRedirection();
    
            /*
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            */
        }
    }
}
