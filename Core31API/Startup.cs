using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;



namespace Core31API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorize",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer Scheam"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }

                        }, new string[]{ }
                    }
                });
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtToken:Issuer"],
                    ValidAudience = Configuration["JwtToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtToken:SecretKey"])) //Configuration["JwtToken:SecretKey"]
                };
            });
            //services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            //     .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            //services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            //{
            //    options.Authority = options.Authority + "/v2.0/";

            //    // Per the code below, this application signs in users in any Work and School
            //    // accounts and any Microsoft Personal Accounts.
            //    // If you want to direct Azure AD to restrict the users that can sign-in, change 
            //    // the tenant value of the appsettings.json file in the following way:
            //    // - only Work and School accounts => 'organizations'
            //    // - only Microsoft Personal accounts => 'consumers'
            //    // - Work and School and Personal accounts => 'common'

            //    // If you want to restrict the users that can sign-in to only one tenant
            //    // set the tenant value in the appsettings.json file to the tenant ID of this
            //    // organization, and set ValidateIssuer below to true.

            //    // If you want to restrict the users that can sign-in to several organizations
            //    // Set the tenant value in the appsettings.json file to 'organizations', set
            //    // ValidateIssuer, above to 'true', and add the issuers you want to accept to the
            //    // options.TokenValidationParameters.ValidIssuers collection
            //    options.TokenValidationParameters.ValidateIssuer = false;
            //});
            services.AddControllers();
            // Register the Swagger generator, defining 1 or more Swagger documents

            services.Configure<MyDBSetting>(Configuration.GetSection("DBSetting"));
            services.AddTransient<IUserRepository, UserRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });



            //if (env.IsDevelopment())
            //{

            //    app.UseDeveloperExceptionPage();
            //}


            // app.UseHttpsRedirection();

            app.UseRouting();
             app.UseAuthentication();
             app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
