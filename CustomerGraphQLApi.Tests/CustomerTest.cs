using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Language;
using ApprovalTests;
using ApprovalTests.Reporters;
using CustomerGraphQLApi.GraphQL;
using CustomerGraphQLApi.Data;

namespace CustomerGraphQLApi.Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class CustomerTest
    {
        [Fact]
        public async void GetList()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(configuration);
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            var context = provider.GetRequiredService<CustomerContext>();
            context.Database.EnsureDeleted();
            DbInitializer.Initialize(context);

            ISchema schema = SchemaBuilder.New()
               .AddQueryType<Query>()
               .Create();

            IRequestExecutor executor = schema.MakeExecutable();

            IReadOnlyQueryRequest request = QueryRequestBuilder.New()
                .SetQuery("query{customers{id,name,email,code,status,isBlocked}}")
                .SetServices(provider)
                .Create();
            IExecutionResult result = await executor.ExecuteAsync(request);


            var resultJson = result.ToJson();

            Approvals.Verify(resultJson);

        }

        [Fact]
        public async void CreateCustomer()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(configuration);
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            var context = provider.GetRequiredService<CustomerContext>();
            context.Database.EnsureDeleted();
            DbInitializer.Initialize(context);

            ISchema schema = SchemaBuilder.New()
               .AddQueryType<Query>()
               .AddMutationType<MutationType>()
               .Create();

            IRequestExecutor executor = schema.MakeExecutable();

            var inputType = new ObjectValueNode(
               new ObjectFieldNode("Name", "Abc"),
               new ObjectFieldNode("Email", "abc@gmail.com"),               
               new ObjectFieldNode("Code", 7500),
               new ObjectFieldNode("Status", "ACTIVE"),
               new ObjectFieldNode("IsBlocked", false)
               );

            IReadOnlyQueryRequest request = QueryRequestBuilder.New()
                .SetQuery("mutation ($inputType:AddCustomer ){ create(input :$inputType) {   id }}")
                .SetServices(provider)
                .SetVariableValue("inputType", inputType)
                .Create();


            IExecutionResult result = await executor.ExecuteAsync(request);


            var resultJson = result.ToJson();

            Approvals.Verify(resultJson);
        }

        [Fact]
        public async void UpdateCustomerNotFound()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(configuration);
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            var context = provider.GetRequiredService<CustomerContext>();
            context.Database.EnsureDeleted();
            DbInitializer.Initialize(context);

            ISchema schema = SchemaBuilder.New()
               .AddQueryType<Query>()
               .AddMutationType<MutationType>()
               .Create();

            IRequestExecutor executor = schema.MakeExecutable();

            var inputType = new ObjectValueNode(
               new ObjectFieldNode("id", 11),
               new ObjectFieldNode("name", "Abc"),
               new ObjectFieldNode("email", "abc@gmail.com"),
               new ObjectFieldNode("createdAt", "2021-08-09T21:12:39.913+05:30"),
               new ObjectFieldNode("code", 7500),
               new ObjectFieldNode("status", "ACTIVE"),
               new ObjectFieldNode("isBlocked", false)
               );

            IReadOnlyQueryRequest request = QueryRequestBuilder.New()
                .SetQuery("mutation ($inputType:CustomerInput ){ update(customer :$inputType)}")
                .SetServices(provider)
                .SetVariableValue("inputType", inputType)
                .Create();


            IExecutionResult result = await executor.ExecuteAsync(request);

            Assert.Contains("CustomerNotFoundException", result.Errors[0].Exception.Message.ToString());
        }

        [Fact]
        public async void DeleteCustomerNotFound()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            IServiceCollection services = new ServiceCollection();

            var startup = new Startup(configuration);
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            var context = provider.GetRequiredService<CustomerContext>();
            context.Database.EnsureDeleted();
            DbInitializer.Initialize(context);

            ISchema schema = SchemaBuilder.New()
               .AddQueryType<Query>()
               .AddMutationType<MutationType>()
               .Create();

            IRequestExecutor executor = schema.MakeExecutable();

            var inputType = new ObjectValueNode(
               new ObjectFieldNode("Id", 11)              
               );

            IReadOnlyQueryRequest request = QueryRequestBuilder.New()
                .SetQuery("mutation ($inputType:DeleteCustomerById ){ deletedbyid(deleteCustomerById :$inputType)}")
                .SetServices(provider)
                .SetVariableValue("inputType", inputType)
                .Create();


            IExecutionResult result = await executor.ExecuteAsync(request);

            Assert.Contains("CustomerNotFoundException", result.Errors[0].Exception.Message.ToString());
        }
    }
}
