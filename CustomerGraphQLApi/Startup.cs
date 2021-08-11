using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CustomerGraphQLApi.Data;
using CustomerGraphQLApi.Services;
using CustomerGraphQLApi.GraphQL;
using CustomerGraphQLApi.ErrorHandling;

namespace CustomerGraphQLApi
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
            //The execution engine of Hot Chocolate executes resolvers in parallel.
            //This can lead to exceptions because the database context of Entity Framework cannot
            //handle more than one request in parallel
            //services.AddDbContext<CustomerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Used AddPooledDbContextFactory HotChocolate.Data.EnityFramework package 
            services.AddPooledDbContextFactory<CustomerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, poolSize: 20);
            services.AddScoped<CustomerContext>(p => p.GetRequiredService<IDbContextFactory<CustomerContext>>().CreateDbContext());

            #region GraphQL

            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<MutationType>();
            #endregion

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            var logger = loggerFactory.CreateLogger("CustomerService");

            services.AddScoped<ICustomerService>(p =>
            {                
                var dbContext = p.GetRequiredService<IDbContextFactory<CustomerContext>>().CreateDbContext();
                return new CustomerService(dbContext) { Logger = logger };
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddErrorFilter<ExceptionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
