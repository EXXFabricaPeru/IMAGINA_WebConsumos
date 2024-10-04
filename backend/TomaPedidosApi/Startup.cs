using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BOCNegocio;

namespace TomaPedidosApi
{
    public class Startup
    {
        readonly string CorsConfiguration = "_CorsConfiguration";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //var authkey = "THIS IS MY KEY KITCHEN";
            //services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(authkey));

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authkey)),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            //services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            //services.AddScoped<IBOCArticulo, BOCArticulo>();
            //services.AddScoped<IBOCCliente, BOCCliente>();
            //services.AddScoped<IBOCPedidoVenta, BOCPedidoVenta>();
            //services.AddScoped<IBOCSeguridad, BOCSeguridad>();
            //services.AddScoped<IBOCTablaGeneral, BOCTablaGeneral>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TomaPedidosApi", Version = "v1" });
            });

            services.AddCors(t => {
                                    t.AddPolicy(name: CorsConfiguration,
                                                builder => {
                                                    //builder.WithOrigins(new string[] { "http://localhost", "http://localhost:4200" })
                                                    builder
                                                    .AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod();
                                                });
                            }
            );

        }   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TomaPedidosApi v1"));
            }

            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(CorsConfiguration);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
